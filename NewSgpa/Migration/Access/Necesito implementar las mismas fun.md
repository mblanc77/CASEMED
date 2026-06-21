Necesito implementar las mismas funcionalidades que ciertos modulos del un proyecto dnn  C:\\Personal\\Gestion\\CMU\\src\\dnn\\DesktopModules, el mismo actualmente solo sirve para gestion de usuarios importandos de un backend y provee ciertas funcionalidades al usuario aka Colegiado. Ej; permite actualizar los datos personales del backend, emitir un certificador de estar activo y al día, emitir un reporte de estado de cuenta, ver una lista de expedientes, cargar una declaracion jurada de bajos ingresos, etc.



Para eso se creo un Proyecto asp.net CuentasPersonales.Web, que debería autenticarse con el Proyecto IndentityServer. La idea es migrar todos los usuarios del dnn, y accedería a los datos via una api CuentasPersonales.Api.



También existe un Proyecto MAUI (blazor Hybrid) para en una segunda instancia porder gneerar una aplicacion web.



Puedes referenciar los módulos dnn:

CMU\_ADModule -> Actualizar datos personales

CMU\_CRModule -> Emitir certificado de colegiacion  para el usuario.

CMU\_DJModule -> Ver/descargar estado de cuenta del usuario

CMU\_EXPModule -> Visualizacion de expendientes.



Tambien existe una aplicación mobile en Xamarin Forms, que realiza las misma funcionalidades de la web:

**C:\\Personal\\Gestion\\CMU\\Mobile\\CMU.Mobile**



**Si tienes dudas me consultas**



**Voy a pegar Abajo el prompt inicial que le solicité a otra herramienta de IA, pero que no veo terminado. Esto para que lo tengas como referencia:**



**"Tengo los proyectos referencia bajo C:\\Personal\\Gestion\\CMU\\src**



**Necesito migrar una aplicación DNN (carpeta dnn) a una aplicación ASP.NET Core Blazor Hybrid.**



**La aplicación debe utilizar una interfaz de usuario moderna; puedes usar controles de DevExpress 25.2, ya que están instalados en esta máquina.**



**No necesitamos la funcionalidad de CRM de DNN; solo utilizamos la gestión de usuarios y algunos módulos personalizados desarrollados por nosotros. Puedes encontrarlos en la carpeta DesktopModules, específicamente los que se llaman CMU\_xxxx.**



**No todo se utilizan actualmente**



**La idea es que para cada usuario (Colegiado) se proporcione un conjunto de operaciones que pueda realizar.**



**\* Cambiar ciertos datos personales**

**\* Emitir certificado de colegiación (si cumple condiciones).**

**\* Emitir un estado de cuenta.**

**\* Ingresar una DJ de bajos ingresos.**

**\* Ver una lista de expedientes generados al usuario.**





**Tambien puedes consultar las caracteristicas de la aplicación movil (carpeta Mobile) que hace básicamente lo mismo. Esta desarrollado en Xamarin forms. La idea es generar un**

**nuevo aplicativo hibrido.**



**Tambien debe soportar usuarios administradores que permitan impersonarse en algun colegiado y poder realizer dichas operaciones por ellos.**



**Crea una nueva carpeta bajo src, y crea los proyectos necesarios de Visual Studio. Idealmente en .net10. Puedes re utilizer las clases XPO definidas en src\\Admin\\CMU.Module.DAL**

**"**

