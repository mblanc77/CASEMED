
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace SgpaNew.Client
{
    public partial class SgpaService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public SgpaService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/Sgpa/");
        }


        public async System.Threading.Tasks.Task ExportAdPreJubsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAdPreJubsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAdPreJubs(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AdPreJub>> GetAdPreJubs(Query query)
        {
            return await GetAdPreJubs(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AdPreJub>> GetAdPreJubs(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AdPreJubs");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAdPreJubs(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AdPreJub>>(response);
        }

        partial void OnCreateAdPreJub(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJub> CreateAdPreJub(SgpaNew.Server.Models.Sgpa.AdPreJub adPreJub = default(SgpaNew.Server.Models.Sgpa.AdPreJub))
        {
            var uri = new Uri(baseUri, $"AdPreJubs");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(adPreJub), Encoding.UTF8, "application/json");

            OnCreateAdPreJub(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AdPreJub>(response);
        }

        partial void OnDeleteAdPreJub(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAdPreJub(int ci = default(int))
        {
            var uri = new Uri(baseUri, $"AdPreJubs({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAdPreJub(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAdPreJubByCi(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJub> GetAdPreJubByCi(string expand = default(string), int ci = default(int))
        {
            var uri = new Uri(baseUri, $"AdPreJubs({ci})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAdPreJubByCi(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AdPreJub>(response);
        }

        partial void OnUpdateAdPreJub(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAdPreJub(int ci = default(int), SgpaNew.Server.Models.Sgpa.AdPreJub adPreJub = default(SgpaNew.Server.Models.Sgpa.AdPreJub))
        {
            var uri = new Uri(baseUri, $"AdPreJubs({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", adPreJub.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(adPreJub), Encoding.UTF8, "application/json");

            OnUpdateAdPreJub(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAdPreJubPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAdPreJubPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAdPreJubPagos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AdPreJubPago>> GetAdPreJubPagos(Query query)
        {
            return await GetAdPreJubPagos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AdPreJubPago>> GetAdPreJubPagos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AdPreJubPagos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAdPreJubPagos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AdPreJubPago>>(response);
        }

        partial void OnCreateAdPreJubPago(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJubPago> CreateAdPreJubPago(SgpaNew.Server.Models.Sgpa.AdPreJubPago adPreJubPago = default(SgpaNew.Server.Models.Sgpa.AdPreJubPago))
        {
            var uri = new Uri(baseUri, $"AdPreJubPagos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(adPreJubPago), Encoding.UTF8, "application/json");

            OnCreateAdPreJubPago(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AdPreJubPago>(response);
        }

        partial void OnDeleteAdPreJubPago(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAdPreJubPago(int adPreJubPagoId = default(int))
        {
            var uri = new Uri(baseUri, $"AdPreJubPagos({adPreJubPagoId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAdPreJubPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAdPreJubPagoByAdPreJubPagoId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJubPago> GetAdPreJubPagoByAdPreJubPagoId(string expand = default(string), int adPreJubPagoId = default(int))
        {
            var uri = new Uri(baseUri, $"AdPreJubPagos({adPreJubPagoId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAdPreJubPagoByAdPreJubPagoId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AdPreJubPago>(response);
        }

        partial void OnUpdateAdPreJubPago(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAdPreJubPago(int adPreJubPagoId = default(int), SgpaNew.Server.Models.Sgpa.AdPreJubPago adPreJubPago = default(SgpaNew.Server.Models.Sgpa.AdPreJubPago))
        {
            var uri = new Uri(baseUri, $"AdPreJubPagos({adPreJubPagoId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", adPreJubPago.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(adPreJubPago), Encoding.UTF8, "application/json");

            OnUpdateAdPreJubPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAfeccionGruposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciongrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciongrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAfeccionGruposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciongrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciongrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAfeccionGrupos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>> GetAfeccionGrupos(Query query)
        {
            return await GetAfeccionGrupos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>> GetAfeccionGrupos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AfeccionGrupos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfeccionGrupos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>>(response);
        }

        partial void OnCreateAfeccionGrupo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> CreateAfeccionGrupo(SgpaNew.Server.Models.Sgpa.AfeccionGrupo afeccionGrupo = default(SgpaNew.Server.Models.Sgpa.AfeccionGrupo))
        {
            var uri = new Uri(baseUri, $"AfeccionGrupos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afeccionGrupo), Encoding.UTF8, "application/json");

            OnCreateAfeccionGrupo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>(response);
        }

        partial void OnDeleteAfeccionGrupo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAfeccionGrupo(int codAfeccionGrupo = default(int))
        {
            var uri = new Uri(baseUri, $"AfeccionGrupos({codAfeccionGrupo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAfeccionGrupo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAfeccionGrupoByCodAfeccionGrupo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> GetAfeccionGrupoByCodAfeccionGrupo(string expand = default(string), int codAfeccionGrupo = default(int))
        {
            var uri = new Uri(baseUri, $"AfeccionGrupos({codAfeccionGrupo})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfeccionGrupoByCodAfeccionGrupo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>(response);
        }

        partial void OnUpdateAfeccionGrupo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAfeccionGrupo(int codAfeccionGrupo = default(int), SgpaNew.Server.Models.Sgpa.AfeccionGrupo afeccionGrupo = default(SgpaNew.Server.Models.Sgpa.AfeccionGrupo))
        {
            var uri = new Uri(baseUri, $"AfeccionGrupos({codAfeccionGrupo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", afeccionGrupo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afeccionGrupo), Encoding.UTF8, "application/json");

            OnUpdateAfeccionGrupo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAfeccionTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAfeccionTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAfeccionTipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfeccionTipo>> GetAfeccionTipos(Query query)
        {
            return await GetAfeccionTipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfeccionTipo>> GetAfeccionTipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AfeccionTipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfeccionTipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfeccionTipo>>(response);
        }

        partial void OnCreateAfeccionTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionTipo> CreateAfeccionTipo(SgpaNew.Server.Models.Sgpa.AfeccionTipo afeccionTipo = default(SgpaNew.Server.Models.Sgpa.AfeccionTipo))
        {
            var uri = new Uri(baseUri, $"AfeccionTipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afeccionTipo), Encoding.UTF8, "application/json");

            OnCreateAfeccionTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfeccionTipo>(response);
        }

        partial void OnDeleteAfeccionTipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAfeccionTipo(short codAfeccionTipo = default(short))
        {
            var uri = new Uri(baseUri, $"AfeccionTipos({codAfeccionTipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAfeccionTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAfeccionTipoByCodAfeccionTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionTipo> GetAfeccionTipoByCodAfeccionTipo(string expand = default(string), short codAfeccionTipo = default(short))
        {
            var uri = new Uri(baseUri, $"AfeccionTipos({codAfeccionTipo})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfeccionTipoByCodAfeccionTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfeccionTipo>(response);
        }

        partial void OnUpdateAfeccionTipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAfeccionTipo(short codAfeccionTipo = default(short), SgpaNew.Server.Models.Sgpa.AfeccionTipo afeccionTipo = default(SgpaNew.Server.Models.Sgpa.AfeccionTipo))
        {
            var uri = new Uri(baseUri, $"AfeccionTipos({codAfeccionTipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", afeccionTipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afeccionTipo), Encoding.UTF8, "application/json");

            OnUpdateAfeccionTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAfiliadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAfiliadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAfiliados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Afiliado>> GetAfiliados(Query query)
        {
            return await GetAfiliados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Afiliado>> GetAfiliados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Afiliados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfiliados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Afiliado>>(response);
        }

        partial void OnCreateAfiliado(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Afiliado> CreateAfiliado(SgpaNew.Server.Models.Sgpa.Afiliado afiliado = default(SgpaNew.Server.Models.Sgpa.Afiliado))
        {
            var uri = new Uri(baseUri, $"Afiliados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afiliado), Encoding.UTF8, "application/json");

            OnCreateAfiliado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Afiliado>(response);
        }

        partial void OnDeleteAfiliado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAfiliado(int ci = default(int))
        {
            var uri = new Uri(baseUri, $"Afiliados({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAfiliado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAfiliadoByCi(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Afiliado> GetAfiliadoByCi(string expand = default(string), int ci = default(int))
        {
            var uri = new Uri(baseUri, $"Afiliados({ci})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfiliadoByCi(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Afiliado>(response);
        }

        partial void OnUpdateAfiliado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAfiliado(int ci = default(int), SgpaNew.Server.Models.Sgpa.Afiliado afiliado = default(SgpaNew.Server.Models.Sgpa.Afiliado))
        {
            var uri = new Uri(baseUri, $"Afiliados({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", afiliado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afiliado), Encoding.UTF8, "application/json");

            OnUpdateAfiliado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAfiliadoApuntesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoapuntes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoapuntes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAfiliadoApuntesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoapuntes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoapuntes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAfiliadoApuntes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>> GetAfiliadoApuntes(Query query)
        {
            return await GetAfiliadoApuntes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>> GetAfiliadoApuntes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AfiliadoApuntes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfiliadoApuntes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>>(response);
        }

        partial void OnCreateAfiliadoApunte(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> CreateAfiliadoApunte(SgpaNew.Server.Models.Sgpa.AfiliadoApunte afiliadoApunte = default(SgpaNew.Server.Models.Sgpa.AfiliadoApunte))
        {
            var uri = new Uri(baseUri, $"AfiliadoApuntes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afiliadoApunte), Encoding.UTF8, "application/json");

            OnCreateAfiliadoApunte(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>(response);
        }

        partial void OnDeleteAfiliadoApunte(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAfiliadoApunte(int afiliadoApunteId = default(int))
        {
            var uri = new Uri(baseUri, $"AfiliadoApuntes({afiliadoApunteId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAfiliadoApunte(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAfiliadoApunteByAfiliadoApunteId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> GetAfiliadoApunteByAfiliadoApunteId(string expand = default(string), int afiliadoApunteId = default(int))
        {
            var uri = new Uri(baseUri, $"AfiliadoApuntes({afiliadoApunteId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfiliadoApunteByAfiliadoApunteId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>(response);
        }

        partial void OnUpdateAfiliadoApunte(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAfiliadoApunte(int afiliadoApunteId = default(int), SgpaNew.Server.Models.Sgpa.AfiliadoApunte afiliadoApunte = default(SgpaNew.Server.Models.Sgpa.AfiliadoApunte))
        {
            var uri = new Uri(baseUri, $"AfiliadoApuntes({afiliadoApunteId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", afiliadoApunte.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afiliadoApunte), Encoding.UTF8, "application/json");

            OnUpdateAfiliadoApunte(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAfiliadoEspecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAfiliadoEspecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAfiliadoEspecialidads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>> GetAfiliadoEspecialidads(Query query)
        {
            return await GetAfiliadoEspecialidads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>> GetAfiliadoEspecialidads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AfiliadoEspecialidads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfiliadoEspecialidads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>>(response);
        }

        partial void OnCreateAfiliadoEspecialidad(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> CreateAfiliadoEspecialidad(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad afiliadoEspecialidad = default(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad))
        {
            var uri = new Uri(baseUri, $"AfiliadoEspecialidads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afiliadoEspecialidad), Encoding.UTF8, "application/json");

            OnCreateAfiliadoEspecialidad(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>(response);
        }

        partial void OnDeleteAfiliadoEspecialidad(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAfiliadoEspecialidad(int afiliadoEspecialidadId = default(int))
        {
            var uri = new Uri(baseUri, $"AfiliadoEspecialidads({afiliadoEspecialidadId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAfiliadoEspecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAfiliadoEspecialidadByAfiliadoEspecialidadId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> GetAfiliadoEspecialidadByAfiliadoEspecialidadId(string expand = default(string), int afiliadoEspecialidadId = default(int))
        {
            var uri = new Uri(baseUri, $"AfiliadoEspecialidads({afiliadoEspecialidadId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAfiliadoEspecialidadByAfiliadoEspecialidadId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>(response);
        }

        partial void OnUpdateAfiliadoEspecialidad(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAfiliadoEspecialidad(int afiliadoEspecialidadId = default(int), SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad afiliadoEspecialidad = default(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad))
        {
            var uri = new Uri(baseUri, $"AfiliadoEspecialidads({afiliadoEspecialidadId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", afiliadoEspecialidad.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(afiliadoEspecialidad), Encoding.UTF8, "application/json");

            OnUpdateAfiliadoEspecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAporteTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/aportetipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/aportetipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAporteTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/aportetipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/aportetipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAporteTipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AporteTipo>> GetAporteTipos(Query query)
        {
            return await GetAporteTipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AporteTipo>> GetAporteTipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AporteTipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAporteTipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.AporteTipo>>(response);
        }

        partial void OnCreateAporteTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AporteTipo> CreateAporteTipo(SgpaNew.Server.Models.Sgpa.AporteTipo aporteTipo = default(SgpaNew.Server.Models.Sgpa.AporteTipo))
        {
            var uri = new Uri(baseUri, $"AporteTipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aporteTipo), Encoding.UTF8, "application/json");

            OnCreateAporteTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AporteTipo>(response);
        }

        partial void OnDeleteAporteTipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAporteTipo(string codAporteTipo = default(string))
        {
            var uri = new Uri(baseUri, $"AporteTipos('{Uri.EscapeDataString(codAporteTipo.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAporteTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAporteTipoByCodAporteTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.AporteTipo> GetAporteTipoByCodAporteTipo(string expand = default(string), string codAporteTipo = default(string))
        {
            var uri = new Uri(baseUri, $"AporteTipos('{Uri.EscapeDataString(codAporteTipo.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAporteTipoByCodAporteTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.AporteTipo>(response);
        }

        partial void OnUpdateAporteTipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAporteTipo(string codAporteTipo = default(string), SgpaNew.Server.Models.Sgpa.AporteTipo aporteTipo = default(SgpaNew.Server.Models.Sgpa.AporteTipo))
        {
            var uri = new Uri(baseUri, $"AporteTipos('{Uri.EscapeDataString(codAporteTipo.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", aporteTipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aporteTipo), Encoding.UTF8, "application/json");

            OnUpdateAporteTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBajaMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBajaMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBajaMotivos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.BajaMotivo>> GetBajaMotivos(Query query)
        {
            return await GetBajaMotivos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.BajaMotivo>> GetBajaMotivos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BajaMotivos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBajaMotivos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.BajaMotivo>>(response);
        }

        partial void OnCreateBajaMotivo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.BajaMotivo> CreateBajaMotivo(SgpaNew.Server.Models.Sgpa.BajaMotivo bajaMotivo = default(SgpaNew.Server.Models.Sgpa.BajaMotivo))
        {
            var uri = new Uri(baseUri, $"BajaMotivos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bajaMotivo), Encoding.UTF8, "application/json");

            OnCreateBajaMotivo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.BajaMotivo>(response);
        }

        partial void OnDeleteBajaMotivo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBajaMotivo(int codBajaMotivo = default(int))
        {
            var uri = new Uri(baseUri, $"BajaMotivos({codBajaMotivo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBajaMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBajaMotivoByCodBajaMotivo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.BajaMotivo> GetBajaMotivoByCodBajaMotivo(string expand = default(string), int codBajaMotivo = default(int))
        {
            var uri = new Uri(baseUri, $"BajaMotivos({codBajaMotivo})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBajaMotivoByCodBajaMotivo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.BajaMotivo>(response);
        }

        partial void OnUpdateBajaMotivo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBajaMotivo(int codBajaMotivo = default(int), SgpaNew.Server.Models.Sgpa.BajaMotivo bajaMotivo = default(SgpaNew.Server.Models.Sgpa.BajaMotivo))
        {
            var uri = new Uri(baseUri, $"BajaMotivos({codBajaMotivo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", bajaMotivo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bajaMotivo), Encoding.UTF8, "application/json");

            OnUpdateBajaMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBancosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBancosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBancos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Banco>> GetBancos(Query query)
        {
            return await GetBancos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Banco>> GetBancos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Bancos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBancos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Banco>>(response);
        }

        partial void OnCreateBanco(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Banco> CreateBanco(SgpaNew.Server.Models.Sgpa.Banco banco = default(SgpaNew.Server.Models.Sgpa.Banco))
        {
            var uri = new Uri(baseUri, $"Bancos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(banco), Encoding.UTF8, "application/json");

            OnCreateBanco(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Banco>(response);
        }

        partial void OnDeleteBanco(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBanco(int codBanco = default(int))
        {
            var uri = new Uri(baseUri, $"Bancos({codBanco})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBanco(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBancoByCodBanco(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Banco> GetBancoByCodBanco(string expand = default(string), int codBanco = default(int))
        {
            var uri = new Uri(baseUri, $"Bancos({codBanco})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBancoByCodBanco(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Banco>(response);
        }

        partial void OnUpdateBanco(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBanco(int codBanco = default(int), SgpaNew.Server.Models.Sgpa.Banco banco = default(SgpaNew.Server.Models.Sgpa.Banco))
        {
            var uri = new Uri(baseUri, $"Bancos({codBanco})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", banco.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(banco), Encoding.UTF8, "application/json");

            OnUpdateBanco(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCertificacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCertificacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCertificacions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Certificacion>> GetCertificacions(Query query)
        {
            return await GetCertificacions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Certificacion>> GetCertificacions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Certificacions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCertificacions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Certificacion>>(response);
        }

        partial void OnCreateCertificacion(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificacion> CreateCertificacion(SgpaNew.Server.Models.Sgpa.Certificacion certificacion = default(SgpaNew.Server.Models.Sgpa.Certificacion))
        {
            var uri = new Uri(baseUri, $"Certificacions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(certificacion), Encoding.UTF8, "application/json");

            OnCreateCertificacion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Certificacion>(response);
        }

        partial void OnDeleteCertificacion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCertificacion(int nroLlamado = default(int))
        {
            var uri = new Uri(baseUri, $"Certificacions({nroLlamado})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCertificacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCertificacionByNroLlamado(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificacion> GetCertificacionByNroLlamado(string expand = default(string), int nroLlamado = default(int))
        {
            var uri = new Uri(baseUri, $"Certificacions({nroLlamado})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCertificacionByNroLlamado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Certificacion>(response);
        }

        partial void OnUpdateCertificacion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCertificacion(int nroLlamado = default(int), SgpaNew.Server.Models.Sgpa.Certificacion certificacion = default(SgpaNew.Server.Models.Sgpa.Certificacion))
        {
            var uri = new Uri(baseUri, $"Certificacions({nroLlamado})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", certificacion.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(certificacion), Encoding.UTF8, "application/json");

            OnUpdateCertificacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCertificacionProrrogasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacionprorrogas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacionprorrogas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCertificacionProrrogasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacionprorrogas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacionprorrogas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCertificacionProrrogas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>> GetCertificacionProrrogas(Query query)
        {
            return await GetCertificacionProrrogas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>> GetCertificacionProrrogas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CertificacionProrrogas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCertificacionProrrogas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>>(response);
        }

        partial void OnCreateCertificacionProrroga(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> CreateCertificacionProrroga(SgpaNew.Server.Models.Sgpa.CertificacionProrroga certificacionProrroga = default(SgpaNew.Server.Models.Sgpa.CertificacionProrroga))
        {
            var uri = new Uri(baseUri, $"CertificacionProrrogas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(certificacionProrroga), Encoding.UTF8, "application/json");

            OnCreateCertificacionProrroga(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>(response);
        }

        partial void OnDeleteCertificacionProrroga(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCertificacionProrroga(int certificacionProrrogaId = default(int))
        {
            var uri = new Uri(baseUri, $"CertificacionProrrogas({certificacionProrrogaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCertificacionProrroga(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCertificacionProrrogaByCertificacionProrrogaId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> GetCertificacionProrrogaByCertificacionProrrogaId(string expand = default(string), int certificacionProrrogaId = default(int))
        {
            var uri = new Uri(baseUri, $"CertificacionProrrogas({certificacionProrrogaId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCertificacionProrrogaByCertificacionProrrogaId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>(response);
        }

        partial void OnUpdateCertificacionProrroga(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCertificacionProrroga(int certificacionProrrogaId = default(int), SgpaNew.Server.Models.Sgpa.CertificacionProrroga certificacionProrroga = default(SgpaNew.Server.Models.Sgpa.CertificacionProrroga))
        {
            var uri = new Uri(baseUri, $"CertificacionProrrogas({certificacionProrrogaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", certificacionProrroga.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(certificacionProrroga), Encoding.UTF8, "application/json");

            OnUpdateCertificacionProrroga(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCertificadorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCertificadorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCertificadors(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Certificador>> GetCertificadors(Query query)
        {
            return await GetCertificadors(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Certificador>> GetCertificadors(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Certificadors");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCertificadors(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Certificador>>(response);
        }

        partial void OnCreateCertificador(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificador> CreateCertificador(SgpaNew.Server.Models.Sgpa.Certificador certificador = default(SgpaNew.Server.Models.Sgpa.Certificador))
        {
            var uri = new Uri(baseUri, $"Certificadors");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(certificador), Encoding.UTF8, "application/json");

            OnCreateCertificador(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Certificador>(response);
        }

        partial void OnDeleteCertificador(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCertificador(short codCertificador = default(short))
        {
            var uri = new Uri(baseUri, $"Certificadors({codCertificador})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCertificador(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCertificadorByCodCertificador(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificador> GetCertificadorByCodCertificador(string expand = default(string), short codCertificador = default(short))
        {
            var uri = new Uri(baseUri, $"Certificadors({codCertificador})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCertificadorByCodCertificador(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Certificador>(response);
        }

        partial void OnUpdateCertificador(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCertificador(short codCertificador = default(short), SgpaNew.Server.Models.Sgpa.Certificador certificador = default(SgpaNew.Server.Models.Sgpa.Certificador))
        {
            var uri = new Uri(baseUri, $"Certificadors({codCertificador})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", certificador.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(certificador), Encoding.UTF8, "application/json");

            OnUpdateCertificador(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCtasbrousToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/ctasbrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/ctasbrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCtasbrousToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/ctasbrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/ctasbrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCtasbrous(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Ctasbrou>> GetCtasbrous(Query query)
        {
            return await GetCtasbrous(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Ctasbrou>> GetCtasbrous(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Ctasbrous");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCtasbrous(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Ctasbrou>>(response);
        }

        partial void OnCreateCtasbrou(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Ctasbrou> CreateCtasbrou(SgpaNew.Server.Models.Sgpa.Ctasbrou ctasbrou = default(SgpaNew.Server.Models.Sgpa.Ctasbrou))
        {
            var uri = new Uri(baseUri, $"Ctasbrous");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ctasbrou), Encoding.UTF8, "application/json");

            OnCreateCtasbrou(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Ctasbrou>(response);
        }

        partial void OnDeleteCtasbrou(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCtasbrou(int ci = default(int))
        {
            var uri = new Uri(baseUri, $"Ctasbrous({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCtasbrou(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCtasbrouByCi(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Ctasbrou> GetCtasbrouByCi(string expand = default(string), int ci = default(int))
        {
            var uri = new Uri(baseUri, $"Ctasbrous({ci})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCtasbrouByCi(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Ctasbrou>(response);
        }

        partial void OnUpdateCtasbrou(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCtasbrou(int ci = default(int), SgpaNew.Server.Models.Sgpa.Ctasbrou ctasbrou = default(SgpaNew.Server.Models.Sgpa.Ctasbrou))
        {
            var uri = new Uri(baseUri, $"Ctasbrous({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", ctasbrou.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ctasbrou), Encoding.UTF8, "application/json");

            OnUpdateCtasbrou(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCuentaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/cuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/cuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCuentaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/cuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/cuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCuenta(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Cuentum>> GetCuenta(Query query)
        {
            return await GetCuenta(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Cuentum>> GetCuenta(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Cuenta");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCuenta(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Cuentum>>(response);
        }

        public async System.Threading.Tasks.Task ExportDepartamentosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepartamentosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepartamentos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Departamento>> GetDepartamentos(Query query)
        {
            return await GetDepartamentos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Departamento>> GetDepartamentos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Departamentos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepartamentos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Departamento>>(response);
        }

        partial void OnCreateDepartamento(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Departamento> CreateDepartamento(SgpaNew.Server.Models.Sgpa.Departamento departamento = default(SgpaNew.Server.Models.Sgpa.Departamento))
        {
            var uri = new Uri(baseUri, $"Departamentos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(departamento), Encoding.UTF8, "application/json");

            OnCreateDepartamento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Departamento>(response);
        }

        partial void OnDeleteDepartamento(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepartamento(string codDepartamento = default(string))
        {
            var uri = new Uri(baseUri, $"Departamentos('{Uri.EscapeDataString(codDepartamento.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepartamento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepartamentoByCodDepartamento(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Departamento> GetDepartamentoByCodDepartamento(string expand = default(string), string codDepartamento = default(string))
        {
            var uri = new Uri(baseUri, $"Departamentos('{Uri.EscapeDataString(codDepartamento.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepartamentoByCodDepartamento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Departamento>(response);
        }

        partial void OnUpdateDepartamento(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepartamento(string codDepartamento = default(string), SgpaNew.Server.Models.Sgpa.Departamento departamento = default(SgpaNew.Server.Models.Sgpa.Departamento))
        {
            var uri = new Uri(baseUri, $"Departamentos('{Uri.EscapeDataString(codDepartamento.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", departamento.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(departamento), Encoding.UTF8, "application/json");

            OnUpdateDepartamento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDiscountsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/discounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/discounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDiscountsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/discounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/discounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDiscounts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Discount>> GetDiscounts(Query query)
        {
            return await GetDiscounts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Discount>> GetDiscounts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Discounts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDiscounts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Discount>>(response);
        }

        public async System.Threading.Tasks.Task ExportEmpresasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEmpresasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEmpresas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Empresa>> GetEmpresas(Query query)
        {
            return await GetEmpresas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Empresa>> GetEmpresas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Empresas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmpresas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Empresa>>(response);
        }

        partial void OnCreateEmpresa(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Empresa> CreateEmpresa(SgpaNew.Server.Models.Sgpa.Empresa empresa = default(SgpaNew.Server.Models.Sgpa.Empresa))
        {
            var uri = new Uri(baseUri, $"Empresas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(empresa), Encoding.UTF8, "application/json");

            OnCreateEmpresa(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Empresa>(response);
        }

        partial void OnDeleteEmpresa(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEmpresa(short codEmpresa = default(short))
        {
            var uri = new Uri(baseUri, $"Empresas({codEmpresa})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEmpresa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEmpresaByCodEmpresa(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Empresa> GetEmpresaByCodEmpresa(string expand = default(string), short codEmpresa = default(short))
        {
            var uri = new Uri(baseUri, $"Empresas({codEmpresa})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmpresaByCodEmpresa(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Empresa>(response);
        }

        partial void OnUpdateEmpresa(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEmpresa(short codEmpresa = default(short), SgpaNew.Server.Models.Sgpa.Empresa empresa = default(SgpaNew.Server.Models.Sgpa.Empresa))
        {
            var uri = new Uri(baseUri, $"Empresas({codEmpresa})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", empresa.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(empresa), Encoding.UTF8, "application/json");

            OnUpdateEmpresa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEmpresaPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEmpresaPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEmpresaPagos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.EmpresaPago>> GetEmpresaPagos(Query query)
        {
            return await GetEmpresaPagos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.EmpresaPago>> GetEmpresaPagos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"EmpresaPagos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmpresaPagos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.EmpresaPago>>(response);
        }

        partial void OnCreateEmpresaPago(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.EmpresaPago> CreateEmpresaPago(SgpaNew.Server.Models.Sgpa.EmpresaPago empresaPago = default(SgpaNew.Server.Models.Sgpa.EmpresaPago))
        {
            var uri = new Uri(baseUri, $"EmpresaPagos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(empresaPago), Encoding.UTF8, "application/json");

            OnCreateEmpresaPago(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.EmpresaPago>(response);
        }

        partial void OnDeleteEmpresaPago(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEmpresaPago(int empresaPagoId = default(int))
        {
            var uri = new Uri(baseUri, $"EmpresaPagos({empresaPagoId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEmpresaPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEmpresaPagoByEmpresaPagoId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.EmpresaPago> GetEmpresaPagoByEmpresaPagoId(string expand = default(string), int empresaPagoId = default(int))
        {
            var uri = new Uri(baseUri, $"EmpresaPagos({empresaPagoId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmpresaPagoByEmpresaPagoId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.EmpresaPago>(response);
        }

        partial void OnUpdateEmpresaPago(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEmpresaPago(int empresaPagoId = default(int), SgpaNew.Server.Models.Sgpa.EmpresaPago empresaPago = default(SgpaNew.Server.Models.Sgpa.EmpresaPago))
        {
            var uri = new Uri(baseUri, $"EmpresaPagos({empresaPagoId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", empresaPago.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(empresaPago), Encoding.UTF8, "application/json");

            OnUpdateEmpresaPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEspecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEspecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEspecialidads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Especialidad>> GetEspecialidads(Query query)
        {
            return await GetEspecialidads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Especialidad>> GetEspecialidads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Especialidads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEspecialidads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Especialidad>>(response);
        }

        partial void OnCreateEspecialidad(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Especialidad> CreateEspecialidad(SgpaNew.Server.Models.Sgpa.Especialidad especialidad = default(SgpaNew.Server.Models.Sgpa.Especialidad))
        {
            var uri = new Uri(baseUri, $"Especialidads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(especialidad), Encoding.UTF8, "application/json");

            OnCreateEspecialidad(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Especialidad>(response);
        }

        partial void OnDeleteEspecialidad(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEspecialidad(int codEspecialidad = default(int))
        {
            var uri = new Uri(baseUri, $"Especialidads({codEspecialidad})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEspecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEspecialidadByCodEspecialidad(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Especialidad> GetEspecialidadByCodEspecialidad(string expand = default(string), int codEspecialidad = default(int))
        {
            var uri = new Uri(baseUri, $"Especialidads({codEspecialidad})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEspecialidadByCodEspecialidad(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Especialidad>(response);
        }

        partial void OnUpdateEspecialidad(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEspecialidad(int codEspecialidad = default(int), SgpaNew.Server.Models.Sgpa.Especialidad especialidad = default(SgpaNew.Server.Models.Sgpa.Especialidad))
        {
            var uri = new Uri(baseUri, $"Especialidads({codEspecialidad})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", especialidad.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(especialidad), Encoding.UTF8, "application/json");

            OnUpdateEspecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportFormaPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/formapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/formapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFormaPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/formapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/formapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetFormaPagos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.FormaPago>> GetFormaPagos(Query query)
        {
            return await GetFormaPagos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.FormaPago>> GetFormaPagos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"FormaPagos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFormaPagos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.FormaPago>>(response);
        }

        partial void OnCreateFormaPago(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.FormaPago> CreateFormaPago(SgpaNew.Server.Models.Sgpa.FormaPago formaPago = default(SgpaNew.Server.Models.Sgpa.FormaPago))
        {
            var uri = new Uri(baseUri, $"FormaPagos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(formaPago), Encoding.UTF8, "application/json");

            OnCreateFormaPago(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.FormaPago>(response);
        }

        partial void OnDeleteFormaPago(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteFormaPago(short codFormaPago = default(short))
        {
            var uri = new Uri(baseUri, $"FormaPagos({codFormaPago})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFormaPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetFormaPagoByCodFormaPago(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.FormaPago> GetFormaPagoByCodFormaPago(string expand = default(string), short codFormaPago = default(short))
        {
            var uri = new Uri(baseUri, $"FormaPagos({codFormaPago})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFormaPagoByCodFormaPago(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.FormaPago>(response);
        }

        partial void OnUpdateFormaPago(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateFormaPago(short codFormaPago = default(short), SgpaNew.Server.Models.Sgpa.FormaPago formaPago = default(SgpaNew.Server.Models.Sgpa.FormaPago))
        {
            var uri = new Uri(baseUri, $"FormaPagos({codFormaPago})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", formaPago.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(formaPago), Encoding.UTF8, "application/json");

            OnUpdateFormaPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportFranjaIrpfsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/franjairpfs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/franjairpfs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFranjaIrpfsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/franjairpfs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/franjairpfs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetFranjaIrpfs(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.FranjaIrpf>> GetFranjaIrpfs(Query query)
        {
            return await GetFranjaIrpfs(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.FranjaIrpf>> GetFranjaIrpfs(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"FranjaIrpfs");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFranjaIrpfs(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.FranjaIrpf>>(response);
        }

        partial void OnCreateFranjaIrpf(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.FranjaIrpf> CreateFranjaIrpf(SgpaNew.Server.Models.Sgpa.FranjaIrpf franjaIrpf = default(SgpaNew.Server.Models.Sgpa.FranjaIrpf))
        {
            var uri = new Uri(baseUri, $"FranjaIrpfs");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(franjaIrpf), Encoding.UTF8, "application/json");

            OnCreateFranjaIrpf(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.FranjaIrpf>(response);
        }

        partial void OnDeleteFranjaIrpf(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteFranjaIrpf(double desde = default(double))
        {
            var uri = new Uri(baseUri, $"FranjaIrpfs({desde})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFranjaIrpf(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetFranjaIrpfByDesde(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.FranjaIrpf> GetFranjaIrpfByDesde(string expand = default(string), double desde = default(double))
        {
            var uri = new Uri(baseUri, $"FranjaIrpfs({desde})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFranjaIrpfByDesde(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.FranjaIrpf>(response);
        }

        partial void OnUpdateFranjaIrpf(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateFranjaIrpf(double desde = default(double), SgpaNew.Server.Models.Sgpa.FranjaIrpf franjaIrpf = default(SgpaNew.Server.Models.Sgpa.FranjaIrpf))
        {
            var uri = new Uri(baseUri, $"FranjaIrpfs({desde})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", franjaIrpf.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(franjaIrpf), Encoding.UTF8, "application/json");

            OnUpdateFranjaIrpf(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportImpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportImpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetImps(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Imp>> GetImps(Query query)
        {
            return await GetImps(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Imp>> GetImps(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Imps");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetImps(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Imp>>(response);
        }

        public async System.Threading.Tasks.Task ExportImponiblesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportImponiblesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetImponibles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Imponible>> GetImponibles(Query query)
        {
            return await GetImponibles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Imponible>> GetImponibles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Imponibles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetImponibles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Imponible>>(response);
        }

        partial void OnCreateImponible(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Imponible> CreateImponible(SgpaNew.Server.Models.Sgpa.Imponible imponible = default(SgpaNew.Server.Models.Sgpa.Imponible))
        {
            var uri = new Uri(baseUri, $"Imponibles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(imponible), Encoding.UTF8, "application/json");

            OnCreateImponible(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Imponible>(response);
        }

        partial void OnDeleteImponible(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteImponible(int imponibleId = default(int))
        {
            var uri = new Uri(baseUri, $"Imponibles({imponibleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteImponible(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetImponibleByImponibleId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Imponible> GetImponibleByImponibleId(string expand = default(string), int imponibleId = default(int))
        {
            var uri = new Uri(baseUri, $"Imponibles({imponibleId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetImponibleByImponibleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Imponible>(response);
        }

        partial void OnUpdateImponible(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateImponible(int imponibleId = default(int), SgpaNew.Server.Models.Sgpa.Imponible imponible = default(SgpaNew.Server.Models.Sgpa.Imponible))
        {
            var uri = new Uri(baseUri, $"Imponibles({imponibleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", imponible.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(imponible), Encoding.UTF8, "application/json");

            OnUpdateImponible(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportInformeEstadisticosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/informeestadisticos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/informeestadisticos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportInformeEstadisticosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/informeestadisticos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/informeestadisticos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetInformeEstadisticos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.InformeEstadistico>> GetInformeEstadisticos(Query query)
        {
            return await GetInformeEstadisticos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.InformeEstadistico>> GetInformeEstadisticos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"InformeEstadisticos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetInformeEstadisticos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.InformeEstadistico>>(response);
        }

        partial void OnCreateInformeEstadistico(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.InformeEstadistico> CreateInformeEstadistico(SgpaNew.Server.Models.Sgpa.InformeEstadistico informeEstadistico = default(SgpaNew.Server.Models.Sgpa.InformeEstadistico))
        {
            var uri = new Uri(baseUri, $"InformeEstadisticos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(informeEstadistico), Encoding.UTF8, "application/json");

            OnCreateInformeEstadistico(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.InformeEstadistico>(response);
        }

        partial void OnDeleteInformeEstadistico(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteInformeEstadistico(int idRpt = default(int))
        {
            var uri = new Uri(baseUri, $"InformeEstadisticos({idRpt})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteInformeEstadistico(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetInformeEstadisticoByIdRpt(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.InformeEstadistico> GetInformeEstadisticoByIdRpt(string expand = default(string), int idRpt = default(int))
        {
            var uri = new Uri(baseUri, $"InformeEstadisticos({idRpt})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetInformeEstadisticoByIdRpt(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.InformeEstadistico>(response);
        }

        partial void OnUpdateInformeEstadistico(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateInformeEstadistico(int idRpt = default(int), SgpaNew.Server.Models.Sgpa.InformeEstadistico informeEstadistico = default(SgpaNew.Server.Models.Sgpa.InformeEstadistico))
        {
            var uri = new Uri(baseUri, $"InformeEstadisticos({idRpt})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", informeEstadistico.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(informeEstadistico), Encoding.UTF8, "application/json");

            OnUpdateInformeEstadistico(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportLiquidacionBpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/liquidacionbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/liquidacionbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLiquidacionBpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/liquidacionbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/liquidacionbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetLiquidacionBps(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.LiquidacionBp>> GetLiquidacionBps(Query query)
        {
            return await GetLiquidacionBps(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.LiquidacionBp>> GetLiquidacionBps(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"LiquidacionBps");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLiquidacionBps(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.LiquidacionBp>>(response);
        }

        partial void OnCreateLiquidacionBp(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.LiquidacionBp> CreateLiquidacionBp(SgpaNew.Server.Models.Sgpa.LiquidacionBp liquidacionBp = default(SgpaNew.Server.Models.Sgpa.LiquidacionBp))
        {
            var uri = new Uri(baseUri, $"LiquidacionBps");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(liquidacionBp), Encoding.UTF8, "application/json");

            OnCreateLiquidacionBp(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.LiquidacionBp>(response);
        }

        partial void OnDeleteLiquidacionBp(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteLiquidacionBp(int liquidacionBpsid = default(int))
        {
            var uri = new Uri(baseUri, $"LiquidacionBps({liquidacionBpsid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLiquidacionBp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetLiquidacionBpByLiquidacionBpsid(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.LiquidacionBp> GetLiquidacionBpByLiquidacionBpsid(string expand = default(string), int liquidacionBpsid = default(int))
        {
            var uri = new Uri(baseUri, $"LiquidacionBps({liquidacionBpsid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLiquidacionBpByLiquidacionBpsid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.LiquidacionBp>(response);
        }

        partial void OnUpdateLiquidacionBp(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateLiquidacionBp(int liquidacionBpsid = default(int), SgpaNew.Server.Models.Sgpa.LiquidacionBp liquidacionBp = default(SgpaNew.Server.Models.Sgpa.LiquidacionBp))
        {
            var uri = new Uri(baseUri, $"LiquidacionBps({liquidacionBpsid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", liquidacionBp.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(liquidacionBp), Encoding.UTF8, "application/json");

            OnUpdateLiquidacionBp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMaeFunsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/maefuns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/maefuns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMaeFunsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/maefuns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/maefuns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMaeFuns(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.MaeFun>> GetMaeFuns(Query query)
        {
            return await GetMaeFuns(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.MaeFun>> GetMaeFuns(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MaeFuns");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMaeFuns(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.MaeFun>>(response);
        }

        partial void OnCreateMaeFun(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.MaeFun> CreateMaeFun(SgpaNew.Server.Models.Sgpa.MaeFun maeFun = default(SgpaNew.Server.Models.Sgpa.MaeFun))
        {
            var uri = new Uri(baseUri, $"MaeFuns");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(maeFun), Encoding.UTF8, "application/json");

            OnCreateMaeFun(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.MaeFun>(response);
        }

        partial void OnDeleteMaeFun(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMaeFun(int nroFun = default(int))
        {
            var uri = new Uri(baseUri, $"MaeFuns({nroFun})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMaeFun(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMaeFunByNroFun(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.MaeFun> GetMaeFunByNroFun(string expand = default(string), int nroFun = default(int))
        {
            var uri = new Uri(baseUri, $"MaeFuns({nroFun})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMaeFunByNroFun(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.MaeFun>(response);
        }

        partial void OnUpdateMaeFun(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMaeFun(int nroFun = default(int), SgpaNew.Server.Models.Sgpa.MaeFun maeFun = default(SgpaNew.Server.Models.Sgpa.MaeFun))
        {
            var uri = new Uri(baseUri, $"MaeFuns({nroFun})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", maeFun.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(maeFun), Encoding.UTF8, "application/json");

            OnUpdateMaeFun(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMonedaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/moneda/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/moneda/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMonedaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/moneda/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/moneda/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMoneda(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Monedum>> GetMoneda(Query query)
        {
            return await GetMoneda(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Monedum>> GetMoneda(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Moneda");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMoneda(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Monedum>>(response);
        }

        partial void OnCreateMonedum(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Monedum> CreateMonedum(SgpaNew.Server.Models.Sgpa.Monedum monedum = default(SgpaNew.Server.Models.Sgpa.Monedum))
        {
            var uri = new Uri(baseUri, $"Moneda");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(monedum), Encoding.UTF8, "application/json");

            OnCreateMonedum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Monedum>(response);
        }

        partial void OnDeleteMonedum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMonedum(string moneda1 = default(string))
        {
            var uri = new Uri(baseUri, $"Moneda('{Uri.EscapeDataString(moneda1.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMonedum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMonedumByMoneda1(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Monedum> GetMonedumByMoneda1(string expand = default(string), string moneda1 = default(string))
        {
            var uri = new Uri(baseUri, $"Moneda('{Uri.EscapeDataString(moneda1.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMonedumByMoneda1(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Monedum>(response);
        }

        partial void OnUpdateMonedum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMonedum(string moneda1 = default(string), SgpaNew.Server.Models.Sgpa.Monedum monedum = default(SgpaNew.Server.Models.Sgpa.Monedum))
        {
            var uri = new Uri(baseUri, $"Moneda('{Uri.EscapeDataString(moneda1.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", monedum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(monedum), Encoding.UTF8, "application/json");

            OnUpdateMonedum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMutualistaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/mutualista/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/mutualista/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMutualistaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/mutualista/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/mutualista/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMutualista(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Mutualistum>> GetMutualista(Query query)
        {
            return await GetMutualista(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Mutualistum>> GetMutualista(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Mutualista");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMutualista(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Mutualistum>>(response);
        }

        partial void OnCreateMutualistum(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Mutualistum> CreateMutualistum(SgpaNew.Server.Models.Sgpa.Mutualistum mutualistum = default(SgpaNew.Server.Models.Sgpa.Mutualistum))
        {
            var uri = new Uri(baseUri, $"Mutualista");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mutualistum), Encoding.UTF8, "application/json");

            OnCreateMutualistum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Mutualistum>(response);
        }

        partial void OnDeleteMutualistum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMutualistum(short codMutualista = default(short))
        {
            var uri = new Uri(baseUri, $"Mutualista({codMutualista})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMutualistum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMutualistumByCodMutualista(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Mutualistum> GetMutualistumByCodMutualista(string expand = default(string), short codMutualista = default(short))
        {
            var uri = new Uri(baseUri, $"Mutualista({codMutualista})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMutualistumByCodMutualista(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Mutualistum>(response);
        }

        partial void OnUpdateMutualistum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMutualistum(short codMutualista = default(short), SgpaNew.Server.Models.Sgpa.Mutualistum mutualistum = default(SgpaNew.Server.Models.Sgpa.Mutualistum))
        {
            var uri = new Uri(baseUri, $"Mutualista({codMutualista})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", mutualistum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mutualistum), Encoding.UTF8, "application/json");

            OnUpdateMutualistum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportNoCargadoHlsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/nocargadohls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/nocargadohls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportNoCargadoHlsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/nocargadohls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/nocargadohls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetNoCargadoHls(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.NoCargadoHl>> GetNoCargadoHls(Query query)
        {
            return await GetNoCargadoHls(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.NoCargadoHl>> GetNoCargadoHls(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"NoCargadoHls");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetNoCargadoHls(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.NoCargadoHl>>(response);
        }

        partial void OnCreateNoCargadoHl(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.NoCargadoHl> CreateNoCargadoHl(SgpaNew.Server.Models.Sgpa.NoCargadoHl noCargadoHl = default(SgpaNew.Server.Models.Sgpa.NoCargadoHl))
        {
            var uri = new Uri(baseUri, $"NoCargadoHls");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(noCargadoHl), Encoding.UTF8, "application/json");

            OnCreateNoCargadoHl(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.NoCargadoHl>(response);
        }

        partial void OnDeleteNoCargadoHl(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteNoCargadoHl(int noCargadoHlid = default(int))
        {
            var uri = new Uri(baseUri, $"NoCargadoHls({noCargadoHlid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteNoCargadoHl(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetNoCargadoHlByNoCargadoHlid(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.NoCargadoHl> GetNoCargadoHlByNoCargadoHlid(string expand = default(string), int noCargadoHlid = default(int))
        {
            var uri = new Uri(baseUri, $"NoCargadoHls({noCargadoHlid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetNoCargadoHlByNoCargadoHlid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.NoCargadoHl>(response);
        }

        partial void OnUpdateNoCargadoHl(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateNoCargadoHl(int noCargadoHlid = default(int), SgpaNew.Server.Models.Sgpa.NoCargadoHl noCargadoHl = default(SgpaNew.Server.Models.Sgpa.NoCargadoHl))
        {
            var uri = new Uri(baseUri, $"NoCargadoHls({noCargadoHlid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", noCargadoHl.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(noCargadoHl), Encoding.UTF8, "application/json");

            OnUpdateNoCargadoHl(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportParametrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportParametrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetParametros(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Parametro>> GetParametros(Query query)
        {
            return await GetParametros(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Parametro>> GetParametros(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Parametros");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetParametros(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Parametro>>(response);
        }

        public async System.Threading.Tasks.Task ExportPatologiaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/patologia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/patologia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPatologiaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/patologia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/patologia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetPatologia(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Patologium>> GetPatologia(Query query)
        {
            return await GetPatologia(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Patologium>> GetPatologia(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Patologia");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPatologia(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Patologium>>(response);
        }

        partial void OnCreatePatologium(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Patologium> CreatePatologium(SgpaNew.Server.Models.Sgpa.Patologium patologium = default(SgpaNew.Server.Models.Sgpa.Patologium))
        {
            var uri = new Uri(baseUri, $"Patologia");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(patologium), Encoding.UTF8, "application/json");

            OnCreatePatologium(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Patologium>(response);
        }

        partial void OnDeletePatologium(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeletePatologium(int codPatologia = default(int))
        {
            var uri = new Uri(baseUri, $"Patologia({codPatologia})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeletePatologium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetPatologiumByCodPatologia(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Patologium> GetPatologiumByCodPatologia(string expand = default(string), int codPatologia = default(int))
        {
            var uri = new Uri(baseUri, $"Patologia({codPatologia})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPatologiumByCodPatologia(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Patologium>(response);
        }

        partial void OnUpdatePatologium(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdatePatologium(int codPatologia = default(int), SgpaNew.Server.Models.Sgpa.Patologium patologium = default(SgpaNew.Server.Models.Sgpa.Patologium))
        {
            var uri = new Uri(baseUri, $"Patologia({codPatologia})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", patologium.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(patologium), Encoding.UTF8, "application/json");

            OnUpdatePatologium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportPrestacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPrestacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetPrestacions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Prestacion>> GetPrestacions(Query query)
        {
            return await GetPrestacions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Prestacion>> GetPrestacions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Prestacions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPrestacions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Prestacion>>(response);
        }

        partial void OnCreatePrestacion(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Prestacion> CreatePrestacion(SgpaNew.Server.Models.Sgpa.Prestacion prestacion = default(SgpaNew.Server.Models.Sgpa.Prestacion))
        {
            var uri = new Uri(baseUri, $"Prestacions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(prestacion), Encoding.UTF8, "application/json");

            OnCreatePrestacion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Prestacion>(response);
        }

        partial void OnDeletePrestacion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeletePrestacion(int prestacionId = default(int))
        {
            var uri = new Uri(baseUri, $"Prestacions({prestacionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeletePrestacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetPrestacionByPrestacionId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Prestacion> GetPrestacionByPrestacionId(string expand = default(string), int prestacionId = default(int))
        {
            var uri = new Uri(baseUri, $"Prestacions({prestacionId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPrestacionByPrestacionId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Prestacion>(response);
        }

        partial void OnUpdatePrestacion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdatePrestacion(int prestacionId = default(int), SgpaNew.Server.Models.Sgpa.Prestacion prestacion = default(SgpaNew.Server.Models.Sgpa.Prestacion))
        {
            var uri = new Uri(baseUri, $"Prestacions({prestacionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", prestacion.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(prestacion), Encoding.UTF8, "application/json");

            OnUpdatePrestacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportPrestacionTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestaciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestaciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPrestacionTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestaciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestaciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetPrestacionTipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.PrestacionTipo>> GetPrestacionTipos(Query query)
        {
            return await GetPrestacionTipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.PrestacionTipo>> GetPrestacionTipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"PrestacionTipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPrestacionTipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.PrestacionTipo>>(response);
        }

        partial void OnCreatePrestacionTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.PrestacionTipo> CreatePrestacionTipo(SgpaNew.Server.Models.Sgpa.PrestacionTipo prestacionTipo = default(SgpaNew.Server.Models.Sgpa.PrestacionTipo))
        {
            var uri = new Uri(baseUri, $"PrestacionTipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(prestacionTipo), Encoding.UTF8, "application/json");

            OnCreatePrestacionTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.PrestacionTipo>(response);
        }

        partial void OnDeletePrestacionTipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeletePrestacionTipo(short codPrestacionTipo = default(short))
        {
            var uri = new Uri(baseUri, $"PrestacionTipos({codPrestacionTipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeletePrestacionTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetPrestacionTipoByCodPrestacionTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.PrestacionTipo> GetPrestacionTipoByCodPrestacionTipo(string expand = default(string), short codPrestacionTipo = default(short))
        {
            var uri = new Uri(baseUri, $"PrestacionTipos({codPrestacionTipo})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPrestacionTipoByCodPrestacionTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.PrestacionTipo>(response);
        }

        partial void OnUpdatePrestacionTipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdatePrestacionTipo(short codPrestacionTipo = default(short), SgpaNew.Server.Models.Sgpa.PrestacionTipo prestacionTipo = default(SgpaNew.Server.Models.Sgpa.PrestacionTipo))
        {
            var uri = new Uri(baseUri, $"PrestacionTipos({codPrestacionTipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", prestacionTipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(prestacionTipo), Encoding.UTF8, "application/json");

            OnUpdatePrestacionTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportPrimaFallecimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/primafallecimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/primafallecimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPrimaFallecimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/primafallecimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/primafallecimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetPrimaFallecimientos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>> GetPrimaFallecimientos(Query query)
        {
            return await GetPrimaFallecimientos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>> GetPrimaFallecimientos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"PrimaFallecimientos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPrimaFallecimientos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>>(response);
        }

        partial void OnCreatePrimaFallecimiento(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> CreatePrimaFallecimiento(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento primaFallecimiento = default(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento))
        {
            var uri = new Uri(baseUri, $"PrimaFallecimientos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(primaFallecimiento), Encoding.UTF8, "application/json");

            OnCreatePrimaFallecimiento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>(response);
        }

        partial void OnDeletePrimaFallecimiento(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeletePrimaFallecimiento(int ci = default(int))
        {
            var uri = new Uri(baseUri, $"PrimaFallecimientos({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeletePrimaFallecimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetPrimaFallecimientoByCi(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> GetPrimaFallecimientoByCi(string expand = default(string), int ci = default(int))
        {
            var uri = new Uri(baseUri, $"PrimaFallecimientos({ci})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPrimaFallecimientoByCi(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>(response);
        }

        partial void OnUpdatePrimaFallecimiento(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdatePrimaFallecimiento(int ci = default(int), SgpaNew.Server.Models.Sgpa.PrimaFallecimiento primaFallecimiento = default(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento))
        {
            var uri = new Uri(baseUri, $"PrimaFallecimientos({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", primaFallecimiento.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(primaFallecimiento), Encoding.UTF8, "application/json");

            OnUpdatePrimaFallecimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRecetaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/receta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/receta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRecetaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/receta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/receta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetReceta(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Recetum>> GetReceta(Query query)
        {
            return await GetReceta(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Recetum>> GetReceta(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Receta");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetReceta(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Recetum>>(response);
        }

        partial void OnCreateRecetum(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Recetum> CreateRecetum(SgpaNew.Server.Models.Sgpa.Recetum recetum = default(SgpaNew.Server.Models.Sgpa.Recetum))
        {
            var uri = new Uri(baseUri, $"Receta");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(recetum), Encoding.UTF8, "application/json");

            OnCreateRecetum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Recetum>(response);
        }

        partial void OnDeleteRecetum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRecetum(int recetaId = default(int))
        {
            var uri = new Uri(baseUri, $"Receta({recetaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRecetum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRecetumByRecetaId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Recetum> GetRecetumByRecetaId(string expand = default(string), int recetaId = default(int))
        {
            var uri = new Uri(baseUri, $"Receta({recetaId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRecetumByRecetaId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Recetum>(response);
        }

        partial void OnUpdateRecetum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRecetum(int recetaId = default(int), SgpaNew.Server.Models.Sgpa.Recetum recetum = default(SgpaNew.Server.Models.Sgpa.Recetum))
        {
            var uri = new Uri(baseUri, $"Receta({recetaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", recetum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(recetum), Encoding.UTF8, "application/json");

            OnUpdateRecetum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRecetaDistanciaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/recetadistancia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/recetadistancia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRecetaDistanciaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/recetadistancia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/recetadistancia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRecetaDistancia(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RecetaDistancium>> GetRecetaDistancia(Query query)
        {
            return await GetRecetaDistancia(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RecetaDistancium>> GetRecetaDistancia(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RecetaDistancia");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRecetaDistancia(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RecetaDistancium>>(response);
        }

        partial void OnCreateRecetaDistancium(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.RecetaDistancium> CreateRecetaDistancium(SgpaNew.Server.Models.Sgpa.RecetaDistancium recetaDistancium = default(SgpaNew.Server.Models.Sgpa.RecetaDistancium))
        {
            var uri = new Uri(baseUri, $"RecetaDistancia");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(recetaDistancium), Encoding.UTF8, "application/json");

            OnCreateRecetaDistancium(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.RecetaDistancium>(response);
        }

        partial void OnDeleteRecetaDistancium(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRecetaDistancium(string codRecetaDistancia = default(string))
        {
            var uri = new Uri(baseUri, $"RecetaDistancia('{Uri.EscapeDataString(codRecetaDistancia.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRecetaDistancium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRecetaDistanciumByCodRecetaDistancia(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.RecetaDistancium> GetRecetaDistanciumByCodRecetaDistancia(string expand = default(string), string codRecetaDistancia = default(string))
        {
            var uri = new Uri(baseUri, $"RecetaDistancia('{Uri.EscapeDataString(codRecetaDistancia.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRecetaDistanciumByCodRecetaDistancia(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.RecetaDistancium>(response);
        }

        partial void OnUpdateRecetaDistancium(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRecetaDistancium(string codRecetaDistancia = default(string), SgpaNew.Server.Models.Sgpa.RecetaDistancium recetaDistancium = default(SgpaNew.Server.Models.Sgpa.RecetaDistancium))
        {
            var uri = new Uri(baseUri, $"RecetaDistancia('{Uri.EscapeDataString(codRecetaDistancia.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", recetaDistancium.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(recetaDistancium), Encoding.UTF8, "application/json");

            OnUpdateRecetaDistancium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegimenAportesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenaportes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenaportes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegimenAportesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenaportes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenaportes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegimenAportes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RegimenAporte>> GetRegimenAportes(Query query)
        {
            return await GetRegimenAportes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RegimenAporte>> GetRegimenAportes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RegimenAportes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegimenAportes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RegimenAporte>>(response);
        }

        partial void OnCreateRegimenAporte(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenAporte> CreateRegimenAporte(SgpaNew.Server.Models.Sgpa.RegimenAporte regimenAporte = default(SgpaNew.Server.Models.Sgpa.RegimenAporte))
        {
            var uri = new Uri(baseUri, $"RegimenAportes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regimenAporte), Encoding.UTF8, "application/json");

            OnCreateRegimenAporte(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.RegimenAporte>(response);
        }

        partial void OnDeleteRegimenAporte(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegimenAporte(short codRegimenAporte = default(short))
        {
            var uri = new Uri(baseUri, $"RegimenAportes({codRegimenAporte})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegimenAporte(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegimenAporteByCodRegimenAporte(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenAporte> GetRegimenAporteByCodRegimenAporte(string expand = default(string), short codRegimenAporte = default(short))
        {
            var uri = new Uri(baseUri, $"RegimenAportes({codRegimenAporte})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegimenAporteByCodRegimenAporte(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.RegimenAporte>(response);
        }

        partial void OnUpdateRegimenAporte(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegimenAporte(short codRegimenAporte = default(short), SgpaNew.Server.Models.Sgpa.RegimenAporte regimenAporte = default(SgpaNew.Server.Models.Sgpa.RegimenAporte))
        {
            var uri = new Uri(baseUri, $"RegimenAportes({codRegimenAporte})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", regimenAporte.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regimenAporte), Encoding.UTF8, "application/json");

            OnUpdateRegimenAporte(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegimenJubilatoriosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenjubilatorios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenjubilatorios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegimenJubilatoriosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenjubilatorios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenjubilatorios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegimenJubilatorios(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>> GetRegimenJubilatorios(Query query)
        {
            return await GetRegimenJubilatorios(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>> GetRegimenJubilatorios(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RegimenJubilatorios");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegimenJubilatorios(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>>(response);
        }

        partial void OnCreateRegimenJubilatorio(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> CreateRegimenJubilatorio(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio regimenJubilatorio = default(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio))
        {
            var uri = new Uri(baseUri, $"RegimenJubilatorios");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regimenJubilatorio), Encoding.UTF8, "application/json");

            OnCreateRegimenJubilatorio(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>(response);
        }

        partial void OnDeleteRegimenJubilatorio(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegimenJubilatorio(byte codRegimenJubilatorio = default(byte))
        {
            var uri = new Uri(baseUri, $"RegimenJubilatorios({codRegimenJubilatorio})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegimenJubilatorio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegimenJubilatorioByCodRegimenJubilatorio(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> GetRegimenJubilatorioByCodRegimenJubilatorio(string expand = default(string), byte codRegimenJubilatorio = default(byte))
        {
            var uri = new Uri(baseUri, $"RegimenJubilatorios({codRegimenJubilatorio})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegimenJubilatorioByCodRegimenJubilatorio(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>(response);
        }

        partial void OnUpdateRegimenJubilatorio(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegimenJubilatorio(byte codRegimenJubilatorio = default(byte), SgpaNew.Server.Models.Sgpa.RegimenJubilatorio regimenJubilatorio = default(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio))
        {
            var uri = new Uri(baseUri, $"RegimenJubilatorios({codRegimenJubilatorio})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", regimenJubilatorio.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regimenJubilatorio), Encoding.UTF8, "application/json");

            OnUpdateRegimenJubilatorio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportReintegroMutualsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/reintegromutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/reintegromutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportReintegroMutualsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/reintegromutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/reintegromutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetReintegroMutuals(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.ReintegroMutual>> GetReintegroMutuals(Query query)
        {
            return await GetReintegroMutuals(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.ReintegroMutual>> GetReintegroMutuals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ReintegroMutuals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetReintegroMutuals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.ReintegroMutual>>(response);
        }

        partial void OnCreateReintegroMutual(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.ReintegroMutual> CreateReintegroMutual(SgpaNew.Server.Models.Sgpa.ReintegroMutual reintegroMutual = default(SgpaNew.Server.Models.Sgpa.ReintegroMutual))
        {
            var uri = new Uri(baseUri, $"ReintegroMutuals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(reintegroMutual), Encoding.UTF8, "application/json");

            OnCreateReintegroMutual(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.ReintegroMutual>(response);
        }

        partial void OnDeleteReintegroMutual(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteReintegroMutual(int reintegroMutualId = default(int))
        {
            var uri = new Uri(baseUri, $"ReintegroMutuals({reintegroMutualId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteReintegroMutual(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetReintegroMutualByReintegroMutualId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.ReintegroMutual> GetReintegroMutualByReintegroMutualId(string expand = default(string), int reintegroMutualId = default(int))
        {
            var uri = new Uri(baseUri, $"ReintegroMutuals({reintegroMutualId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetReintegroMutualByReintegroMutualId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.ReintegroMutual>(response);
        }

        partial void OnUpdateReintegroMutual(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateReintegroMutual(int reintegroMutualId = default(int), SgpaNew.Server.Models.Sgpa.ReintegroMutual reintegroMutual = default(SgpaNew.Server.Models.Sgpa.ReintegroMutual))
        {
            var uri = new Uri(baseUri, $"ReintegroMutuals({reintegroMutualId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", reintegroMutual.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(reintegroMutual), Encoding.UTF8, "application/json");

            OnUpdateReintegroMutual(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalidaTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/salidatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/salidatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalidaTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/salidatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/salidatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalidaTipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SalidaTipo>> GetSalidaTipos(Query query)
        {
            return await GetSalidaTipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SalidaTipo>> GetSalidaTipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalidaTipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalidaTipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SalidaTipo>>(response);
        }

        partial void OnCreateSalidaTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SalidaTipo> CreateSalidaTipo(SgpaNew.Server.Models.Sgpa.SalidaTipo salidaTipo = default(SgpaNew.Server.Models.Sgpa.SalidaTipo))
        {
            var uri = new Uri(baseUri, $"SalidaTipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salidaTipo), Encoding.UTF8, "application/json");

            OnCreateSalidaTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SalidaTipo>(response);
        }

        partial void OnDeleteSalidaTipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalidaTipo(short codSalidaTipo = default(short))
        {
            var uri = new Uri(baseUri, $"SalidaTipos({codSalidaTipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalidaTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalidaTipoByCodSalidaTipo(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SalidaTipo> GetSalidaTipoByCodSalidaTipo(string expand = default(string), short codSalidaTipo = default(short))
        {
            var uri = new Uri(baseUri, $"SalidaTipos({codSalidaTipo})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalidaTipoByCodSalidaTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SalidaTipo>(response);
        }

        partial void OnUpdateSalidaTipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalidaTipo(short codSalidaTipo = default(short), SgpaNew.Server.Models.Sgpa.SalidaTipo salidaTipo = default(SgpaNew.Server.Models.Sgpa.SalidaTipo))
        {
            var uri = new Uri(baseUri, $"SalidaTipos({codSalidaTipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", salidaTipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salidaTipo), Encoding.UTF8, "application/json");

            OnUpdateSalidaTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSeleccionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/seleccions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/seleccions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSeleccionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/seleccions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/seleccions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSeleccions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Seleccion>> GetSeleccions(Query query)
        {
            return await GetSeleccions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Seleccion>> GetSeleccions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Seleccions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSeleccions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Seleccion>>(response);
        }

        partial void OnCreateSeleccion(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Seleccion> CreateSeleccion(SgpaNew.Server.Models.Sgpa.Seleccion seleccion = default(SgpaNew.Server.Models.Sgpa.Seleccion))
        {
            var uri = new Uri(baseUri, $"Seleccions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(seleccion), Encoding.UTF8, "application/json");

            OnCreateSeleccion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Seleccion>(response);
        }

        partial void OnDeleteSeleccion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSeleccion(int seleccionId = default(int))
        {
            var uri = new Uri(baseUri, $"Seleccions({seleccionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSeleccion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSeleccionBySeleccionId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Seleccion> GetSeleccionBySeleccionId(string expand = default(string), int seleccionId = default(int))
        {
            var uri = new Uri(baseUri, $"Seleccions({seleccionId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSeleccionBySeleccionId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Seleccion>(response);
        }

        partial void OnUpdateSeleccion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSeleccion(int seleccionId = default(int), SgpaNew.Server.Models.Sgpa.Seleccion seleccion = default(SgpaNew.Server.Models.Sgpa.Seleccion))
        {
            var uri = new Uri(baseUri, $"Seleccions({seleccionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", seleccion.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(seleccion), Encoding.UTF8, "application/json");

            OnUpdateSeleccion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSituacionMutualsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionmutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionmutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSituacionMutualsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionmutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionmutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSituacionMutuals(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SituacionMutual>> GetSituacionMutuals(Query query)
        {
            return await GetSituacionMutuals(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SituacionMutual>> GetSituacionMutuals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SituacionMutuals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSituacionMutuals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SituacionMutual>>(response);
        }

        partial void OnCreateSituacionMutual(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionMutual> CreateSituacionMutual(SgpaNew.Server.Models.Sgpa.SituacionMutual situacionMutual = default(SgpaNew.Server.Models.Sgpa.SituacionMutual))
        {
            var uri = new Uri(baseUri, $"SituacionMutuals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(situacionMutual), Encoding.UTF8, "application/json");

            OnCreateSituacionMutual(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SituacionMutual>(response);
        }

        partial void OnDeleteSituacionMutual(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSituacionMutual(string codSituacionMutual = default(string))
        {
            var uri = new Uri(baseUri, $"SituacionMutuals('{Uri.EscapeDataString(codSituacionMutual.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSituacionMutual(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSituacionMutualByCodSituacionMutual(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionMutual> GetSituacionMutualByCodSituacionMutual(string expand = default(string), string codSituacionMutual = default(string))
        {
            var uri = new Uri(baseUri, $"SituacionMutuals('{Uri.EscapeDataString(codSituacionMutual.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSituacionMutualByCodSituacionMutual(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SituacionMutual>(response);
        }

        partial void OnUpdateSituacionMutual(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSituacionMutual(string codSituacionMutual = default(string), SgpaNew.Server.Models.Sgpa.SituacionMutual situacionMutual = default(SgpaNew.Server.Models.Sgpa.SituacionMutual))
        {
            var uri = new Uri(baseUri, $"SituacionMutuals('{Uri.EscapeDataString(codSituacionMutual.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", situacionMutual.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(situacionMutual), Encoding.UTF8, "application/json");

            OnUpdateSituacionMutual(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSituacionPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSituacionPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSituacionPagos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SituacionPago>> GetSituacionPagos(Query query)
        {
            return await GetSituacionPagos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SituacionPago>> GetSituacionPagos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SituacionPagos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSituacionPagos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SituacionPago>>(response);
        }

        partial void OnCreateSituacionPago(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionPago> CreateSituacionPago(SgpaNew.Server.Models.Sgpa.SituacionPago situacionPago = default(SgpaNew.Server.Models.Sgpa.SituacionPago))
        {
            var uri = new Uri(baseUri, $"SituacionPagos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(situacionPago), Encoding.UTF8, "application/json");

            OnCreateSituacionPago(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SituacionPago>(response);
        }

        partial void OnDeleteSituacionPago(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSituacionPago(short codSituacionPago = default(short))
        {
            var uri = new Uri(baseUri, $"SituacionPagos({codSituacionPago})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSituacionPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSituacionPagoByCodSituacionPago(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionPago> GetSituacionPagoByCodSituacionPago(string expand = default(string), short codSituacionPago = default(short))
        {
            var uri = new Uri(baseUri, $"SituacionPagos({codSituacionPago})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSituacionPagoByCodSituacionPago(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SituacionPago>(response);
        }

        partial void OnUpdateSituacionPago(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSituacionPago(short codSituacionPago = default(short), SgpaNew.Server.Models.Sgpa.SituacionPago situacionPago = default(SgpaNew.Server.Models.Sgpa.SituacionPago))
        {
            var uri = new Uri(baseUri, $"SituacionPagos({codSituacionPago})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", situacionPago.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(situacionPago), Encoding.UTF8, "application/json");

            OnUpdateSituacionPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidioCabezalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioCabezalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioCabezals(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>> GetSubsidioCabezals(Query query)
        {
            return await GetSubsidioCabezals(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>> GetSubsidioCabezals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioCabezals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>>(response);
        }

        partial void OnCreateSubsidioCabezal(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> CreateSubsidioCabezal(SgpaNew.Server.Models.Sgpa.SubsidioCabezal subsidioCabezal = default(SgpaNew.Server.Models.Sgpa.SubsidioCabezal))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioCabezal), Encoding.UTF8, "application/json");

            OnCreateSubsidioCabezal(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>(response);
        }

        partial void OnDeleteSubsidioCabezal(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidioCabezal(int idSubsidio = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezals({idSubsidio})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidioCabezal(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidioCabezalByIdSubsidio(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> GetSubsidioCabezalByIdSubsidio(string expand = default(string), int idSubsidio = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezals({idSubsidio})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioCabezalByIdSubsidio(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>(response);
        }

        partial void OnUpdateSubsidioCabezal(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidioCabezal(int idSubsidio = default(int), SgpaNew.Server.Models.Sgpa.SubsidioCabezal subsidioCabezal = default(SgpaNew.Server.Models.Sgpa.SubsidioCabezal))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezals({idSubsidio})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidioCabezal.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioCabezal), Encoding.UTF8, "application/json");

            OnUpdateSubsidioCabezal(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidiocabezalBpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidiocabezalBpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidiocabezalBps(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>> GetSubsidiocabezalBps(Query query)
        {
            return await GetSubsidiocabezalBps(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>> GetSubsidiocabezalBps(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidiocabezalBps");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidiocabezalBps(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>>(response);
        }

        partial void OnCreateSubsidiocabezalBp(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> CreateSubsidiocabezalBp(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp subsidiocabezalBp = default(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp))
        {
            var uri = new Uri(baseUri, $"SubsidiocabezalBps");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidiocabezalBp), Encoding.UTF8, "application/json");

            OnCreateSubsidiocabezalBp(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>(response);
        }

        partial void OnDeleteSubsidiocabezalBp(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidiocabezalBp(int idSubsidio = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidiocabezalBps({idSubsidio})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidiocabezalBp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidiocabezalBpByIdSubsidio(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> GetSubsidiocabezalBpByIdSubsidio(string expand = default(string), int idSubsidio = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidiocabezalBps({idSubsidio})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidiocabezalBpByIdSubsidio(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>(response);
        }

        partial void OnUpdateSubsidiocabezalBp(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidiocabezalBp(int idSubsidio = default(int), SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp subsidiocabezalBp = default(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp))
        {
            var uri = new Uri(baseUri, $"SubsidiocabezalBps({idSubsidio})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidiocabezalBp.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidiocabezalBp), Encoding.UTF8, "application/json");

            OnUpdateSubsidiocabezalBp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidioCabezalEmpresasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioCabezalEmpresasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioCabezalEmpresas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>> GetSubsidioCabezalEmpresas(Query query)
        {
            return await GetSubsidioCabezalEmpresas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>> GetSubsidioCabezalEmpresas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezalEmpresas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioCabezalEmpresas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>>(response);
        }

        partial void OnCreateSubsidioCabezalEmpresa(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> CreateSubsidioCabezalEmpresa(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa subsidioCabezalEmpresa = default(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezalEmpresas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioCabezalEmpresa), Encoding.UTF8, "application/json");

            OnCreateSubsidioCabezalEmpresa(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>(response);
        }

        partial void OnDeleteSubsidioCabezalEmpresa(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidioCabezalEmpresa(int subsidioCabezalempresaId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezalEmpresas({subsidioCabezalempresaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidioCabezalEmpresa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> GetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(string expand = default(string), int subsidioCabezalempresaId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezalEmpresas({subsidioCabezalempresaId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>(response);
        }

        partial void OnUpdateSubsidioCabezalEmpresa(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidioCabezalEmpresa(int subsidioCabezalempresaId = default(int), SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa subsidioCabezalEmpresa = default(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa))
        {
            var uri = new Uri(baseUri, $"SubsidioCabezalEmpresas({subsidioCabezalempresaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidioCabezalEmpresa.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioCabezalEmpresa), Encoding.UTF8, "application/json");

            OnUpdateSubsidioCabezalEmpresa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidioEnfermedadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioenfermedads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioenfermedads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioEnfermedadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioenfermedads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioenfermedads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioEnfermedads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>> GetSubsidioEnfermedads(Query query)
        {
            return await GetSubsidioEnfermedads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>> GetSubsidioEnfermedads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioEnfermedads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioEnfermedads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>>(response);
        }

        partial void OnCreateSubsidioEnfermedad(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> CreateSubsidioEnfermedad(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad subsidioEnfermedad = default(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad))
        {
            var uri = new Uri(baseUri, $"SubsidioEnfermedads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioEnfermedad), Encoding.UTF8, "application/json");

            OnCreateSubsidioEnfermedad(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>(response);
        }

        partial void OnDeleteSubsidioEnfermedad(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidioEnfermedad(int subsidioEnfermedadId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioEnfermedads({subsidioEnfermedadId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidioEnfermedad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidioEnfermedadBySubsidioEnfermedadId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> GetSubsidioEnfermedadBySubsidioEnfermedadId(string expand = default(string), int subsidioEnfermedadId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioEnfermedads({subsidioEnfermedadId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioEnfermedadBySubsidioEnfermedadId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>(response);
        }

        partial void OnUpdateSubsidioEnfermedad(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidioEnfermedad(int subsidioEnfermedadId = default(int), SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad subsidioEnfermedad = default(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad))
        {
            var uri = new Uri(baseUri, $"SubsidioEnfermedads({subsidioEnfermedadId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidioEnfermedad.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioEnfermedad), Encoding.UTF8, "application/json");

            OnUpdateSubsidioEnfermedad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidioImponiblesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioimponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioimponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioImponiblesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioimponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioimponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioImponibles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioImponible>> GetSubsidioImponibles(Query query)
        {
            return await GetSubsidioImponibles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioImponible>> GetSubsidioImponibles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioImponibles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioImponibles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioImponible>>(response);
        }

        public async System.Threading.Tasks.Task ExportSubsidioItemsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioItemsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioItems(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItem>> GetSubsidioItems(Query query)
        {
            return await GetSubsidioItems(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItem>> GetSubsidioItems(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioItems");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioItems(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItem>>(response);
        }

        partial void OnCreateSubsidioItem(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItem> CreateSubsidioItem(SgpaNew.Server.Models.Sgpa.SubsidioItem subsidioItem = default(SgpaNew.Server.Models.Sgpa.SubsidioItem))
        {
            var uri = new Uri(baseUri, $"SubsidioItems");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioItem), Encoding.UTF8, "application/json");

            OnCreateSubsidioItem(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioItem>(response);
        }

        partial void OnDeleteSubsidioItem(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidioItem(int subsidioItemId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioItems({subsidioItemId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidioItem(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidioItemBySubsidioItemId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItem> GetSubsidioItemBySubsidioItemId(string expand = default(string), int subsidioItemId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioItems({subsidioItemId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioItemBySubsidioItemId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioItem>(response);
        }

        partial void OnUpdateSubsidioItem(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidioItem(int subsidioItemId = default(int), SgpaNew.Server.Models.Sgpa.SubsidioItem subsidioItem = default(SgpaNew.Server.Models.Sgpa.SubsidioItem))
        {
            var uri = new Uri(baseUri, $"SubsidioItems({subsidioItemId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidioItem.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioItem), Encoding.UTF8, "application/json");

            OnUpdateSubsidioItem(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidioItemCodsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcods/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcods/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioItemCodsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcods/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcods/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioItemCods(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>> GetSubsidioItemCods(Query query)
        {
            return await GetSubsidioItemCods(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>> GetSubsidioItemCods(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioItemCods");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioItemCods(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>>(response);
        }

        partial void OnCreateSubsidioItemCod(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> CreateSubsidioItemCod(SgpaNew.Server.Models.Sgpa.SubsidioItemCod subsidioItemCod = default(SgpaNew.Server.Models.Sgpa.SubsidioItemCod))
        {
            var uri = new Uri(baseUri, $"SubsidioItemCods");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioItemCod), Encoding.UTF8, "application/json");

            OnCreateSubsidioItemCod(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>(response);
        }

        partial void OnDeleteSubsidioItemCod(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidioItemCod(short codSubsidioItemCod = default(short))
        {
            var uri = new Uri(baseUri, $"SubsidioItemCods({codSubsidioItemCod})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidioItemCod(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidioItemCodByCodSubsidioItemCod(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> GetSubsidioItemCodByCodSubsidioItemCod(string expand = default(string), short codSubsidioItemCod = default(short))
        {
            var uri = new Uri(baseUri, $"SubsidioItemCods({codSubsidioItemCod})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioItemCodByCodSubsidioItemCod(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>(response);
        }

        partial void OnUpdateSubsidioItemCod(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidioItemCod(short codSubsidioItemCod = default(short), SgpaNew.Server.Models.Sgpa.SubsidioItemCod subsidioItemCod = default(SgpaNew.Server.Models.Sgpa.SubsidioItemCod))
        {
            var uri = new Uri(baseUri, $"SubsidioItemCods({codSubsidioItemCod})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidioItemCod.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioItemCod), Encoding.UTF8, "application/json");

            OnUpdateSubsidioItemCod(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidioitemcodAfiliadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcodafiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcodafiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioitemcodAfiliadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcodafiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcodafiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioitemcodAfiliados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>> GetSubsidioitemcodAfiliados(Query query)
        {
            return await GetSubsidioitemcodAfiliados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>> GetSubsidioitemcodAfiliados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioitemcodAfiliados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioitemcodAfiliados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>>(response);
        }

        partial void OnCreateSubsidioitemcodAfiliado(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> CreateSubsidioitemcodAfiliado(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado subsidioitemcodAfiliado = default(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado))
        {
            var uri = new Uri(baseUri, $"SubsidioitemcodAfiliados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioitemcodAfiliado), Encoding.UTF8, "application/json");

            OnCreateSubsidioitemcodAfiliado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>(response);
        }

        partial void OnDeleteSubsidioitemcodAfiliado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidioitemcodAfiliado(int subItmCodAfiId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioitemcodAfiliados({subItmCodAfiId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidioitemcodAfiliado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidioitemcodAfiliadoBySubItmCodAfiId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> GetSubsidioitemcodAfiliadoBySubItmCodAfiId(string expand = default(string), int subItmCodAfiId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioitemcodAfiliados({subItmCodAfiId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioitemcodAfiliadoBySubItmCodAfiId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>(response);
        }

        partial void OnUpdateSubsidioitemcodAfiliado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidioitemcodAfiliado(int subItmCodAfiId = default(int), SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado subsidioitemcodAfiliado = default(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado))
        {
            var uri = new Uri(baseUri, $"SubsidioitemcodAfiliados({subItmCodAfiId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidioitemcodAfiliado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioitemcodAfiliado), Encoding.UTF8, "application/json");

            OnUpdateSubsidioitemcodAfiliado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubsidioItemEmpresasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubsidioItemEmpresasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubsidioItemEmpresas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>> GetSubsidioItemEmpresas(Query query)
        {
            return await GetSubsidioItemEmpresas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>> GetSubsidioItemEmpresas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubsidioItemEmpresas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioItemEmpresas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>>(response);
        }

        partial void OnCreateSubsidioItemEmpresa(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> CreateSubsidioItemEmpresa(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa subsidioItemEmpresa = default(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa))
        {
            var uri = new Uri(baseUri, $"SubsidioItemEmpresas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioItemEmpresa), Encoding.UTF8, "application/json");

            OnCreateSubsidioItemEmpresa(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>(response);
        }

        partial void OnDeleteSubsidioItemEmpresa(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubsidioItemEmpresa(int subsidioItemEmpresaId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioItemEmpresas({subsidioItemEmpresaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubsidioItemEmpresa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubsidioItemEmpresaBySubsidioItemEmpresaId(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> GetSubsidioItemEmpresaBySubsidioItemEmpresaId(string expand = default(string), int subsidioItemEmpresaId = default(int))
        {
            var uri = new Uri(baseUri, $"SubsidioItemEmpresas({subsidioItemEmpresaId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubsidioItemEmpresaBySubsidioItemEmpresaId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>(response);
        }

        partial void OnUpdateSubsidioItemEmpresa(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubsidioItemEmpresa(int subsidioItemEmpresaId = default(int), SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa subsidioItemEmpresa = default(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa))
        {
            var uri = new Uri(baseUri, $"SubsidioItemEmpresas({subsidioItemEmpresaId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subsidioItemEmpresa.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subsidioItemEmpresa), Encoding.UTF8, "application/json");

            OnUpdateSubsidioItemEmpresa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTrabajasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/trabajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/trabajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTrabajasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/trabajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/trabajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTrabajas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Trabaja>> GetTrabajas(Query query)
        {
            return await GetTrabajas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Trabaja>> GetTrabajas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Trabajas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTrabajas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.Trabaja>>(response);
        }

        partial void OnCreateTrabaja(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Trabaja> CreateTrabaja(SgpaNew.Server.Models.Sgpa.Trabaja trabaja = default(SgpaNew.Server.Models.Sgpa.Trabaja))
        {
            var uri = new Uri(baseUri, $"Trabajas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(trabaja), Encoding.UTF8, "application/json");

            OnCreateTrabaja(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Trabaja>(response);
        }

        partial void OnDeleteTrabaja(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTrabaja(int idTrabaja = default(int))
        {
            var uri = new Uri(baseUri, $"Trabajas({idTrabaja})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTrabaja(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTrabajaByIdTrabaja(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.Trabaja> GetTrabajaByIdTrabaja(string expand = default(string), int idTrabaja = default(int))
        {
            var uri = new Uri(baseUri, $"Trabajas({idTrabaja})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTrabajaByIdTrabaja(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.Trabaja>(response);
        }

        partial void OnUpdateTrabaja(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTrabaja(int idTrabaja = default(int), SgpaNew.Server.Models.Sgpa.Trabaja trabaja = default(SgpaNew.Server.Models.Sgpa.Trabaja))
        {
            var uri = new Uri(baseUri, $"Trabajas({idTrabaja})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", trabaja.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(trabaja), Encoding.UTF8, "application/json");

            OnUpdateTrabaja(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXUsrParamsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/xusrparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/xusrparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXUsrParamsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/xusrparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/xusrparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXUsrParams(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.XUsrParam>> GetXUsrParams(Query query)
        {
            return await GetXUsrParams(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.XUsrParam>> GetXUsrParams(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XUsrParams");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXUsrParams(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SgpaNew.Server.Models.Sgpa.XUsrParam>>(response);
        }

        partial void OnCreateXUsrParam(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.XUsrParam> CreateXUsrParam(SgpaNew.Server.Models.Sgpa.XUsrParam xusrParam = default(SgpaNew.Server.Models.Sgpa.XUsrParam))
        {
            var uri = new Uri(baseUri, $"XUsrParams");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xusrParam), Encoding.UTF8, "application/json");

            OnCreateXUsrParam(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.XUsrParam>(response);
        }

        partial void OnDeleteXUsrParam(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXUsrParam(int idUsuario = default(int))
        {
            var uri = new Uri(baseUri, $"XUsrParams({idUsuario})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXUsrParam(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXUsrParamByIdUsuario(HttpRequestMessage requestMessage);

        public async Task<SgpaNew.Server.Models.Sgpa.XUsrParam> GetXUsrParamByIdUsuario(string expand = default(string), int idUsuario = default(int))
        {
            var uri = new Uri(baseUri, $"XUsrParams({idUsuario})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXUsrParamByIdUsuario(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SgpaNew.Server.Models.Sgpa.XUsrParam>(response);
        }

        partial void OnUpdateXUsrParam(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXUsrParam(int idUsuario = default(int), SgpaNew.Server.Models.Sgpa.XUsrParam xusrParam = default(SgpaNew.Server.Models.Sgpa.XUsrParam))
        {
            var uri = new Uri(baseUri, $"XUsrParams({idUsuario})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xusrParam.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xusrParam), Encoding.UTF8, "application/json");

            OnUpdateXUsrParam(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}