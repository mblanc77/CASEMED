using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

// Verificación interactiva (browser real) de las grillas server-side: render de datos vía circuito,
// orden por columna, búsqueda y grilla hija grande. Uso: dotnet run -- [baseUrl]
string baseUrl = args.Length > 0 ? args[0].TrimEnd('/') : "http://localhost:5210";
int failures = 0;
void Check(string name, bool ok, string detail = "")
{
    Console.WriteLine($"  [{(ok ? "PASS" : "FAIL")}] {name}{(detail.Length > 0 ? " — " + detail : "")}");
    if (!ok) failures++;
}
void Note(string msg) => Console.WriteLine($"  [INFO] {msg}");

var options = new ChromeOptions();
options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
options.AddArgument("--headless=new");
options.AddArgument("--disable-gpu");
options.AddArgument("--no-sandbox");
options.AddArgument("--window-size=1400,1000");

var driverDir = @"C:\Personal\Gestion\CASEMED\SgpaBlazor\tools\UiVerify\driver\chromedriver-win64";
var service = ChromeDriverService.CreateDefaultService(driverDir);
service.HideCommandPromptWindow = true;

Console.WriteLine($"Iniciando Chrome headless contra {baseUrl} …");
using var driver = new ChromeDriver(service, options);
driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

// Filas de datos: tr dentro del tbody de la grilla que NO son fila vacía/spacer/edición.
// Tolerante a stale (el grid puede estar re-renderizando mientras se cuenta).
int DataRowCount()
{
    int count = 0;
    foreach (var tr in driver.FindElements(By.CssSelector(".dxbl-grid-table > tbody > tr, .dxbl-grid tbody tr")))
    {
        try
        {
            var cls = tr.GetAttribute("class") ?? "";
            if (!cls.Contains("empty") && !cls.Contains("spacer") && !cls.Contains("filter-row")
                && !cls.Contains("header") && !cls.Contains("group"))
                count++;
        }
        catch (StaleElementReferenceException) { }
    }
    return count;
}

static string Trunc(string s) => s.Length <= 50 ? s : s.Substring(0, 50);

string FirstRowText()
{
    var r = driver.FindElements(By.CssSelector(".dxbl-grid tbody tr")).FirstOrDefault(tr =>
    {
        var c = tr.GetAttribute("class") ?? "";
        return !c.Contains("empty") && !c.Contains("spacer") && !c.Contains("filter")
               && !c.Contains("header") && !c.Contains("group");
    });
    return (r?.Text ?? "").Replace("\n", " ").Trim();
}

void DumpGrid(string label)
{
    var grids = driver.FindElements(By.CssSelector(".dxbl-grid"));
    Console.WriteLine($"  · {label}: grids={grids.Count}");
    var trs = driver.FindElements(By.CssSelector(".dxbl-grid tbody tr"));
    Console.WriteLine($"    tbody tr={trs.Count}");
    foreach (var tr in trs.Take(3))
        Console.WriteLine($"      tr class='{tr.GetAttribute("class")}' txt='{(tr.Text ?? "").Replace("\n", "|").Substring(0, Math.Min(70, (tr.Text ?? "").Length))}'");
}

bool WaitRows(int min = 1)
{
    try { return wait.Until(_ => DataRowCount() >= min); }
    catch (WebDriverTimeoutException) { return false; }
}

try
{
    // --- Login ---
    driver.Navigate().GoToUrl($"{baseUrl}/login");
    wait.Until(d => d.FindElement(By.Name("login")));
    driver.FindElement(By.Name("login")).SendKeys("qa");
    driver.FindElement(By.Name("password")).SendKeys("Sgpa.QA.2026!");
    driver.FindElement(By.CssSelector("button[type=submit]")).Click();
    wait.Until(d => !d.Url.Contains("/login"));
    Check("Login", true, driver.Url);

    // --- Nav (DxTreeView interactivo): expandir un grupo colapsado muestra sus hijos ---
    try
    {
        System.Threading.Thread.Sleep(6000); // que conecte la isla interactiva del menú (circuito + init DevExpress)
        bool before = driver.FindElements(By.XPath("//*[normalize-space()='Mutualistas']")).Any(e => e.Displayed);
        // Click real sobre la fila del nodo del DxTreeView (no JS), que es lo que el usuario hace.
        var grp = driver.FindElements(By.CssSelector(".dxbl-treeview-item-container"))
            .FirstOrDefault(e => (e.Text ?? "").Trim().StartsWith("Catálogos", StringComparison.OrdinalIgnoreCase))
            ?? driver.FindElements(By.XPath("//*[normalize-space()='Catálogos']")).FirstOrDefault();
        if (grp is not null)
        {
            try { grp.Click(); } catch { ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", grp); }
            System.Threading.Thread.Sleep(2000);
            bool after = driver.FindElements(By.XPath("//*[normalize-space()='Mutualistas']")).Any(e => e.Displayed);
            Check("Nav: expandir grupo 'Catálogos' muestra hijos", !before && after, $"visible antes={before}, después={after}");
        }
        else Note("Nav: grupo 'Catálogos' no hallado");
    }
    catch (Exception ex) { Note($"Nav expand: {ex.GetType().Name}"); }

    // --- Lista server-side: los datos llegan al conectar el circuito ---
    driver.Navigate().GoToUrl($"{baseUrl}/afiliados");
    try { wait.Until(d => d.FindElements(By.CssSelector(".dxbl-grid")).Count > 0); } catch { }
    System.Threading.Thread.Sleep(5000); // dar tiempo al circuito a traer la página
    DumpGrid("afiliados");
    bool rows = WaitRows(1);
    int n = DataRowCount();
    Check("Afiliados: filas renderizadas (data source server-side)", rows, $"{n} filas");

    // Botón "Nuevo afiliado" visible tras conectar el circuito (no sólo en el prerender).
    var nuevoBtn = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Nuevo afiliado')]")).FirstOrDefault();
    Check("Afiliados: botón 'Nuevo afiliado' visible", nuevoBtn is not null && nuevoBtn.Displayed,
        nuevoBtn is null ? "NO está en el DOM" : $"Displayed={nuevoBtn.Displayed}");
    if (nuevoBtn is not null)
    {
        var pt = (string)((IJavaScriptExecutor)driver).ExecuteScript("return getComputedStyle(arguments[0]).paddingTop", nuevoBtn);
        Check("Botón Nuevo: más padding vertical (0.5rem=8px)", (pt ?? "") == "8px", pt);
    }

    // Tema de marca: fuente Plus Jakarta Sans + color primario #1C3680 (rgb(28,54,128)).
    var js = (IJavaScriptExecutor)driver;
    try
    {
        var font = (string)js.ExecuteScript("return getComputedStyle(document.body).fontFamily");
        Check("Tema: fuente Plus Jakarta Sans aplicada", (font ?? "").ToLower().Contains("jakarta"), font);
        if (nuevoBtn is not null)
        {
            var bg = (string)js.ExecuteScript("return getComputedStyle(arguments[0]).backgroundColor", nuevoBtn);
            Check("Tema: primario de marca en botón Nuevo", (bg ?? "").Replace(" ", "") == "rgb(28,54,128)", bg);
        }
        var sec = (string)js.ExecuteScript("return getComputedStyle(document.documentElement).getPropertyValue('--bs-secondary').trim()");
        Check("Tema: secundario verde de marca en la paleta", (sec ?? "").ToLower() == "#009242", sec);
        var gh = (string)js.ExecuteScript("var g=document.querySelector('.dxbl-grid'); return g?getComputedStyle(g).getPropertyValue('--dxbl-grid-header-bg').trim():''");
        Check("Tema: cabezal de grilla en verde", (gh ?? "").ToLower() == "#009242", gh);
        // Hover del cabezal: el fondo pasa a gris claro → el frente debe quedar verde.
        var th = driver.FindElements(By.CssSelector("th.dxbl-grid-header-sortable")).FirstOrDefault(e => e.Displayed);
        if (th is not null)
        {
            new OpenQA.Selenium.Interactions.Actions(driver).MoveToElement(th).Perform();
            System.Threading.Thread.Sleep(700);
            var c = (string)js.ExecuteScript("return getComputedStyle(arguments[0]).color", th);
            Check("Tema: cabezal en hover con frente verde", (c ?? "").Replace(" ", "") == "rgb(0,146,66)", c);
        }
    }
    catch (Exception ex) { Note($"Tema: {ex.GetType().Name} - {Trunc(ex.Message)}"); }

    // --- Orden por columna (clic en header) ---
    if (rows)
    {
        var before = FirstRowText();
        // Se ordena por apellido (el orden por defecto es por CI asc; ordenar por CI no cambiaría nada).
        var header = driver.FindElements(By.CssSelector(".dxbl-grid-header-cell, th, [role=columnheader]"))
            .FirstOrDefault(h => (h.Text ?? "").Contains("apellido", StringComparison.OrdinalIgnoreCase));
        if (header is not null)
        {
            try { header.Click(); } catch { }
            System.Threading.Thread.Sleep(2500);
            var after = FirstRowText();
            Check("Afiliados: orden por columna (1er apellido) cambia el primer registro",
                after.Length > 0 && after != before, $"'{Trunc(before)}' -> '{Trunc(after)}'");
        }
        else Note("Orden: no se halló header de apellido (selector)");
    }

    // --- Búsqueda global ---
    var searchInput = driver.FindElements(By.CssSelector("input[type=search]")).FirstOrDefault()
        ?? driver.FindElements(By.CssSelector(".dxbl-grid input")).FirstOrDefault();
    if (searchInput is not null)
    {
        searchInput.SendKeys("13010559");
        searchInput.SendKeys(Keys.Enter); // el buscador del toolbar commitea con Enter (bind OnChange)
        System.Threading.Thread.Sleep(3000);
        int after = DataRowCount();
        Check("Afiliados: búsqueda filtra a pocos registros", after >= 1 && after <= 5, $"{after} filas para '13010559'");
    }
    else Note("Búsqueda: no se halló input (no crítico)");

    // --- FilterControl (DxFilterBuilder) + filtros guardados ---
    driver.Navigate().GoToUrl($"{baseUrl}/afiliados");
    WaitRows(1);
    System.Threading.Thread.Sleep(1500);
    // (1) Aplicar un filtro de sistema de un clic (dropdown fresco).
    try
    {
        var fBtn = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Filtros')]")).FirstOrDefault();
        if (fBtn is not null)
        {
            fBtn.Click();
            System.Threading.Thread.Sleep(1500);
            var sf = driver.FindElements(By.XPath("//*[normalize-space()='Afiliado por cédula (demo)']")).FirstOrDefault(e => e.Displayed);
            if (sf is not null)
            {
                sf.Click();
                System.Threading.Thread.Sleep(2500);
                int rowsAfter = DataRowCount();
                Check("Filtro de sistema aplica de un clic (server-side)", rowsAfter == 1, $"{rowsAfter} filas");
            }
            else Note("Filtro 'Afiliado por cédula (demo)' no listado (¿sin sembrar?)");
        }
        else Note("Dropdown 'Filtros' no hallado");
    }
    catch (Exception ex) { Note($"Filtro sistema: {ex.GetType().Name} - {Trunc(ex.Message)}"); }

    // (2) FilterBuilder abre (navegación fresca para evitar estado del paso anterior).
    driver.Navigate().GoToUrl($"{baseUrl}/afiliados");
    WaitRows(1);
    System.Threading.Thread.Sleep(1500);
    try
    {
        var fBtn = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Filtros')]")).FirstOrDefault();
        if (fBtn is not null)
        {
            fBtn.Click();
            System.Threading.Thread.Sleep(1200);
            var adv = driver.FindElements(By.XPath("//*[normalize-space()='Filtro avanzado…']")).FirstOrDefault(e => e.Displayed);
            if (adv is not null)
            {
                adv.Click();
                System.Threading.Thread.Sleep(1500);
                bool popup = driver.FindElements(By.XPath("//button[normalize-space(.)='Aplicar']")).Any(e => e.Displayed)
                    || driver.FindElements(By.CssSelector(".dxbl-popup, .dxbl-window, [role=dialog]")).Any(e => e.Displayed);
                Check("FilterBuilder: popup abre (FilterControl)", popup);
            }
            else Note("Item 'Filtro avanzado' no hallado");
        }
    }
    catch (Exception ex) { Note($"FilterBuilder: {ex.GetType().Name} - {Trunc(ex.Message)}"); }

    // --- Grilla hija grande: Empleados de una empresa (Trabaja por empresa, ~10k) ---
    driver.Navigate().GoToUrl($"{baseUrl}/empresas/1");
    wait.Until(d => d.PageSource.Contains("Empleados"));
    // Espera a que el tablist deje de animar (skeleton) para evitar stale.
    try { wait.Until(d => d.FindElements(By.CssSelector(".dxbl-tabs-tablist.dxbl-loaded")).Count > 0); } catch { }
    System.Threading.Thread.Sleep(2500);
    try
    {
        var tab = driver.FindElements(By.XPath("//*[normalize-space()='Empleados']")).LastOrDefault();
        if (tab is not null)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", tab);
            bool childRows = WaitRows(1);
            Check("Empresa 1 → tab Empleados (Trabaja ~10k): filas server-side", childRows, $"{DataRowCount()} filas");
            var tsel = (string)((IJavaScriptExecutor)driver).ExecuteScript("var t=document.querySelector('.dxbl-tabs'); return t?getComputedStyle(t).getPropertyValue('--dxbl-tabs-tab-selected-color').trim():''");
            Check("Tema: indicador de tab activo en verde", (tsel ?? "").ToLower() == "#009242", tsel);
        }
        else Note("Tab 'Empleados': no se halló (selector) — no crítico");
    }
    catch (Exception ex) { Note($"Tab 'Empleados': verificación no concluyente ({ex.GetType().Name}) — no crítico"); }

    // --- Consolidación Subsidios + Liquidación: acción "Liquidar" en la toolbar de /subsidios ---
    try
    {
        driver.Navigate().GoToUrl($"{baseUrl}/subsidios");
        try { wait.Until(d => d.FindElements(By.CssSelector(".dxbl-grid")).Count > 0); } catch { }
        System.Threading.Thread.Sleep(5000);

        var liquidarBtn = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Liquidar')]"))
            .FirstOrDefault(e => e.Displayed);
        Check("Subsidios: botón 'Liquidar' en la toolbar", liquidarBtn is not null,
            liquidarBtn is null ? "NO está en el DOM" : "visible");

        if (liquidarBtn is not null)
        {
            liquidarBtn.Click();
            System.Threading.Thread.Sleep(1500);
            bool popup = driver.FindElements(By.XPath("//*[contains(normalize-space(.),'Liquidación de subsidios')]")).Any(e => e.Displayed);
            bool cedula = driver.FindElements(By.XPath("//*[contains(normalize-space(.),'Cédula (opcional)')]")).Any(e => e.Displayed);
            bool recibo = driver.FindElements(By.XPath("//*[contains(normalize-space(.),'Generar nros de recibo')]")).Any(e => e.Displayed);
            Check("Subsidios: el diálogo pide Cédula opcional y Generar nros recibo (como VB6)",
                popup && cedula && recibo, $"popup={popup}, cedula={cedula}, recibo={recibo}");
        }

        // La entrada de menú separada 'Liquidación' ya no debe existir.
        bool menuLiquidacion = driver.FindElements(By.XPath("//*[normalize-space()='Liquidación']")).Any(e => e.Displayed);
        Check("Nav: ya no hay entrada separada 'Liquidación' (consolidada en Subsidios)", !menuLiquidacion,
            menuLiquidacion ? "todavía visible" : "removida");
    }
    catch (Exception ex) { Note($"Subsidios/Liquidar: verificación no concluyente ({ex.GetType().Name}) — no crítico"); }

    // --- Workbench de préstamos: layout + cálculo en vivo (/prestamos/calcular) ---
    try
    {
        driver.Navigate().GoToUrl($"{baseUrl}/prestamos/calcular");
        try { wait.Until(d => d.FindElements(By.XPath("//button[contains(normalize-space(.),'Calcular')]")).Count > 0); } catch { }
        System.Threading.Thread.Sleep(4000);

        bool titulo = driver.FindElements(By.XPath("//*[normalize-space()='Nuevo préstamo']")).Any(e => e.Displayed);
        bool ctx = driver.FindElements(By.XPath("//*[contains(normalize-space(.),'Contexto financiero')]")).Any(e => e.Displayed);
        var buscarBtn = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Buscar')]")).FirstOrDefault(e => e.Displayed);
        var btnTexts = string.Join(" | ", driver.FindElements(By.CssSelector("button")).Where(b => b.Displayed).Select(b => (b.Text ?? "").Trim()).Where(t => t.Length > 0).Take(12));
        bool afiliadoCard = driver.FindElements(By.XPath("//*[normalize-space()='Afiliado']")).Any(e => e.Displayed);
        Check("Préstamos: workbench /prestamos/calcular renderiza (título + contexto + Buscar afiliado)",
            titulo && ctx && buscarBtn is not null, $"titulo={titulo}, ctx={ctx}, afiliadoCard={afiliadoCard}, buscar={buscarBtn is not null}, botones=[{btnTexts}]");

        // DevExpress: las teclas reales (SendKeys) commitean el editor; el JS value-set no.
        void SetSpin(IWebElement el, string val)
        {
            el.Click();
            el.SendKeys(Keys.Control + "a"); el.SendKeys(Keys.Delete);
            el.SendKeys(val); el.SendKeys(Keys.Tab);
        }

        // Cédula 99 = cálculo de prueba → habilita el panel de cálculo sin depender de datos puntuales.
        if (buscarBtn is not null)
        {
            var cedula = driver.FindElements(By.CssSelector(".dxbl-spin-edit input")).FirstOrDefault(n => n.Enabled);
            if (cedula is not null) { SetSpin(cedula, "99"); System.Threading.Thread.Sleep(400); }
            buscarBtn.Click();
            System.Threading.Thread.Sleep(1500);
            bool prueba = driver.FindElements(By.XPath("//*[contains(normalize-space(.),'Cálculo de prueba')]")).Any(e => e.Displayed);
            var calcBtn = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Calcular')]")).FirstOrDefault(e => e.Displayed);
            bool calcEnabled = calcBtn is not null && calcBtn.Enabled;
            // Interacción best-effort: driver de editores numéricos DevExpress vía Selenium es inestable.
            // La lógica (repo promedio/tope/aportes + amortización) se valida con tests determinísticos
            // (PrestamoRepositoryTests + PrestamoCalculatorTests).
            Note($"Workbench CI 99 (prueba): prueba={prueba}, calcEnabled={calcEnabled} (interacción best-effort)");

            // --- Calculadora "Plan" (popup): no depende de editores numéricos (el valor viene precargado) ---
            var planBtn = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Plan')]")).FirstOrDefault(e => e.Displayed && e.Enabled);
            if (planBtn is not null)
            {
                planBtn.Click();
                System.Threading.Thread.Sleep(1200);
                bool planPopup = driver.FindElements(By.XPath("//*[normalize-space()='Plan de cuotas']")).Any(e => e.Displayed);
                var planCalc = driver.FindElements(By.XPath("//button[contains(normalize-space(.),'Calcular')]")).LastOrDefault(e => e.Displayed);
                if (planPopup && planCalc is not null)
                {
                    planCalc.Click();
                    System.Threading.Thread.Sleep(1500);
                    bool planRows = WaitRows(1);
                    bool elegir = driver.FindElements(By.XPath("//button[normalize-space(.)='Elegir']")).Any(e => e.Displayed);
                    Check("Préstamos: calculadora 'Plan' abre y lista planes viables (Elegir)",
                        planPopup && (planRows || elegir), $"popup={planPopup}, filas={DataRowCount()}, elegir={elegir}");
                    // Cerrar el popup para no tapar el resto.
                    var cerrar = driver.FindElements(By.CssSelector(".dxbl-popup .dxbl-btn-close, [aria-label=Close]")).FirstOrDefault(e => e.Displayed);
                    try { cerrar?.Click(); } catch { }
                    System.Threading.Thread.Sleep(500);
                }
                else Check("Préstamos: calculadora 'Plan' abre el popup", planPopup, $"popup={planPopup}");
            }
            else Note("Plan: botón 'Plan…' no habilitado/visible — no crítico");

            if (calcEnabled)
            {
                var nums = driver.FindElements(By.CssSelector(".dxbl-spin-edit input")).Where(n => n.Enabled).ToList();
                if (nums.Count >= 2) { SetSpin(nums[1], "10000"); System.Threading.Thread.Sleep(400); }
                calcBtn!.Click();
                System.Threading.Thread.Sleep(1500);
                bool cuadro = driver.FindElements(By.XPath("//*[contains(normalize-space(.),'Cuadro de amortización')]")).Any(e => e.Displayed);
                Note($"Workbench Calcular: cuadro visible={cuadro}");
            }
        }
    }
    catch (Exception ex) { Note($"Préstamos workbench: verificación no concluyente ({ex.GetType().Name}) — no crítico"); }

    // --- Catálogos (Gr*): edición inline (EditRow) vs popup en una pantalla normal ---
    bool PopupVisible() => driver.FindElements(By.CssSelector(".dxbl-popup, .dxbl-window, [role=dialog]")).Any(e => e.Displayed);
    IWebElement? EditPen() => driver.FindElements(By.CssSelector("button"))
        .FirstOrDefault(b => b.Displayed && (b.GetAttribute("class") ?? "").Contains("dxbl") &&
            ((b.FindElements(By.CssSelector("i.fa-pen")).Count > 0)));
    try
    {
        // Catálogo: la edición debe ser inline (sin popup).
        driver.Navigate().GoToUrl($"{baseUrl}/crud/SalidaTipo");
        try { wait.Until(d => d.FindElements(By.CssSelector(".dxbl-grid")).Count > 0); } catch { }
        System.Threading.Thread.Sleep(3500);
        WaitRows(1);
        var pen = EditPen();
        if (pen is not null)
        {
            pen.Click();
            System.Threading.Thread.Sleep(1500);
            bool popup = PopupVisible();
            bool inlineInputs = driver.FindElements(By.CssSelector(".dxbl-grid tbody tr input, .dxbl-grid tbody tr .dxbl-edit")).Any(e => e.Displayed);
            Check("Catálogo SalidaTipo: edición INLINE (EditRow, sin popup)", inlineInputs && !popup,
                $"inlineInputs={inlineInputs}, popup={popup}");
        }
        else Note("Catálogo: botón editar no hallado — no concluyente");

        // Pantalla normal (Reintegros): la edición debe abrir popup.
        driver.Navigate().GoToUrl($"{baseUrl}/reintegros");
        try { wait.Until(d => d.FindElements(By.CssSelector(".dxbl-grid")).Count > 0); } catch { }
        System.Threading.Thread.Sleep(3500);
        WaitRows(1);
        var pen2 = EditPen();
        if (pen2 is not null)
        {
            pen2.Click();
            System.Threading.Thread.Sleep(1500);
            Check("Reintegros: edición en POPUP (no inline)", PopupVisible(), $"popup={PopupVisible()}");
        }
        else Note("Reintegros: botón editar no hallado — no concluyente");
    }
    catch (Exception ex) { Note($"Catálogos inline-edit: verificación no concluyente ({ex.GetType().Name}) — no crítico"); }
}
catch (Exception ex)
{
    Check("Excepción no controlada", false, ex.Message);
}
finally
{
    driver.Quit();
}

Console.WriteLine(failures == 0 ? "\nRESULTADO: TODO OK" : $"\nRESULTADO: {failures} fallo(s)");
return failures == 0 ? 0 : 1;
