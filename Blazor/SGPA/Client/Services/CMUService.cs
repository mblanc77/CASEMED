
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

namespace SGPA.Client
{
    public partial class CMUService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public CMUService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/CMU/");
        }


        public async System.Threading.Tasks.Task ExportActaConsejosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/actaconsejos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/actaconsejos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportActaConsejosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/actaconsejos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/actaconsejos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetActaConsejos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ActaConsejo>> GetActaConsejos(Query query)
        {
            return await GetActaConsejos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ActaConsejo>> GetActaConsejos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ActaConsejos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetActaConsejos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ActaConsejo>>(response);
        }

        partial void OnCreateActaConsejo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ActaConsejo> CreateActaConsejo(SGPA.Server.Models.CMU.ActaConsejo actaConsejo = default(SGPA.Server.Models.CMU.ActaConsejo))
        {
            var uri = new Uri(baseUri, $"ActaConsejos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(actaConsejo), Encoding.UTF8, "application/json");

            OnCreateActaConsejo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ActaConsejo>(response);
        }

        partial void OnDeleteActaConsejo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteActaConsejo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ActaConsejos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteActaConsejo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetActaConsejoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ActaConsejo> GetActaConsejoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ActaConsejos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetActaConsejoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ActaConsejo>(response);
        }

        partial void OnUpdateActaConsejo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateActaConsejo(int id = default(int), SGPA.Server.Models.CMU.ActaConsejo actaConsejo = default(SGPA.Server.Models.CMU.ActaConsejo))
        {
            var uri = new Uri(baseUri, $"ActaConsejos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", actaConsejo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(actaConsejo), Encoding.UTF8, "application/json");

            OnUpdateActaConsejo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAgenteCobranzasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAgenteCobranzasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAgenteCobranzas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranza>> GetAgenteCobranzas(Query query)
        {
            return await GetAgenteCobranzas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranza>> GetAgenteCobranzas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteCobranzas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranza>>(response);
        }

        partial void OnCreateAgenteCobranza(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranza> CreateAgenteCobranza(SGPA.Server.Models.CMU.AgenteCobranza agenteCobranza = default(SGPA.Server.Models.CMU.AgenteCobranza))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteCobranza), Encoding.UTF8, "application/json");

            OnCreateAgenteCobranza(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteCobranza>(response);
        }

        partial void OnDeleteAgenteCobranza(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAgenteCobranza(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAgenteCobranza(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAgenteCobranzaById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranza> GetAgenteCobranzaById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzas({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteCobranzaById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteCobranza>(response);
        }

        partial void OnUpdateAgenteCobranza(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAgenteCobranza(int id = default(int), SGPA.Server.Models.CMU.AgenteCobranza agenteCobranza = default(SGPA.Server.Models.CMU.AgenteCobranza))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", agenteCobranza.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteCobranza), Encoding.UTF8, "application/json");

            OnUpdateAgenteCobranza(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAgenteCobranzaDebitosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzadebitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzadebitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAgenteCobranzaDebitosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzadebitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzadebitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAgenteCobranzaDebitos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranzaDebito>> GetAgenteCobranzaDebitos(Query query)
        {
            return await GetAgenteCobranzaDebitos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranzaDebito>> GetAgenteCobranzaDebitos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaDebitos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteCobranzaDebitos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranzaDebito>>(response);
        }

        partial void OnCreateAgenteCobranzaDebito(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaDebito> CreateAgenteCobranzaDebito(SGPA.Server.Models.CMU.AgenteCobranzaDebito agenteCobranzaDebito = default(SGPA.Server.Models.CMU.AgenteCobranzaDebito))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaDebitos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteCobranzaDebito), Encoding.UTF8, "application/json");

            OnCreateAgenteCobranzaDebito(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteCobranzaDebito>(response);
        }

        partial void OnDeleteAgenteCobranzaDebito(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAgenteCobranzaDebito(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaDebitos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAgenteCobranzaDebito(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAgenteCobranzaDebitoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaDebito> GetAgenteCobranzaDebitoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaDebitos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteCobranzaDebitoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteCobranzaDebito>(response);
        }

        partial void OnUpdateAgenteCobranzaDebito(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAgenteCobranzaDebito(int id = default(int), SGPA.Server.Models.CMU.AgenteCobranzaDebito agenteCobranzaDebito = default(SGPA.Server.Models.CMU.AgenteCobranzaDebito))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaDebitos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", agenteCobranzaDebito.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteCobranzaDebito), Encoding.UTF8, "application/json");

            OnUpdateAgenteCobranzaDebito(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAgenteCobranzaTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAgenteCobranzaTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAgenteCobranzaTipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranzaTipo>> GetAgenteCobranzaTipos(Query query)
        {
            return await GetAgenteCobranzaTipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranzaTipo>> GetAgenteCobranzaTipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaTipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteCobranzaTipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteCobranzaTipo>>(response);
        }

        partial void OnCreateAgenteCobranzaTipo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaTipo> CreateAgenteCobranzaTipo(SGPA.Server.Models.CMU.AgenteCobranzaTipo agenteCobranzaTipo = default(SGPA.Server.Models.CMU.AgenteCobranzaTipo))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaTipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteCobranzaTipo), Encoding.UTF8, "application/json");

            OnCreateAgenteCobranzaTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteCobranzaTipo>(response);
        }

        partial void OnDeleteAgenteCobranzaTipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAgenteCobranzaTipo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaTipos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAgenteCobranzaTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAgenteCobranzaTipoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaTipo> GetAgenteCobranzaTipoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaTipos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteCobranzaTipoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteCobranzaTipo>(response);
        }

        partial void OnUpdateAgenteCobranzaTipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAgenteCobranzaTipo(int id = default(int), SGPA.Server.Models.CMU.AgenteCobranzaTipo agenteCobranzaTipo = default(SGPA.Server.Models.CMU.AgenteCobranzaTipo))
        {
            var uri = new Uri(baseUri, $"AgenteCobranzaTipos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", agenteCobranzaTipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteCobranzaTipo), Encoding.UTF8, "application/json");

            OnUpdateAgenteCobranzaTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAgenteGruposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentegrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentegrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAgenteGruposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentegrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentegrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAgenteGrupos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteGrupo>> GetAgenteGrupos(Query query)
        {
            return await GetAgenteGrupos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteGrupo>> GetAgenteGrupos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AgenteGrupos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteGrupos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AgenteGrupo>>(response);
        }

        partial void OnCreateAgenteGrupo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteGrupo> CreateAgenteGrupo(SGPA.Server.Models.CMU.AgenteGrupo agenteGrupo = default(SGPA.Server.Models.CMU.AgenteGrupo))
        {
            var uri = new Uri(baseUri, $"AgenteGrupos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteGrupo), Encoding.UTF8, "application/json");

            OnCreateAgenteGrupo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteGrupo>(response);
        }

        partial void OnDeleteAgenteGrupo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAgenteGrupo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteGrupos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAgenteGrupo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAgenteGrupoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AgenteGrupo> GetAgenteGrupoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AgenteGrupos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAgenteGrupoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AgenteGrupo>(response);
        }

        partial void OnUpdateAgenteGrupo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAgenteGrupo(int id = default(int), SGPA.Server.Models.CMU.AgenteGrupo agenteGrupo = default(SGPA.Server.Models.CMU.AgenteGrupo))
        {
            var uri = new Uri(baseUri, $"AgenteGrupos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", agenteGrupo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(agenteGrupo), Encoding.UTF8, "application/json");

            OnUpdateAgenteGrupo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAjusteDetallesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajustedetalles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajustedetalles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAjusteDetallesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajustedetalles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajustedetalles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAjusteDetalles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AjusteDetalle>> GetAjusteDetalles(Query query)
        {
            return await GetAjusteDetalles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AjusteDetalle>> GetAjusteDetalles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AjusteDetalles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAjusteDetalles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AjusteDetalle>>(response);
        }

        partial void OnCreateAjusteDetalle(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AjusteDetalle> CreateAjusteDetalle(SGPA.Server.Models.CMU.AjusteDetalle ajusteDetalle = default(SGPA.Server.Models.CMU.AjusteDetalle))
        {
            var uri = new Uri(baseUri, $"AjusteDetalles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ajusteDetalle), Encoding.UTF8, "application/json");

            OnCreateAjusteDetalle(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AjusteDetalle>(response);
        }

        partial void OnDeleteAjusteDetalle(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAjusteDetalle(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AjusteDetalles({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAjusteDetalle(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAjusteDetalleById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AjusteDetalle> GetAjusteDetalleById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AjusteDetalles({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAjusteDetalleById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AjusteDetalle>(response);
        }

        partial void OnUpdateAjusteDetalle(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAjusteDetalle(int id = default(int), SGPA.Server.Models.CMU.AjusteDetalle ajusteDetalle = default(SGPA.Server.Models.CMU.AjusteDetalle))
        {
            var uri = new Uri(baseUri, $"AjusteDetalles({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", ajusteDetalle.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ajusteDetalle), Encoding.UTF8, "application/json");

            OnUpdateAjusteDetalle(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAjusteRetroactivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajusteretroactivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajusteretroactivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAjusteRetroactivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajusteretroactivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajusteretroactivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAjusteRetroactivos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AjusteRetroactivo>> GetAjusteRetroactivos(Query query)
        {
            return await GetAjusteRetroactivos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AjusteRetroactivo>> GetAjusteRetroactivos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AjusteRetroactivos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAjusteRetroactivos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AjusteRetroactivo>>(response);
        }

        partial void OnCreateAjusteRetroactivo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AjusteRetroactivo> CreateAjusteRetroactivo(SGPA.Server.Models.CMU.AjusteRetroactivo ajusteRetroactivo = default(SGPA.Server.Models.CMU.AjusteRetroactivo))
        {
            var uri = new Uri(baseUri, $"AjusteRetroactivos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ajusteRetroactivo), Encoding.UTF8, "application/json");

            OnCreateAjusteRetroactivo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AjusteRetroactivo>(response);
        }

        partial void OnDeleteAjusteRetroactivo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAjusteRetroactivo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AjusteRetroactivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAjusteRetroactivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAjusteRetroactivoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AjusteRetroactivo> GetAjusteRetroactivoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AjusteRetroactivos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAjusteRetroactivoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AjusteRetroactivo>(response);
        }

        partial void OnUpdateAjusteRetroactivo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAjusteRetroactivo(int id = default(int), SGPA.Server.Models.CMU.AjusteRetroactivo ajusteRetroactivo = default(SGPA.Server.Models.CMU.AjusteRetroactivo))
        {
            var uri = new Uri(baseUri, $"AjusteRetroactivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", ajusteRetroactivo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ajusteRetroactivo), Encoding.UTF8, "application/json");

            OnUpdateAjusteRetroactivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAnalysesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/analyses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/analyses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAnalysesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/analyses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/analyses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAnalyses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Analysis>> GetAnalyses(Query query)
        {
            return await GetAnalyses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Analysis>> GetAnalyses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Analyses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAnalyses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Analysis>>(response);
        }

        partial void OnCreateAnalysis(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Analysis> CreateAnalysis(SGPA.Server.Models.CMU.Analysis analysis = default(SGPA.Server.Models.CMU.Analysis))
        {
            var uri = new Uri(baseUri, $"Analyses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(analysis), Encoding.UTF8, "application/json");

            OnCreateAnalysis(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Analysis>(response);
        }

        partial void OnDeleteAnalysis(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAnalysis(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"Analyses({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAnalysis(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAnalysisByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Analysis> GetAnalysisByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"Analyses({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAnalysisByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Analysis>(response);
        }

        partial void OnUpdateAnalysis(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAnalysis(Guid oid = default(Guid), SGPA.Server.Models.CMU.Analysis analysis = default(SGPA.Server.Models.CMU.Analysis))
        {
            var uri = new Uri(baseUri, $"Analyses({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", analysis.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(analysis), Encoding.UTF8, "application/json");

            OnUpdateAnalysis(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAreaContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/areacontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/areacontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAreaContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/areacontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/areacontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAreaContactos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AreaContacto>> GetAreaContactos(Query query)
        {
            return await GetAreaContactos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AreaContacto>> GetAreaContactos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AreaContactos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAreaContactos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AreaContacto>>(response);
        }

        partial void OnCreateAreaContacto(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AreaContacto> CreateAreaContacto(SGPA.Server.Models.CMU.AreaContacto areaContacto = default(SGPA.Server.Models.CMU.AreaContacto))
        {
            var uri = new Uri(baseUri, $"AreaContactos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(areaContacto), Encoding.UTF8, "application/json");

            OnCreateAreaContacto(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AreaContacto>(response);
        }

        partial void OnDeleteAreaContacto(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAreaContacto(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AreaContactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAreaContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAreaContactoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AreaContacto> GetAreaContactoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AreaContactos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAreaContactoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AreaContacto>(response);
        }

        partial void OnUpdateAreaContacto(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAreaContacto(int id = default(int), SGPA.Server.Models.CMU.AreaContacto areaContacto = default(SGPA.Server.Models.CMU.AreaContacto))
        {
            var uri = new Uri(baseUri, $"AreaContactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", areaContacto.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(areaContacto), Encoding.UTF8, "application/json");

            OnUpdateAreaContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAuditDataItemPersistentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditdataitempersistents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditdataitempersistents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAuditDataItemPersistentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditdataitempersistents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditdataitempersistents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAuditDataItemPersistents(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AuditDataItemPersistent>> GetAuditDataItemPersistents(Query query)
        {
            return await GetAuditDataItemPersistents(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AuditDataItemPersistent>> GetAuditDataItemPersistents(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AuditDataItemPersistents");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAuditDataItemPersistents(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AuditDataItemPersistent>>(response);
        }

        partial void OnCreateAuditDataItemPersistent(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AuditDataItemPersistent> CreateAuditDataItemPersistent(SGPA.Server.Models.CMU.AuditDataItemPersistent auditDataItemPersistent = default(SGPA.Server.Models.CMU.AuditDataItemPersistent))
        {
            var uri = new Uri(baseUri, $"AuditDataItemPersistents");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(auditDataItemPersistent), Encoding.UTF8, "application/json");

            OnCreateAuditDataItemPersistent(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AuditDataItemPersistent>(response);
        }

        partial void OnDeleteAuditDataItemPersistent(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAuditDataItemPersistent(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"AuditDataItemPersistents({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAuditDataItemPersistent(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAuditDataItemPersistentByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AuditDataItemPersistent> GetAuditDataItemPersistentByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"AuditDataItemPersistents({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAuditDataItemPersistentByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AuditDataItemPersistent>(response);
        }

        partial void OnUpdateAuditDataItemPersistent(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAuditDataItemPersistent(Guid oid = default(Guid), SGPA.Server.Models.CMU.AuditDataItemPersistent auditDataItemPersistent = default(SGPA.Server.Models.CMU.AuditDataItemPersistent))
        {
            var uri = new Uri(baseUri, $"AuditDataItemPersistents({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", auditDataItemPersistent.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(auditDataItemPersistent), Encoding.UTF8, "application/json");

            OnUpdateAuditDataItemPersistent(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAuditedObjectWeakReferencesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditedobjectweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditedobjectweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAuditedObjectWeakReferencesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditedobjectweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditedobjectweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAuditedObjectWeakReferences(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AuditedObjectWeakReference>> GetAuditedObjectWeakReferences(Query query)
        {
            return await GetAuditedObjectWeakReferences(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AuditedObjectWeakReference>> GetAuditedObjectWeakReferences(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AuditedObjectWeakReferences");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAuditedObjectWeakReferences(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.AuditedObjectWeakReference>>(response);
        }

        partial void OnCreateAuditedObjectWeakReference(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AuditedObjectWeakReference> CreateAuditedObjectWeakReference(SGPA.Server.Models.CMU.AuditedObjectWeakReference auditedObjectWeakReference = default(SGPA.Server.Models.CMU.AuditedObjectWeakReference))
        {
            var uri = new Uri(baseUri, $"AuditedObjectWeakReferences");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(auditedObjectWeakReference), Encoding.UTF8, "application/json");

            OnCreateAuditedObjectWeakReference(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AuditedObjectWeakReference>(response);
        }

        partial void OnDeleteAuditedObjectWeakReference(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAuditedObjectWeakReference(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"AuditedObjectWeakReferences({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAuditedObjectWeakReference(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAuditedObjectWeakReferenceByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.AuditedObjectWeakReference> GetAuditedObjectWeakReferenceByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"AuditedObjectWeakReferences({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAuditedObjectWeakReferenceByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.AuditedObjectWeakReference>(response);
        }

        partial void OnUpdateAuditedObjectWeakReference(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAuditedObjectWeakReference(Guid oid = default(Guid), SGPA.Server.Models.CMU.AuditedObjectWeakReference auditedObjectWeakReference = default(SGPA.Server.Models.CMU.AuditedObjectWeakReference))
        {
            var uri = new Uri(baseUri, $"AuditedObjectWeakReferences({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", auditedObjectWeakReference.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(auditedObjectWeakReference), Encoding.UTF8, "application/json");

            OnUpdateAuditedObjectWeakReference(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBajaMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBajaMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBajaMotivos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.BajaMotivo>> GetBajaMotivos(Query query)
        {
            return await GetBajaMotivos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.BajaMotivo>> GetBajaMotivos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BajaMotivos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBajaMotivos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.BajaMotivo>>(response);
        }

        partial void OnCreateBajaMotivo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.BajaMotivo> CreateBajaMotivo(SGPA.Server.Models.CMU.BajaMotivo bajaMotivo = default(SGPA.Server.Models.CMU.BajaMotivo))
        {
            var uri = new Uri(baseUri, $"BajaMotivos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bajaMotivo), Encoding.UTF8, "application/json");

            OnCreateBajaMotivo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.BajaMotivo>(response);
        }

        partial void OnDeleteBajaMotivo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBajaMotivo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"BajaMotivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBajaMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBajaMotivoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.BajaMotivo> GetBajaMotivoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"BajaMotivos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBajaMotivoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.BajaMotivo>(response);
        }

        partial void OnUpdateBajaMotivo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBajaMotivo(int id = default(int), SGPA.Server.Models.CMU.BajaMotivo bajaMotivo = default(SGPA.Server.Models.CMU.BajaMotivo))
        {
            var uri = new Uri(baseUri, $"BajaMotivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", bajaMotivo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bajaMotivo), Encoding.UTF8, "application/json");

            OnUpdateBajaMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBajaTemporalMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajatemporalmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajatemporalmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBajaTemporalMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajatemporalmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajatemporalmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBajaTemporalMotivos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.BajaTemporalMotivo>> GetBajaTemporalMotivos(Query query)
        {
            return await GetBajaTemporalMotivos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.BajaTemporalMotivo>> GetBajaTemporalMotivos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BajaTemporalMotivos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBajaTemporalMotivos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.BajaTemporalMotivo>>(response);
        }

        partial void OnCreateBajaTemporalMotivo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.BajaTemporalMotivo> CreateBajaTemporalMotivo(SGPA.Server.Models.CMU.BajaTemporalMotivo bajaTemporalMotivo = default(SGPA.Server.Models.CMU.BajaTemporalMotivo))
        {
            var uri = new Uri(baseUri, $"BajaTemporalMotivos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bajaTemporalMotivo), Encoding.UTF8, "application/json");

            OnCreateBajaTemporalMotivo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.BajaTemporalMotivo>(response);
        }

        partial void OnDeleteBajaTemporalMotivo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBajaTemporalMotivo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"BajaTemporalMotivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBajaTemporalMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBajaTemporalMotivoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.BajaTemporalMotivo> GetBajaTemporalMotivoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"BajaTemporalMotivos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBajaTemporalMotivoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.BajaTemporalMotivo>(response);
        }

        partial void OnUpdateBajaTemporalMotivo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBajaTemporalMotivo(int id = default(int), SGPA.Server.Models.CMU.BajaTemporalMotivo bajaTemporalMotivo = default(SGPA.Server.Models.CMU.BajaTemporalMotivo))
        {
            var uri = new Uri(baseUri, $"BajaTemporalMotivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", bajaTemporalMotivo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bajaTemporalMotivo), Encoding.UTF8, "application/json");

            OnUpdateBajaTemporalMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBancosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBancosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBancos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Banco>> GetBancos(Query query)
        {
            return await GetBancos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Banco>> GetBancos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Bancos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBancos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Banco>>(response);
        }

        partial void OnCreateBanco(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Banco> CreateBanco(SGPA.Server.Models.CMU.Banco banco = default(SGPA.Server.Models.CMU.Banco))
        {
            var uri = new Uri(baseUri, $"Bancos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(banco), Encoding.UTF8, "application/json");

            OnCreateBanco(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Banco>(response);
        }

        partial void OnDeleteBanco(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBanco(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Bancos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBanco(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBancoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Banco> GetBancoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Bancos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBancoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Banco>(response);
        }

        partial void OnUpdateBanco(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBanco(int id = default(int), SGPA.Server.Models.CMU.Banco banco = default(SGPA.Server.Models.CMU.Banco))
        {
            var uri = new Uri(baseUri, $"Bancos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", banco.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(banco), Encoding.UTF8, "application/json");

            OnUpdateBanco(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCargoContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cargocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cargocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCargoContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cargocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cargocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCargoContactos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CargoContacto>> GetCargoContactos(Query query)
        {
            return await GetCargoContactos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CargoContacto>> GetCargoContactos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CargoContactos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCargoContactos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CargoContacto>>(response);
        }

        partial void OnCreateCargoContacto(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CargoContacto> CreateCargoContacto(SGPA.Server.Models.CMU.CargoContacto cargoContacto = default(SGPA.Server.Models.CMU.CargoContacto))
        {
            var uri = new Uri(baseUri, $"CargoContactos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cargoContacto), Encoding.UTF8, "application/json");

            OnCreateCargoContacto(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CargoContacto>(response);
        }

        partial void OnDeleteCargoContacto(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCargoContacto(int id = default(int))
        {
            var uri = new Uri(baseUri, $"CargoContactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCargoContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCargoContactoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CargoContacto> GetCargoContactoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"CargoContactos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCargoContactoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CargoContacto>(response);
        }

        partial void OnUpdateCargoContacto(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCargoContacto(int id = default(int), SGPA.Server.Models.CMU.CargoContacto cargoContacto = default(SGPA.Server.Models.CMU.CargoContacto))
        {
            var uri = new Uri(baseUri, $"CargoContactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", cargoContacto.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cargoContacto), Encoding.UTF8, "application/json");

            OnUpdateCargoContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCategoriaColegiadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCategoriaColegiadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCategoriaColegiados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CategoriaColegiado>> GetCategoriaColegiados(Query query)
        {
            return await GetCategoriaColegiados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CategoriaColegiado>> GetCategoriaColegiados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategoriaColegiados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CategoriaColegiado>>(response);
        }

        partial void OnCreateCategoriaColegiado(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiado> CreateCategoriaColegiado(SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiado = default(SGPA.Server.Models.CMU.CategoriaColegiado))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(categoriaColegiado), Encoding.UTF8, "application/json");

            OnCreateCategoriaColegiado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CategoriaColegiado>(response);
        }

        partial void OnDeleteCategoriaColegiado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCategoriaColegiado(int id = default(int))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiados({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCategoriaColegiado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCategoriaColegiadoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiado> GetCategoriaColegiadoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiados({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategoriaColegiadoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CategoriaColegiado>(response);
        }

        partial void OnUpdateCategoriaColegiado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCategoriaColegiado(int id = default(int), SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiado = default(SGPA.Server.Models.CMU.CategoriaColegiado))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiados({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", categoriaColegiado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(categoriaColegiado), Encoding.UTF8, "application/json");

            OnUpdateCategoriaColegiado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCategoriaColegiadoValorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiadovalors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiadovalors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCategoriaColegiadoValorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiadovalors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiadovalors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCategoriaColegiadoValors(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CategoriaColegiadoValor>> GetCategoriaColegiadoValors(Query query)
        {
            return await GetCategoriaColegiadoValors(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CategoriaColegiadoValor>> GetCategoriaColegiadoValors(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiadoValors");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategoriaColegiadoValors(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CategoriaColegiadoValor>>(response);
        }

        partial void OnCreateCategoriaColegiadoValor(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiadoValor> CreateCategoriaColegiadoValor(SGPA.Server.Models.CMU.CategoriaColegiadoValor categoriaColegiadoValor = default(SGPA.Server.Models.CMU.CategoriaColegiadoValor))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiadoValors");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(categoriaColegiadoValor), Encoding.UTF8, "application/json");

            OnCreateCategoriaColegiadoValor(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CategoriaColegiadoValor>(response);
        }

        partial void OnDeleteCategoriaColegiadoValor(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCategoriaColegiadoValor(int id = default(int))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiadoValors({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCategoriaColegiadoValor(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCategoriaColegiadoValorById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiadoValor> GetCategoriaColegiadoValorById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiadoValors({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategoriaColegiadoValorById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CategoriaColegiadoValor>(response);
        }

        partial void OnUpdateCategoriaColegiadoValor(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCategoriaColegiadoValor(int id = default(int), SGPA.Server.Models.CMU.CategoriaColegiadoValor categoriaColegiadoValor = default(SGPA.Server.Models.CMU.CategoriaColegiadoValor))
        {
            var uri = new Uri(baseUri, $"CategoriaColegiadoValors({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", categoriaColegiadoValor.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(categoriaColegiadoValor), Encoding.UTF8, "application/json");

            OnUpdateCategoriaColegiadoValor(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCjpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCjpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCjps(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Cjp>> GetCjps(Query query)
        {
            return await GetCjps(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Cjp>> GetCjps(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Cjps");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCjps(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Cjp>>(response);
        }

        partial void OnCreateCjp(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Cjp> CreateCjp(SGPA.Server.Models.CMU.Cjp cjp = default(SGPA.Server.Models.CMU.Cjp))
        {
            var uri = new Uri(baseUri, $"Cjps");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cjp), Encoding.UTF8, "application/json");

            OnCreateCjp(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Cjp>(response);
        }

        partial void OnDeleteCjp(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCjp(int ci = default(int))
        {
            var uri = new Uri(baseUri, $"Cjps({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCjp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCjpByCi(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Cjp> GetCjpByCi(string expand = default(string), int ci = default(int))
        {
            var uri = new Uri(baseUri, $"Cjps({ci})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCjpByCi(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Cjp>(response);
        }

        partial void OnUpdateCjp(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCjp(int ci = default(int), SGPA.Server.Models.CMU.Cjp cjp = default(SGPA.Server.Models.CMU.Cjp))
        {
            var uri = new Uri(baseUri, $"Cjps({ci})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", cjp.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cjp), Encoding.UTF8, "application/json");

            OnUpdateCjp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCjpMatsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpmats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpmats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCjpMatsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpmats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpmats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCjpMats(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CjpMat>> GetCjpMats(Query query)
        {
            return await GetCjpMats(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CjpMat>> GetCjpMats(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CjpMats");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCjpMats(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CjpMat>>(response);
        }

        partial void OnCreateCjpMat(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CjpMat> CreateCjpMat(SGPA.Server.Models.CMU.CjpMat cjpMat = default(SGPA.Server.Models.CMU.CjpMat))
        {
            var uri = new Uri(baseUri, $"CjpMats");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cjpMat), Encoding.UTF8, "application/json");

            OnCreateCjpMat(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CjpMat>(response);
        }

        partial void OnDeleteCjpMat(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCjpMat(int documento = default(int))
        {
            var uri = new Uri(baseUri, $"CjpMats({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCjpMat(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCjpMatByDocumento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CjpMat> GetCjpMatByDocumento(string expand = default(string), int documento = default(int))
        {
            var uri = new Uri(baseUri, $"CjpMats({documento})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCjpMatByDocumento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CjpMat>(response);
        }

        partial void OnUpdateCjpMat(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCjpMat(int documento = default(int), SGPA.Server.Models.CMU.CjpMat cjpMat = default(SGPA.Server.Models.CMU.CjpMat))
        {
            var uri = new Uri(baseUri, $"CjpMats({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", cjpMat.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cjpMat), Encoding.UTF8, "application/json");

            OnUpdateCjpMat(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCjpOldsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpolds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpolds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCjpOldsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpolds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpolds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCjpOlds(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CjpOld>> GetCjpOlds(Query query)
        {
            return await GetCjpOlds(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CjpOld>> GetCjpOlds(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CjpOlds");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCjpOlds(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CjpOld>>(response);
        }

        partial void OnCreateCjpOld(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CjpOld> CreateCjpOld(SGPA.Server.Models.CMU.CjpOld cjpOld = default(SGPA.Server.Models.CMU.CjpOld))
        {
            var uri = new Uri(baseUri, $"CjpOlds");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cjpOld), Encoding.UTF8, "application/json");

            OnCreateCjpOld(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CjpOld>(response);
        }

        partial void OnDeleteCjpOld(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCjpOld(int documento = default(int))
        {
            var uri = new Uri(baseUri, $"CjpOlds({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCjpOld(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCjpOldByDocumento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CjpOld> GetCjpOldByDocumento(string expand = default(string), int documento = default(int))
        {
            var uri = new Uri(baseUri, $"CjpOlds({documento})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCjpOldByDocumento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CjpOld>(response);
        }

        partial void OnUpdateCjpOld(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCjpOld(int documento = default(int), SGPA.Server.Models.CMU.CjpOld cjpOld = default(SGPA.Server.Models.CMU.CjpOld))
        {
            var uri = new Uri(baseUri, $"CjpOlds({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", cjpOld.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cjpOld), Encoding.UTF8, "application/json");

            OnUpdateCjpOld(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCobrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCobrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCobros(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Cobro>> GetCobros(Query query)
        {
            return await GetCobros(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Cobro>> GetCobros(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Cobros");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCobros(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Cobro>>(response);
        }

        partial void OnCreateCobro(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Cobro> CreateCobro(SGPA.Server.Models.CMU.Cobro cobro = default(SGPA.Server.Models.CMU.Cobro))
        {
            var uri = new Uri(baseUri, $"Cobros");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cobro), Encoding.UTF8, "application/json");

            OnCreateCobro(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Cobro>(response);
        }

        partial void OnDeleteCobro(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCobro(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Cobros({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCobro(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCobroById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Cobro> GetCobroById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Cobros({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCobroById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Cobro>(response);
        }

        partial void OnUpdateCobro(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCobro(int id = default(int), SGPA.Server.Models.CMU.Cobro cobro = default(SGPA.Server.Models.CMU.Cobro))
        {
            var uri = new Uri(baseUri, $"Cobros({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", cobro.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cobro), Encoding.UTF8, "application/json");

            OnUpdateCobro(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCobroNominasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobronominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobronominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCobroNominasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobronominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobronominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCobroNominas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CobroNomina>> GetCobroNominas(Query query)
        {
            return await GetCobroNominas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CobroNomina>> GetCobroNominas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CobroNominas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCobroNominas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CobroNomina>>(response);
        }

        partial void OnCreateCobroNomina(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CobroNomina> CreateCobroNomina(SGPA.Server.Models.CMU.CobroNomina cobroNomina = default(SGPA.Server.Models.CMU.CobroNomina))
        {
            var uri = new Uri(baseUri, $"CobroNominas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cobroNomina), Encoding.UTF8, "application/json");

            OnCreateCobroNomina(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CobroNomina>(response);
        }

        partial void OnDeleteCobroNomina(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCobroNomina(int id = default(int))
        {
            var uri = new Uri(baseUri, $"CobroNominas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCobroNomina(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCobroNominaById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CobroNomina> GetCobroNominaById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"CobroNominas({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCobroNominaById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CobroNomina>(response);
        }

        partial void OnUpdateCobroNomina(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCobroNomina(int id = default(int), SGPA.Server.Models.CMU.CobroNomina cobroNomina = default(SGPA.Server.Models.CMU.CobroNomina))
        {
            var uri = new Uri(baseUri, $"CobroNominas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", cobroNomina.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cobroNomina), Encoding.UTF8, "application/json");

            OnUpdateCobroNomina(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Colegiado>> GetColegiados(Query query)
        {
            return await GetColegiados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Colegiado>> GetColegiados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Colegiados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Colegiado>>(response);
        }

        partial void OnCreateColegiado(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Colegiado> CreateColegiado(SGPA.Server.Models.CMU.Colegiado colegiado = default(SGPA.Server.Models.CMU.Colegiado))
        {
            var uri = new Uri(baseUri, $"Colegiados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiado), Encoding.UTF8, "application/json");

            OnCreateColegiado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Colegiado>(response);
        }

        partial void OnDeleteColegiado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiado(int documento = default(int))
        {
            var uri = new Uri(baseUri, $"Colegiados({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoByDocumento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Colegiado> GetColegiadoByDocumento(string expand = default(string), int documento = default(int))
        {
            var uri = new Uri(baseUri, $"Colegiados({documento})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoByDocumento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Colegiado>(response);
        }

        partial void OnUpdateColegiado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiado(int documento = default(int), SGPA.Server.Models.CMU.Colegiado colegiado = default(SGPA.Server.Models.CMU.Colegiado))
        {
            var uri = new Uri(baseUri, $"Colegiados({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiado), Encoding.UTF8, "application/json");

            OnUpdateColegiado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoActualizacionDpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoactualizaciondps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoactualizaciondps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoActualizacionDpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoactualizaciondps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoactualizaciondps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoActualizacionDps(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>> GetColegiadoActualizacionDps(Query query)
        {
            return await GetColegiadoActualizacionDps(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>> GetColegiadoActualizacionDps(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoActualizacionDps");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoActualizacionDps(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>>(response);
        }

        partial void OnCreateColegiadoActualizacionDp(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> CreateColegiadoActualizacionDp(SGPA.Server.Models.CMU.ColegiadoActualizacionDp colegiadoActualizacionDp = default(SGPA.Server.Models.CMU.ColegiadoActualizacionDp))
        {
            var uri = new Uri(baseUri, $"ColegiadoActualizacionDps");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoActualizacionDp), Encoding.UTF8, "application/json");

            OnCreateColegiadoActualizacionDp(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>(response);
        }

        partial void OnDeleteColegiadoActualizacionDp(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoActualizacionDp(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoActualizacionDps({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoActualizacionDp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoActualizacionDpById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> GetColegiadoActualizacionDpById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoActualizacionDps({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoActualizacionDpById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>(response);
        }

        partial void OnUpdateColegiadoActualizacionDp(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoActualizacionDp(int id = default(int), SGPA.Server.Models.CMU.ColegiadoActualizacionDp colegiadoActualizacionDp = default(SGPA.Server.Models.CMU.ColegiadoActualizacionDp))
        {
            var uri = new Uri(baseUri, $"ColegiadoActualizacionDps({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoActualizacionDp.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoActualizacionDp), Encoding.UTF8, "application/json");

            OnUpdateColegiadoActualizacionDp(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacorasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoras/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoras/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacorasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoras/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoras/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoBitacoras(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacora>> GetColegiadoBitacoras(Query query)
        {
            return await GetColegiadoBitacoras(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacora>> GetColegiadoBitacoras(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoras");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoras(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacora>>(response);
        }

        partial void OnCreateColegiadoBitacora(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacora> CreateColegiadoBitacora(SGPA.Server.Models.CMU.ColegiadoBitacora colegiadoBitacora = default(SGPA.Server.Models.CMU.ColegiadoBitacora))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoras");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacora), Encoding.UTF8, "application/json");

            OnCreateColegiadoBitacora(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacora>(response);
        }

        partial void OnDeleteColegiadoBitacora(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoBitacora(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoras({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoBitacora(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoBitacoraById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacora> GetColegiadoBitacoraById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoras({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoraById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacora>(response);
        }

        partial void OnUpdateColegiadoBitacora(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoBitacora(int id = default(int), SGPA.Server.Models.CMU.ColegiadoBitacora colegiadoBitacora = default(SGPA.Server.Models.CMU.ColegiadoBitacora))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoras({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoBitacora.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacora), Encoding.UTF8, "application/json");

            OnUpdateColegiadoBitacora(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacoraEMailEnviosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacoraEMailEnviosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoBitacoraEMailEnvios(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>> GetColegiadoBitacoraEMailEnvios(Query query)
        {
            return await GetColegiadoBitacoraEMailEnvios(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>> GetColegiadoBitacoraEMailEnvios(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailEnvios");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoraEMailEnvios(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>>(response);
        }

        partial void OnCreateColegiadoBitacoraEMailEnvio(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> CreateColegiadoBitacoraEMailEnvio(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio colegiadoBitacoraEmailEnvio = default(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailEnvios");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacoraEmailEnvio), Encoding.UTF8, "application/json");

            OnCreateColegiadoBitacoraEMailEnvio(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>(response);
        }

        partial void OnDeleteColegiadoBitacoraEMailEnvio(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoBitacoraEMailEnvio(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailEnvios({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoBitacoraEMailEnvio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoBitacoraEMailEnvioById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> GetColegiadoBitacoraEMailEnvioById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailEnvios({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoraEMailEnvioById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>(response);
        }

        partial void OnUpdateColegiadoBitacoraEMailEnvio(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoBitacoraEMailEnvio(int id = default(int), SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio colegiadoBitacoraEmailEnvio = default(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailEnvios({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoBitacoraEmailEnvio.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacoraEmailEnvio), Encoding.UTF8, "application/json");

            OnUpdateColegiadoBitacoraEMailEnvio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacoraEMailRecepcionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailrecepcions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailrecepcions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacoraEMailRecepcionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailrecepcions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailrecepcions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoBitacoraEMailRecepcions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>> GetColegiadoBitacoraEMailRecepcions(Query query)
        {
            return await GetColegiadoBitacoraEMailRecepcions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>> GetColegiadoBitacoraEMailRecepcions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailRecepcions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoraEMailRecepcions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>>(response);
        }

        partial void OnCreateColegiadoBitacoraEMailRecepcion(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> CreateColegiadoBitacoraEMailRecepcion(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion colegiadoBitacoraEmailRecepcion = default(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailRecepcions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacoraEmailRecepcion), Encoding.UTF8, "application/json");

            OnCreateColegiadoBitacoraEMailRecepcion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>(response);
        }

        partial void OnDeleteColegiadoBitacoraEMailRecepcion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoBitacoraEMailRecepcion(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailRecepcions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoBitacoraEMailRecepcion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoBitacoraEMailRecepcionById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> GetColegiadoBitacoraEMailRecepcionById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailRecepcions({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoraEMailRecepcionById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>(response);
        }

        partial void OnUpdateColegiadoBitacoraEMailRecepcion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoBitacoraEMailRecepcion(int id = default(int), SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion colegiadoBitacoraEmailRecepcion = default(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraEMailRecepcions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoBitacoraEmailRecepcion.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacoraEmailRecepcion), Encoding.UTF8, "application/json");

            OnUpdateColegiadoBitacoraEMailRecepcion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacoraNotaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoranota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoranota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoBitacoraNotaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoranota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoranota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoBitacoraNota(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>> GetColegiadoBitacoraNota(Query query)
        {
            return await GetColegiadoBitacoraNota(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>> GetColegiadoBitacoraNota(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraNota");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoraNota(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>>(response);
        }

        partial void OnCreateColegiadoBitacoraNotum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> CreateColegiadoBitacoraNotum(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum colegiadoBitacoraNotum = default(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraNota");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacoraNotum), Encoding.UTF8, "application/json");

            OnCreateColegiadoBitacoraNotum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>(response);
        }

        partial void OnDeleteColegiadoBitacoraNotum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoBitacoraNotum(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraNota({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoBitacoraNotum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoBitacoraNotumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> GetColegiadoBitacoraNotumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraNota({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoBitacoraNotumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>(response);
        }

        partial void OnUpdateColegiadoBitacoraNotum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoBitacoraNotum(int id = default(int), SGPA.Server.Models.CMU.ColegiadoBitacoraNotum colegiadoBitacoraNotum = default(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum))
        {
            var uri = new Uri(baseUri, $"ColegiadoBitacoraNota({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoBitacoraNotum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoBitacoraNotum), Encoding.UTF8, "application/json");

            OnUpdateColegiadoBitacoraNotum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoCambioCategoriaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocambiocategoria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocambiocategoria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoCambioCategoriaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocambiocategoria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocambiocategoria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoCambioCategoria(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>> GetColegiadoCambioCategoria(Query query)
        {
            return await GetColegiadoCambioCategoria(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>> GetColegiadoCambioCategoria(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoCambioCategoria");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoCambioCategoria(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>>(response);
        }

        partial void OnCreateColegiadoCambioCategorium(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> CreateColegiadoCambioCategorium(SGPA.Server.Models.CMU.ColegiadoCambioCategorium colegiadoCambioCategorium = default(SGPA.Server.Models.CMU.ColegiadoCambioCategorium))
        {
            var uri = new Uri(baseUri, $"ColegiadoCambioCategoria");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoCambioCategorium), Encoding.UTF8, "application/json");

            OnCreateColegiadoCambioCategorium(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>(response);
        }

        partial void OnDeleteColegiadoCambioCategorium(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoCambioCategorium(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoCambioCategoria({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoCambioCategorium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoCambioCategoriumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> GetColegiadoCambioCategoriumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoCambioCategoria({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoCambioCategoriumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>(response);
        }

        partial void OnUpdateColegiadoCambioCategorium(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoCambioCategorium(int id = default(int), SGPA.Server.Models.CMU.ColegiadoCambioCategorium colegiadoCambioCategorium = default(SGPA.Server.Models.CMU.ColegiadoCambioCategorium))
        {
            var uri = new Uri(baseUri, $"ColegiadoCambioCategoria({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoCambioCategorium.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoCambioCategorium), Encoding.UTF8, "application/json");

            OnUpdateColegiadoCambioCategorium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoCertificadoExpedidosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocertificadoexpedidos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocertificadoexpedidos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoCertificadoExpedidosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocertificadoexpedidos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocertificadoexpedidos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoCertificadoExpedidos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>> GetColegiadoCertificadoExpedidos(Query query)
        {
            return await GetColegiadoCertificadoExpedidos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>> GetColegiadoCertificadoExpedidos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoCertificadoExpedidos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoCertificadoExpedidos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>>(response);
        }

        partial void OnCreateColegiadoCertificadoExpedido(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> CreateColegiadoCertificadoExpedido(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido colegiadoCertificadoExpedido = default(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido))
        {
            var uri = new Uri(baseUri, $"ColegiadoCertificadoExpedidos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoCertificadoExpedido), Encoding.UTF8, "application/json");

            OnCreateColegiadoCertificadoExpedido(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>(response);
        }

        partial void OnDeleteColegiadoCertificadoExpedido(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoCertificadoExpedido(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoCertificadoExpedidos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoCertificadoExpedido(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoCertificadoExpedidoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> GetColegiadoCertificadoExpedidoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoCertificadoExpedidos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoCertificadoExpedidoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>(response);
        }

        partial void OnUpdateColegiadoCertificadoExpedido(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoCertificadoExpedido(int id = default(int), SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido colegiadoCertificadoExpedido = default(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido))
        {
            var uri = new Uri(baseUri, $"ColegiadoCertificadoExpedidos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoCertificadoExpedido.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoCertificadoExpedido), Encoding.UTF8, "application/json");

            OnUpdateColegiadoCertificadoExpedido(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoDebitoBancarioAsociadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodebitobancarioasociados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodebitobancarioasociados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoDebitoBancarioAsociadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodebitobancarioasociados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodebitobancarioasociados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoDebitoBancarioAsociados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>> GetColegiadoDebitoBancarioAsociados(Query query)
        {
            return await GetColegiadoDebitoBancarioAsociados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>> GetColegiadoDebitoBancarioAsociados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoDebitoBancarioAsociados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoDebitoBancarioAsociados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>>(response);
        }

        partial void OnCreateColegiadoDebitoBancarioAsociado(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> CreateColegiadoDebitoBancarioAsociado(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado colegiadoDebitoBancarioAsociado = default(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado))
        {
            var uri = new Uri(baseUri, $"ColegiadoDebitoBancarioAsociados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoDebitoBancarioAsociado), Encoding.UTF8, "application/json");

            OnCreateColegiadoDebitoBancarioAsociado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>(response);
        }

        partial void OnDeleteColegiadoDebitoBancarioAsociado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoDebitoBancarioAsociado(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoDebitoBancarioAsociados({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoDebitoBancarioAsociado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoDebitoBancarioAsociadoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> GetColegiadoDebitoBancarioAsociadoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoDebitoBancarioAsociados({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoDebitoBancarioAsociadoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>(response);
        }

        partial void OnUpdateColegiadoDebitoBancarioAsociado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoDebitoBancarioAsociado(int id = default(int), SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado colegiadoDebitoBancarioAsociado = default(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado))
        {
            var uri = new Uri(baseUri, $"ColegiadoDebitoBancarioAsociados({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoDebitoBancarioAsociado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoDebitoBancarioAsociado), Encoding.UTF8, "application/json");

            OnUpdateColegiadoDebitoBancarioAsociado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoDeclaracionJuradaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodeclaracionjurada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodeclaracionjurada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoDeclaracionJuradaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodeclaracionjurada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodeclaracionjurada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoDeclaracionJurada(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>> GetColegiadoDeclaracionJurada(Query query)
        {
            return await GetColegiadoDeclaracionJurada(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>> GetColegiadoDeclaracionJurada(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoDeclaracionJurada");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoDeclaracionJurada(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>>(response);
        }

        partial void OnCreateColegiadoDeclaracionJuradum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> CreateColegiadoDeclaracionJuradum(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum colegiadoDeclaracionJuradum = default(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum))
        {
            var uri = new Uri(baseUri, $"ColegiadoDeclaracionJurada");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoDeclaracionJuradum), Encoding.UTF8, "application/json");

            OnCreateColegiadoDeclaracionJuradum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>(response);
        }

        partial void OnDeleteColegiadoDeclaracionJuradum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoDeclaracionJuradum(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoDeclaracionJurada({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoDeclaracionJuradum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoDeclaracionJuradumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> GetColegiadoDeclaracionJuradumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoDeclaracionJurada({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoDeclaracionJuradumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>(response);
        }

        partial void OnUpdateColegiadoDeclaracionJuradum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoDeclaracionJuradum(int id = default(int), SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum colegiadoDeclaracionJuradum = default(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum))
        {
            var uri = new Uri(baseUri, $"ColegiadoDeclaracionJurada({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoDeclaracionJuradum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoDeclaracionJuradum), Encoding.UTF8, "application/json");

            OnUpdateColegiadoDeclaracionJuradum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoImagenesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoimagenes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoimagenes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoImagenesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoimagenes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoimagenes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoImagenes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoImagene>> GetColegiadoImagenes(Query query)
        {
            return await GetColegiadoImagenes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoImagene>> GetColegiadoImagenes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoImagenes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoImagenes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoImagene>>(response);
        }

        partial void OnCreateColegiadoImagene(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoImagene> CreateColegiadoImagene(SGPA.Server.Models.CMU.ColegiadoImagene colegiadoImagene = default(SGPA.Server.Models.CMU.ColegiadoImagene))
        {
            var uri = new Uri(baseUri, $"ColegiadoImagenes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoImagene), Encoding.UTF8, "application/json");

            OnCreateColegiadoImagene(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoImagene>(response);
        }

        partial void OnDeleteColegiadoImagene(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoImagene(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoImagenes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoImagene(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoImageneById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoImagene> GetColegiadoImageneById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoImagenes({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoImageneById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoImagene>(response);
        }

        partial void OnUpdateColegiadoImagene(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoImagene(int id = default(int), SGPA.Server.Models.CMU.ColegiadoImagene colegiadoImagene = default(SGPA.Server.Models.CMU.ColegiadoImagene))
        {
            var uri = new Uri(baseUri, $"ColegiadoImagenes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoImagene.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoImagene), Encoding.UTF8, "application/json");

            OnUpdateColegiadoImagene(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoMovimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadomovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadomovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoMovimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadomovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadomovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoMovimientos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoMovimiento>> GetColegiadoMovimientos(Query query)
        {
            return await GetColegiadoMovimientos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoMovimiento>> GetColegiadoMovimientos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoMovimientos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoMovimientos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoMovimiento>>(response);
        }

        partial void OnCreateColegiadoMovimiento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoMovimiento> CreateColegiadoMovimiento(SGPA.Server.Models.CMU.ColegiadoMovimiento colegiadoMovimiento = default(SGPA.Server.Models.CMU.ColegiadoMovimiento))
        {
            var uri = new Uri(baseUri, $"ColegiadoMovimientos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoMovimiento), Encoding.UTF8, "application/json");

            OnCreateColegiadoMovimiento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoMovimiento>(response);
        }

        partial void OnDeleteColegiadoMovimiento(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoMovimiento(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoMovimientos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoMovimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoMovimientoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoMovimiento> GetColegiadoMovimientoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoMovimientos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoMovimientoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoMovimiento>(response);
        }

        partial void OnUpdateColegiadoMovimiento(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoMovimiento(int id = default(int), SGPA.Server.Models.CMU.ColegiadoMovimiento colegiadoMovimiento = default(SGPA.Server.Models.CMU.ColegiadoMovimiento))
        {
            var uri = new Uri(baseUri, $"ColegiadoMovimientos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoMovimiento.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoMovimiento), Encoding.UTF8, "application/json");

            OnUpdateColegiadoMovimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiados2011SToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados2011s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados2011s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiados2011SToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados2011s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados2011s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiados2011S(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Colegiados2011>> GetColegiados2011S(Query query)
        {
            return await GetColegiados2011S(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Colegiados2011>> GetColegiados2011S(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Colegiados2011S");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiados2011S(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Colegiados2011>>(response);
        }

        partial void OnCreateColegiados2011(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Colegiados2011> CreateColegiados2011(SGPA.Server.Models.CMU.Colegiados2011 colegiados2011 = default(SGPA.Server.Models.CMU.Colegiados2011))
        {
            var uri = new Uri(baseUri, $"Colegiados2011S");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiados2011), Encoding.UTF8, "application/json");

            OnCreateColegiados2011(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Colegiados2011>(response);
        }

        partial void OnDeleteColegiados2011(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiados2011(int documento = default(int))
        {
            var uri = new Uri(baseUri, $"Colegiados2011S({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiados2011(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiados2011ByDocumento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Colegiados2011> GetColegiados2011ByDocumento(string expand = default(string), int documento = default(int))
        {
            var uri = new Uri(baseUri, $"Colegiados2011S({documento})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiados2011ByDocumento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Colegiados2011>(response);
        }

        partial void OnUpdateColegiados2011(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiados2011(int documento = default(int), SGPA.Server.Models.CMU.Colegiados2011 colegiados2011 = default(SGPA.Server.Models.CMU.Colegiados2011))
        {
            var uri = new Uri(baseUri, $"Colegiados2011S({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiados2011.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiados2011), Encoding.UTF8, "application/json");

            OnUpdateColegiados2011(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportColegiadoTarjetaDebitoAsociadaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadotarjetadebitoasociada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadotarjetadebitoasociada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportColegiadoTarjetaDebitoAsociadaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadotarjetadebitoasociada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadotarjetadebitoasociada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetColegiadoTarjetaDebitoAsociada(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>> GetColegiadoTarjetaDebitoAsociada(Query query)
        {
            return await GetColegiadoTarjetaDebitoAsociada(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>> GetColegiadoTarjetaDebitoAsociada(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ColegiadoTarjetaDebitoAsociada");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoTarjetaDebitoAsociada(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>>(response);
        }

        partial void OnCreateColegiadoTarjetaDebitoAsociadum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> CreateColegiadoTarjetaDebitoAsociadum(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum colegiadoTarjetaDebitoAsociadum = default(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum))
        {
            var uri = new Uri(baseUri, $"ColegiadoTarjetaDebitoAsociada");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoTarjetaDebitoAsociadum), Encoding.UTF8, "application/json");

            OnCreateColegiadoTarjetaDebitoAsociadum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>(response);
        }

        partial void OnDeleteColegiadoTarjetaDebitoAsociadum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteColegiadoTarjetaDebitoAsociadum(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoTarjetaDebitoAsociada({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteColegiadoTarjetaDebitoAsociadum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetColegiadoTarjetaDebitoAsociadumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> GetColegiadoTarjetaDebitoAsociadumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ColegiadoTarjetaDebitoAsociada({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetColegiadoTarjetaDebitoAsociadumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>(response);
        }

        partial void OnUpdateColegiadoTarjetaDebitoAsociadum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateColegiadoTarjetaDebitoAsociadum(int id = default(int), SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum colegiadoTarjetaDebitoAsociadum = default(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum))
        {
            var uri = new Uri(baseUri, $"ColegiadoTarjetaDebitoAsociada({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", colegiadoTarjetaDebitoAsociadum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(colegiadoTarjetaDebitoAsociadum), Encoding.UTF8, "application/json");

            OnUpdateColegiadoTarjetaDebitoAsociadum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContactos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Contacto>> GetContactos(Query query)
        {
            return await GetContactos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Contacto>> GetContactos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contactos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Contacto>>(response);
        }

        partial void OnCreateContacto(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Contacto> CreateContacto(SGPA.Server.Models.CMU.Contacto contacto = default(SGPA.Server.Models.CMU.Contacto))
        {
            var uri = new Uri(baseUri, $"Contactos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contacto), Encoding.UTF8, "application/json");

            OnCreateContacto(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Contacto>(response);
        }

        partial void OnDeleteContacto(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContacto(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContactoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Contacto> GetContactoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contactos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Contacto>(response);
        }

        partial void OnUpdateContacto(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContacto(int id = default(int), SGPA.Server.Models.CMU.Contacto contacto = default(SGPA.Server.Models.CMU.Contacto))
        {
            var uri = new Uri(baseUri, $"Contactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", contacto.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contacto), Encoding.UTF8, "application/json");

            OnUpdateContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportContactoInfoAdicionalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactoinfoadicionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactoinfoadicionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactoInfoAdicionalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactoinfoadicionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactoinfoadicionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContactoInfoAdicionals(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ContactoInfoAdicional>> GetContactoInfoAdicionals(Query query)
        {
            return await GetContactoInfoAdicionals(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ContactoInfoAdicional>> GetContactoInfoAdicionals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ContactoInfoAdicionals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactoInfoAdicionals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ContactoInfoAdicional>>(response);
        }

        partial void OnCreateContactoInfoAdicional(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ContactoInfoAdicional> CreateContactoInfoAdicional(SGPA.Server.Models.CMU.ContactoInfoAdicional contactoInfoAdicional = default(SGPA.Server.Models.CMU.ContactoInfoAdicional))
        {
            var uri = new Uri(baseUri, $"ContactoInfoAdicionals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactoInfoAdicional), Encoding.UTF8, "application/json");

            OnCreateContactoInfoAdicional(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ContactoInfoAdicional>(response);
        }

        partial void OnDeleteContactoInfoAdicional(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContactoInfoAdicional(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ContactoInfoAdicionals({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContactoInfoAdicional(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContactoInfoAdicionalById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ContactoInfoAdicional> GetContactoInfoAdicionalById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ContactoInfoAdicionals({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactoInfoAdicionalById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ContactoInfoAdicional>(response);
        }

        partial void OnUpdateContactoInfoAdicional(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContactoInfoAdicional(int id = default(int), SGPA.Server.Models.CMU.ContactoInfoAdicional contactoInfoAdicional = default(SGPA.Server.Models.CMU.ContactoInfoAdicional))
        {
            var uri = new Uri(baseUri, $"ContactoInfoAdicionals({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", contactoInfoAdicional.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactoInfoAdicional), Encoding.UTF8, "application/json");

            OnUpdateContactoInfoAdicional(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportConveniosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/convenios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/convenios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportConveniosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/convenios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/convenios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetConvenios(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Convenio>> GetConvenios(Query query)
        {
            return await GetConvenios(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Convenio>> GetConvenios(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Convenios");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConvenios(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Convenio>>(response);
        }

        partial void OnCreateConvenio(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Convenio> CreateConvenio(SGPA.Server.Models.CMU.Convenio convenio = default(SGPA.Server.Models.CMU.Convenio))
        {
            var uri = new Uri(baseUri, $"Convenios");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(convenio), Encoding.UTF8, "application/json");

            OnCreateConvenio(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Convenio>(response);
        }

        partial void OnDeleteConvenio(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteConvenio(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Convenios({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteConvenio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetConvenioById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Convenio> GetConvenioById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Convenios({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConvenioById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Convenio>(response);
        }

        partial void OnUpdateConvenio(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateConvenio(int id = default(int), SGPA.Server.Models.CMU.Convenio convenio = default(SGPA.Server.Models.CMU.Convenio))
        {
            var uri = new Uri(baseUri, $"Convenios({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", convenio.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(convenio), Encoding.UTF8, "application/json");

            OnUpdateConvenio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportConvenioFinanciacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/conveniofinanciacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/conveniofinanciacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportConvenioFinanciacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/conveniofinanciacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/conveniofinanciacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetConvenioFinanciacions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ConvenioFinanciacion>> GetConvenioFinanciacions(Query query)
        {
            return await GetConvenioFinanciacions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ConvenioFinanciacion>> GetConvenioFinanciacions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ConvenioFinanciacions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConvenioFinanciacions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ConvenioFinanciacion>>(response);
        }

        partial void OnCreateConvenioFinanciacion(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ConvenioFinanciacion> CreateConvenioFinanciacion(SGPA.Server.Models.CMU.ConvenioFinanciacion convenioFinanciacion = default(SGPA.Server.Models.CMU.ConvenioFinanciacion))
        {
            var uri = new Uri(baseUri, $"ConvenioFinanciacions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(convenioFinanciacion), Encoding.UTF8, "application/json");

            OnCreateConvenioFinanciacion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ConvenioFinanciacion>(response);
        }

        partial void OnDeleteConvenioFinanciacion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteConvenioFinanciacion(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ConvenioFinanciacions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteConvenioFinanciacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetConvenioFinanciacionById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ConvenioFinanciacion> GetConvenioFinanciacionById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ConvenioFinanciacions({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConvenioFinanciacionById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ConvenioFinanciacion>(response);
        }

        partial void OnUpdateConvenioFinanciacion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateConvenioFinanciacion(int id = default(int), SGPA.Server.Models.CMU.ConvenioFinanciacion convenioFinanciacion = default(SGPA.Server.Models.CMU.ConvenioFinanciacion))
        {
            var uri = new Uri(baseUri, $"ConvenioFinanciacions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", convenioFinanciacion.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(convenioFinanciacion), Encoding.UTF8, "application/json");

            OnUpdateConvenioFinanciacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCuentaBancariaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCuentaBancariaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCuentaBancaria(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CuentaBancarium>> GetCuentaBancaria(Query query)
        {
            return await GetCuentaBancaria(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CuentaBancarium>> GetCuentaBancaria(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CuentaBancaria");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCuentaBancaria(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.CuentaBancarium>>(response);
        }

        partial void OnCreateCuentaBancarium(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CuentaBancarium> CreateCuentaBancarium(SGPA.Server.Models.CMU.CuentaBancarium cuentaBancarium = default(SGPA.Server.Models.CMU.CuentaBancarium))
        {
            var uri = new Uri(baseUri, $"CuentaBancaria");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cuentaBancarium), Encoding.UTF8, "application/json");

            OnCreateCuentaBancarium(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CuentaBancarium>(response);
        }

        partial void OnDeleteCuentaBancarium(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCuentaBancarium(int id = default(int))
        {
            var uri = new Uri(baseUri, $"CuentaBancaria({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCuentaBancarium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCuentaBancariumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.CuentaBancarium> GetCuentaBancariumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"CuentaBancaria({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCuentaBancariumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.CuentaBancarium>(response);
        }

        partial void OnUpdateCuentaBancarium(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCuentaBancarium(int id = default(int), SGPA.Server.Models.CMU.CuentaBancarium cuentaBancarium = default(SGPA.Server.Models.CMU.CuentaBancarium))
        {
            var uri = new Uri(baseUri, $"CuentaBancaria({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", cuentaBancarium.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(cuentaBancarium), Encoding.UTF8, "application/json");

            OnUpdateCuentaBancarium(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDebitosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDebitosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDebitos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Debito>> GetDebitos(Query query)
        {
            return await GetDebitos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Debito>> GetDebitos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Debitos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDebitos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Debito>>(response);
        }

        partial void OnCreateDebito(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Debito> CreateDebito(SGPA.Server.Models.CMU.Debito debito = default(SGPA.Server.Models.CMU.Debito))
        {
            var uri = new Uri(baseUri, $"Debitos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(debito), Encoding.UTF8, "application/json");

            OnCreateDebito(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Debito>(response);
        }

        partial void OnDeleteDebito(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDebito(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Debitos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDebito(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDebitoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Debito> GetDebitoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Debitos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDebitoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Debito>(response);
        }

        partial void OnUpdateDebito(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDebito(int id = default(int), SGPA.Server.Models.CMU.Debito debito = default(SGPA.Server.Models.CMU.Debito))
        {
            var uri = new Uri(baseUri, $"Debitos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", debito.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(debito), Encoding.UTF8, "application/json");

            OnUpdateDebito(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDebitoAdjuntosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitoadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitoadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDebitoAdjuntosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitoadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitoadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDebitoAdjuntos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DebitoAdjunto>> GetDebitoAdjuntos(Query query)
        {
            return await GetDebitoAdjuntos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DebitoAdjunto>> GetDebitoAdjuntos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DebitoAdjuntos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDebitoAdjuntos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DebitoAdjunto>>(response);
        }

        partial void OnCreateDebitoAdjunto(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DebitoAdjunto> CreateDebitoAdjunto(SGPA.Server.Models.CMU.DebitoAdjunto debitoAdjunto = default(SGPA.Server.Models.CMU.DebitoAdjunto))
        {
            var uri = new Uri(baseUri, $"DebitoAdjuntos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(debitoAdjunto), Encoding.UTF8, "application/json");

            OnCreateDebitoAdjunto(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DebitoAdjunto>(response);
        }

        partial void OnDeleteDebitoAdjunto(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDebitoAdjunto(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DebitoAdjuntos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDebitoAdjunto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDebitoAdjuntoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DebitoAdjunto> GetDebitoAdjuntoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DebitoAdjuntos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDebitoAdjuntoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DebitoAdjunto>(response);
        }

        partial void OnUpdateDebitoAdjunto(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDebitoAdjunto(int id = default(int), SGPA.Server.Models.CMU.DebitoAdjunto debitoAdjunto = default(SGPA.Server.Models.CMU.DebitoAdjunto))
        {
            var uri = new Uri(baseUri, $"DebitoAdjuntos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", debitoAdjunto.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(debitoAdjunto), Encoding.UTF8, "application/json");

            OnUpdateDebitoAdjunto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDebitoNominasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDebitoNominasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDebitoNominas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DebitoNomina>> GetDebitoNominas(Query query)
        {
            return await GetDebitoNominas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DebitoNomina>> GetDebitoNominas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DebitoNominas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDebitoNominas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DebitoNomina>>(response);
        }

        partial void OnCreateDebitoNomina(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DebitoNomina> CreateDebitoNomina(SGPA.Server.Models.CMU.DebitoNomina debitoNomina = default(SGPA.Server.Models.CMU.DebitoNomina))
        {
            var uri = new Uri(baseUri, $"DebitoNominas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(debitoNomina), Encoding.UTF8, "application/json");

            OnCreateDebitoNomina(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DebitoNomina>(response);
        }

        partial void OnDeleteDebitoNomina(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDebitoNomina(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DebitoNominas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDebitoNomina(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDebitoNominaById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DebitoNomina> GetDebitoNominaById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DebitoNominas({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDebitoNominaById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DebitoNomina>(response);
        }

        partial void OnUpdateDebitoNomina(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDebitoNomina(int id = default(int), SGPA.Server.Models.CMU.DebitoNomina debitoNomina = default(SGPA.Server.Models.CMU.DebitoNomina))
        {
            var uri = new Uri(baseUri, $"DebitoNominas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", debitoNomina.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(debitoNomina), Encoding.UTF8, "application/json");

            OnUpdateDebitoNomina(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDeclaracionJuradaAdjuntosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradaadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradaadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDeclaracionJuradaAdjuntosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradaadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradaadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDeclaracionJuradaAdjuntos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>> GetDeclaracionJuradaAdjuntos(Query query)
        {
            return await GetDeclaracionJuradaAdjuntos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>> GetDeclaracionJuradaAdjuntos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaAdjuntos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDeclaracionJuradaAdjuntos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>>(response);
        }

        partial void OnCreateDeclaracionJuradaAdjunto(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> CreateDeclaracionJuradaAdjunto(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto declaracionJuradaAdjunto = default(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaAdjuntos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(declaracionJuradaAdjunto), Encoding.UTF8, "application/json");

            OnCreateDeclaracionJuradaAdjunto(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>(response);
        }

        partial void OnDeleteDeclaracionJuradaAdjunto(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDeclaracionJuradaAdjunto(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaAdjuntos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDeclaracionJuradaAdjunto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDeclaracionJuradaAdjuntoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> GetDeclaracionJuradaAdjuntoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaAdjuntos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDeclaracionJuradaAdjuntoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>(response);
        }

        partial void OnUpdateDeclaracionJuradaAdjunto(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDeclaracionJuradaAdjunto(int id = default(int), SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto declaracionJuradaAdjunto = default(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaAdjuntos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", declaracionJuradaAdjunto.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(declaracionJuradaAdjunto), Encoding.UTF8, "application/json");

            OnUpdateDeclaracionJuradaAdjunto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDeclaracionJuradaTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDeclaracionJuradaTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDeclaracionJuradaTipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>> GetDeclaracionJuradaTipos(Query query)
        {
            return await GetDeclaracionJuradaTipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>> GetDeclaracionJuradaTipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaTipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDeclaracionJuradaTipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>>(response);
        }

        partial void OnCreateDeclaracionJuradaTipo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> CreateDeclaracionJuradaTipo(SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionJuradaTipo = default(SGPA.Server.Models.CMU.DeclaracionJuradaTipo))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaTipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(declaracionJuradaTipo), Encoding.UTF8, "application/json");

            OnCreateDeclaracionJuradaTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>(response);
        }

        partial void OnDeleteDeclaracionJuradaTipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDeclaracionJuradaTipo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaTipos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDeclaracionJuradaTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDeclaracionJuradaTipoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> GetDeclaracionJuradaTipoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaTipos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDeclaracionJuradaTipoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>(response);
        }

        partial void OnUpdateDeclaracionJuradaTipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDeclaracionJuradaTipo(int id = default(int), SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionJuradaTipo = default(SGPA.Server.Models.CMU.DeclaracionJuradaTipo))
        {
            var uri = new Uri(baseUri, $"DeclaracionJuradaTipos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", declaracionJuradaTipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(declaracionJuradaTipo), Encoding.UTF8, "application/json");

            OnUpdateDeclaracionJuradaTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepartamentosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepartamentosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepartamentos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Departamento>> GetDepartamentos(Query query)
        {
            return await GetDepartamentos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Departamento>> GetDepartamentos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Departamentos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepartamentos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Departamento>>(response);
        }

        partial void OnCreateDepartamento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Departamento> CreateDepartamento(SGPA.Server.Models.CMU.Departamento departamento = default(SGPA.Server.Models.CMU.Departamento))
        {
            var uri = new Uri(baseUri, $"Departamentos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(departamento), Encoding.UTF8, "application/json");

            OnCreateDepartamento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Departamento>(response);
        }

        partial void OnDeleteDepartamento(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepartamento(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Departamentos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepartamento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepartamentoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Departamento> GetDepartamentoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Departamentos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepartamentoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Departamento>(response);
        }

        partial void OnUpdateDepartamento(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepartamento(int id = default(int), SGPA.Server.Models.CMU.Departamento departamento = default(SGPA.Server.Models.CMU.Departamento))
        {
            var uri = new Uri(baseUri, $"Departamentos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", departamento.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(departamento), Encoding.UTF8, "application/json");

            OnUpdateDepartamento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepositosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepositosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepositos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Deposito>> GetDepositos(Query query)
        {
            return await GetDepositos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Deposito>> GetDepositos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Depositos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Deposito>>(response);
        }

        partial void OnCreateDeposito(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Deposito> CreateDeposito(SGPA.Server.Models.CMU.Deposito deposito = default(SGPA.Server.Models.CMU.Deposito))
        {
            var uri = new Uri(baseUri, $"Depositos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(deposito), Encoding.UTF8, "application/json");

            OnCreateDeposito(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Deposito>(response);
        }

        partial void OnDeleteDeposito(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDeposito(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Depositos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDeposito(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepositoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Deposito> GetDepositoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Depositos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Deposito>(response);
        }

        partial void OnUpdateDeposito(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDeposito(int id = default(int), SGPA.Server.Models.CMU.Deposito deposito = default(SGPA.Server.Models.CMU.Deposito))
        {
            var uri = new Uri(baseUri, $"Depositos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", deposito.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(deposito), Encoding.UTF8, "application/json");

            OnUpdateDeposito(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepositoNominas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNomina>> GetDepositoNominas(Query query)
        {
            return await GetDepositoNominas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNomina>> GetDepositoNominas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DepositoNominas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNomina>>(response);
        }

        partial void OnCreateDepositoNomina(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNomina> CreateDepositoNomina(SGPA.Server.Models.CMU.DepositoNomina depositoNomina = default(SGPA.Server.Models.CMU.DepositoNomina))
        {
            var uri = new Uri(baseUri, $"DepositoNominas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNomina), Encoding.UTF8, "application/json");

            OnCreateDepositoNomina(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNomina>(response);
        }

        partial void OnDeleteDepositoNomina(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepositoNomina(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepositoNomina(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepositoNominaById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNomina> GetDepositoNominaById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominas({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominaById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNomina>(response);
        }

        partial void OnUpdateDepositoNomina(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepositoNomina(int id = default(int), SGPA.Server.Models.CMU.DepositoNomina depositoNomina = default(SGPA.Server.Models.CMU.DepositoNomina))
        {
            var uri = new Uri(baseUri, $"DepositoNominas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", depositoNomina.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNomina), Encoding.UTF8, "application/json");

            OnUpdateDepositoNomina(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominaMultiBrousToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominamultibrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominamultibrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominaMultiBrousToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominamultibrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominamultibrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepositoNominaMultiBrous(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>> GetDepositoNominaMultiBrous(Query query)
        {
            return await GetDepositoNominaMultiBrous(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>> GetDepositoNominaMultiBrous(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DepositoNominaMultiBrous");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominaMultiBrous(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>>(response);
        }

        partial void OnCreateDepositoNominaMultiBrou(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> CreateDepositoNominaMultiBrou(SGPA.Server.Models.CMU.DepositoNominaMultiBrou depositoNominaMultiBrou = default(SGPA.Server.Models.CMU.DepositoNominaMultiBrou))
        {
            var uri = new Uri(baseUri, $"DepositoNominaMultiBrous");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNominaMultiBrou), Encoding.UTF8, "application/json");

            OnCreateDepositoNominaMultiBrou(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>(response);
        }

        partial void OnDeleteDepositoNominaMultiBrou(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepositoNominaMultiBrou(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominaMultiBrous({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepositoNominaMultiBrou(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepositoNominaMultiBrouById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> GetDepositoNominaMultiBrouById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominaMultiBrous({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominaMultiBrouById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>(response);
        }

        partial void OnUpdateDepositoNominaMultiBrou(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepositoNominaMultiBrou(int id = default(int), SGPA.Server.Models.CMU.DepositoNominaMultiBrou depositoNominaMultiBrou = default(SGPA.Server.Models.CMU.DepositoNominaMultiBrou))
        {
            var uri = new Uri(baseUri, $"DepositoNominaMultiBrous({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", depositoNominaMultiBrou.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNominaMultiBrou), Encoding.UTF8, "application/json");

            OnUpdateDepositoNominaMultiBrou(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominaNoIdentificadaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominanoidentificada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominanoidentificada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominaNoIdentificadaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominanoidentificada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominanoidentificada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepositoNominaNoIdentificada(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>> GetDepositoNominaNoIdentificada(Query query)
        {
            return await GetDepositoNominaNoIdentificada(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>> GetDepositoNominaNoIdentificada(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DepositoNominaNoIdentificada");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominaNoIdentificada(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>>(response);
        }

        partial void OnCreateDepositoNominaNoIdentificadum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> CreateDepositoNominaNoIdentificadum(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum depositoNominaNoIdentificadum = default(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum))
        {
            var uri = new Uri(baseUri, $"DepositoNominaNoIdentificada");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNominaNoIdentificadum), Encoding.UTF8, "application/json");

            OnCreateDepositoNominaNoIdentificadum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>(response);
        }

        partial void OnDeleteDepositoNominaNoIdentificadum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepositoNominaNoIdentificadum(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominaNoIdentificada({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepositoNominaNoIdentificadum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepositoNominaNoIdentificadumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> GetDepositoNominaNoIdentificadumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominaNoIdentificada({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominaNoIdentificadumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>(response);
        }

        partial void OnUpdateDepositoNominaNoIdentificadum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepositoNominaNoIdentificadum(int id = default(int), SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum depositoNominaNoIdentificadum = default(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum))
        {
            var uri = new Uri(baseUri, $"DepositoNominaNoIdentificada({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", depositoNominaNoIdentificadum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNominaNoIdentificadum), Encoding.UTF8, "application/json");

            OnUpdateDepositoNominaNoIdentificadum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominaRedPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominaredpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominaredpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepositoNominaRedPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominaredpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominaredpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepositoNominaRedPagos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaRedPago>> GetDepositoNominaRedPagos(Query query)
        {
            return await GetDepositoNominaRedPagos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaRedPago>> GetDepositoNominaRedPagos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DepositoNominaRedPagos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominaRedPagos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DepositoNominaRedPago>>(response);
        }

        partial void OnCreateDepositoNominaRedPago(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaRedPago> CreateDepositoNominaRedPago(SGPA.Server.Models.CMU.DepositoNominaRedPago depositoNominaRedPago = default(SGPA.Server.Models.CMU.DepositoNominaRedPago))
        {
            var uri = new Uri(baseUri, $"DepositoNominaRedPagos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNominaRedPago), Encoding.UTF8, "application/json");

            OnCreateDepositoNominaRedPago(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNominaRedPago>(response);
        }

        partial void OnDeleteDepositoNominaRedPago(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepositoNominaRedPago(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominaRedPagos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepositoNominaRedPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepositoNominaRedPagoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaRedPago> GetDepositoNominaRedPagoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DepositoNominaRedPagos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepositoNominaRedPagoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DepositoNominaRedPago>(response);
        }

        partial void OnUpdateDepositoNominaRedPago(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepositoNominaRedPago(int id = default(int), SGPA.Server.Models.CMU.DepositoNominaRedPago depositoNominaRedPago = default(SGPA.Server.Models.CMU.DepositoNominaRedPago))
        {
            var uri = new Uri(baseUri, $"DepositoNominaRedPagos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", depositoNominaRedPago.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depositoNominaRedPago), Encoding.UTF8, "application/json");

            OnUpdateDepositoNominaRedPago(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDjInactividadMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/djinactividadmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/djinactividadmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDjInactividadMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/djinactividadmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/djinactividadmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDjInactividadMotivos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DjInactividadMotivo>> GetDjInactividadMotivos(Query query)
        {
            return await GetDjInactividadMotivos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DjInactividadMotivo>> GetDjInactividadMotivos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DjInactividadMotivos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDjInactividadMotivos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DjInactividadMotivo>>(response);
        }

        partial void OnCreateDjInactividadMotivo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DjInactividadMotivo> CreateDjInactividadMotivo(SGPA.Server.Models.CMU.DjInactividadMotivo djInactividadMotivo = default(SGPA.Server.Models.CMU.DjInactividadMotivo))
        {
            var uri = new Uri(baseUri, $"DjInactividadMotivos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(djInactividadMotivo), Encoding.UTF8, "application/json");

            OnCreateDjInactividadMotivo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DjInactividadMotivo>(response);
        }

        partial void OnDeleteDjInactividadMotivo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDjInactividadMotivo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DjInactividadMotivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDjInactividadMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDjInactividadMotivoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DjInactividadMotivo> GetDjInactividadMotivoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DjInactividadMotivos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDjInactividadMotivoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DjInactividadMotivo>(response);
        }

        partial void OnUpdateDjInactividadMotivo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDjInactividadMotivo(int id = default(int), SGPA.Server.Models.CMU.DjInactividadMotivo djInactividadMotivo = default(SGPA.Server.Models.CMU.DjInactividadMotivo))
        {
            var uri = new Uri(baseUri, $"DjInactividadMotivos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", djInactividadMotivo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(djInactividadMotivo), Encoding.UTF8, "application/json");

            OnUpdateDjInactividadMotivo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDynamicListViewFiltersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/dynamiclistviewfilters/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/dynamiclistviewfilters/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDynamicListViewFiltersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/dynamiclistviewfilters/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/dynamiclistviewfilters/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDynamicListViewFilters(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DynamicListViewFilter>> GetDynamicListViewFilters(Query query)
        {
            return await GetDynamicListViewFilters(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DynamicListViewFilter>> GetDynamicListViewFilters(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DynamicListViewFilters");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDynamicListViewFilters(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.DynamicListViewFilter>>(response);
        }

        partial void OnCreateDynamicListViewFilter(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DynamicListViewFilter> CreateDynamicListViewFilter(SGPA.Server.Models.CMU.DynamicListViewFilter dynamicListViewFilter = default(SGPA.Server.Models.CMU.DynamicListViewFilter))
        {
            var uri = new Uri(baseUri, $"DynamicListViewFilters");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(dynamicListViewFilter), Encoding.UTF8, "application/json");

            OnCreateDynamicListViewFilter(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DynamicListViewFilter>(response);
        }

        partial void OnDeleteDynamicListViewFilter(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDynamicListViewFilter(int id = default(int))
        {
            var uri = new Uri(baseUri, $"DynamicListViewFilters({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDynamicListViewFilter(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDynamicListViewFilterById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.DynamicListViewFilter> GetDynamicListViewFilterById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"DynamicListViewFilters({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDynamicListViewFilterById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.DynamicListViewFilter>(response);
        }

        partial void OnUpdateDynamicListViewFilter(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDynamicListViewFilter(int id = default(int), SGPA.Server.Models.CMU.DynamicListViewFilter dynamicListViewFilter = default(SGPA.Server.Models.CMU.DynamicListViewFilter))
        {
            var uri = new Uri(baseUri, $"DynamicListViewFilters({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", dynamicListViewFilter.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(dynamicListViewFilter), Encoding.UTF8, "application/json");

            OnUpdateDynamicListViewFilter(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEmailEnviosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/emailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/emailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEmailEnviosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/emailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/emailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEmailEnvios(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.EmailEnvio>> GetEmailEnvios(Query query)
        {
            return await GetEmailEnvios(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.EmailEnvio>> GetEmailEnvios(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"EmailEnvios");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmailEnvios(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.EmailEnvio>>(response);
        }

        partial void OnCreateEmailEnvio(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.EmailEnvio> CreateEmailEnvio(SGPA.Server.Models.CMU.EmailEnvio emailEnvio = default(SGPA.Server.Models.CMU.EmailEnvio))
        {
            var uri = new Uri(baseUri, $"EmailEnvios");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(emailEnvio), Encoding.UTF8, "application/json");

            OnCreateEmailEnvio(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.EmailEnvio>(response);
        }

        partial void OnDeleteEmailEnvio(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEmailEnvio(int id = default(int))
        {
            var uri = new Uri(baseUri, $"EmailEnvios({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEmailEnvio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEmailEnvioById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.EmailEnvio> GetEmailEnvioById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"EmailEnvios({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmailEnvioById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.EmailEnvio>(response);
        }

        partial void OnUpdateEmailEnvio(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEmailEnvio(int id = default(int), SGPA.Server.Models.CMU.EmailEnvio emailEnvio = default(SGPA.Server.Models.CMU.EmailEnvio))
        {
            var uri = new Uri(baseUri, $"EmailEnvios({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", emailEnvio.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(emailEnvio), Encoding.UTF8, "application/json");

            OnUpdateEmailEnvio(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEspecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEspecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEspecialidads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Especialidad>> GetEspecialidads(Query query)
        {
            return await GetEspecialidads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Especialidad>> GetEspecialidads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Especialidads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEspecialidads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Especialidad>>(response);
        }

        partial void OnCreateEspecialidad(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Especialidad> CreateEspecialidad(SGPA.Server.Models.CMU.Especialidad especialidad = default(SGPA.Server.Models.CMU.Especialidad))
        {
            var uri = new Uri(baseUri, $"Especialidads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(especialidad), Encoding.UTF8, "application/json");

            OnCreateEspecialidad(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Especialidad>(response);
        }

        partial void OnDeleteEspecialidad(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEspecialidad(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Especialidads({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEspecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEspecialidadById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Especialidad> GetEspecialidadById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Especialidads({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEspecialidadById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Especialidad>(response);
        }

        partial void OnUpdateEspecialidad(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEspecialidad(int id = default(int), SGPA.Server.Models.CMU.Especialidad especialidad = default(SGPA.Server.Models.CMU.Especialidad))
        {
            var uri = new Uri(baseUri, $"Especialidads({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", especialidad.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(especialidad), Encoding.UTF8, "application/json");

            OnUpdateEspecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportFacultadTitulosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/facultadtitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/facultadtitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFacultadTitulosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/facultadtitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/facultadtitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetFacultadTitulos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.FacultadTitulo>> GetFacultadTitulos(Query query)
        {
            return await GetFacultadTitulos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.FacultadTitulo>> GetFacultadTitulos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"FacultadTitulos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFacultadTitulos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.FacultadTitulo>>(response);
        }

        partial void OnCreateFacultadTitulo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.FacultadTitulo> CreateFacultadTitulo(SGPA.Server.Models.CMU.FacultadTitulo facultadTitulo = default(SGPA.Server.Models.CMU.FacultadTitulo))
        {
            var uri = new Uri(baseUri, $"FacultadTitulos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(facultadTitulo), Encoding.UTF8, "application/json");

            OnCreateFacultadTitulo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.FacultadTitulo>(response);
        }

        partial void OnDeleteFacultadTitulo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteFacultadTitulo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"FacultadTitulos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFacultadTitulo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetFacultadTituloById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.FacultadTitulo> GetFacultadTituloById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"FacultadTitulos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFacultadTituloById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.FacultadTitulo>(response);
        }

        partial void OnUpdateFacultadTitulo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateFacultadTitulo(int id = default(int), SGPA.Server.Models.CMU.FacultadTitulo facultadTitulo = default(SGPA.Server.Models.CMU.FacultadTitulo))
        {
            var uri = new Uri(baseUri, $"FacultadTitulos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", facultadTitulo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(facultadTitulo), Encoding.UTF8, "application/json");

            OnUpdateFacultadTitulo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportFileDataToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/filedata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/filedata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFileDataToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/filedata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/filedata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetFileData(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.FileDatum>> GetFileData(Query query)
        {
            return await GetFileData(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.FileDatum>> GetFileData(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"FileData");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFileData(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.FileDatum>>(response);
        }

        partial void OnCreateFileDatum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.FileDatum> CreateFileDatum(SGPA.Server.Models.CMU.FileDatum fileDatum = default(SGPA.Server.Models.CMU.FileDatum))
        {
            var uri = new Uri(baseUri, $"FileData");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(fileDatum), Encoding.UTF8, "application/json");

            OnCreateFileDatum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.FileDatum>(response);
        }

        partial void OnDeleteFileDatum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteFileDatum(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"FileData({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFileDatum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetFileDatumByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.FileDatum> GetFileDatumByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"FileData({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFileDatumByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.FileDatum>(response);
        }

        partial void OnUpdateFileDatum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateFileDatum(Guid oid = default(Guid), SGPA.Server.Models.CMU.FileDatum fileDatum = default(SGPA.Server.Models.CMU.FileDatum))
        {
            var uri = new Uri(baseUri, $"FileData({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", fileDatum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(fileDatum), Encoding.UTF8, "application/json");

            OnUpdateFileDatum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportGrupoContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportGrupoContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetGrupoContactos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.GrupoContacto>> GetGrupoContactos(Query query)
        {
            return await GetGrupoContactos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.GrupoContacto>> GetGrupoContactos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"GrupoContactos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGrupoContactos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.GrupoContacto>>(response);
        }

        partial void OnCreateGrupoContacto(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.GrupoContacto> CreateGrupoContacto(SGPA.Server.Models.CMU.GrupoContacto grupoContacto = default(SGPA.Server.Models.CMU.GrupoContacto))
        {
            var uri = new Uri(baseUri, $"GrupoContactos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(grupoContacto), Encoding.UTF8, "application/json");

            OnCreateGrupoContacto(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.GrupoContacto>(response);
        }

        partial void OnDeleteGrupoContacto(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteGrupoContacto(int id = default(int))
        {
            var uri = new Uri(baseUri, $"GrupoContactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteGrupoContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetGrupoContactoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.GrupoContacto> GetGrupoContactoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"GrupoContactos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGrupoContactoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.GrupoContacto>(response);
        }

        partial void OnUpdateGrupoContacto(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateGrupoContacto(int id = default(int), SGPA.Server.Models.CMU.GrupoContacto grupoContacto = default(SGPA.Server.Models.CMU.GrupoContacto))
        {
            var uri = new Uri(baseUri, $"GrupoContactos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", grupoContacto.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(grupoContacto), Encoding.UTF8, "application/json");

            OnUpdateGrupoContacto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportGrupoLugarRetiroCarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupolugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupolugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportGrupoLugarRetiroCarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupolugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupolugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetGrupoLugarRetiroCarnes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>> GetGrupoLugarRetiroCarnes(Query query)
        {
            return await GetGrupoLugarRetiroCarnes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>> GetGrupoLugarRetiroCarnes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"GrupoLugarRetiroCarnes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGrupoLugarRetiroCarnes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>>(response);
        }

        partial void OnCreateGrupoLugarRetiroCarne(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> CreateGrupoLugarRetiroCarne(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne grupoLugarRetiroCarne = default(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne))
        {
            var uri = new Uri(baseUri, $"GrupoLugarRetiroCarnes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(grupoLugarRetiroCarne), Encoding.UTF8, "application/json");

            OnCreateGrupoLugarRetiroCarne(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>(response);
        }

        partial void OnDeleteGrupoLugarRetiroCarne(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteGrupoLugarRetiroCarne(int id = default(int))
        {
            var uri = new Uri(baseUri, $"GrupoLugarRetiroCarnes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteGrupoLugarRetiroCarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetGrupoLugarRetiroCarneById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> GetGrupoLugarRetiroCarneById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"GrupoLugarRetiroCarnes({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGrupoLugarRetiroCarneById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>(response);
        }

        partial void OnUpdateGrupoLugarRetiroCarne(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateGrupoLugarRetiroCarne(int id = default(int), SGPA.Server.Models.CMU.GrupoLugarRetiroCarne grupoLugarRetiroCarne = default(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne))
        {
            var uri = new Uri(baseUri, $"GrupoLugarRetiroCarnes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", grupoLugarRetiroCarne.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(grupoLugarRetiroCarne), Encoding.UTF8, "application/json");

            OnUpdateGrupoLugarRetiroCarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportKpiDefinitionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpidefinitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpidefinitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportKpiDefinitionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpidefinitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpidefinitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetKpiDefinitions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiDefinition>> GetKpiDefinitions(Query query)
        {
            return await GetKpiDefinitions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiDefinition>> GetKpiDefinitions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"KpiDefinitions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiDefinitions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiDefinition>>(response);
        }

        partial void OnCreateKpiDefinition(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiDefinition> CreateKpiDefinition(SGPA.Server.Models.CMU.KpiDefinition kpiDefinition = default(SGPA.Server.Models.CMU.KpiDefinition))
        {
            var uri = new Uri(baseUri, $"KpiDefinitions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiDefinition), Encoding.UTF8, "application/json");

            OnCreateKpiDefinition(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiDefinition>(response);
        }

        partial void OnDeleteKpiDefinition(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteKpiDefinition(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiDefinitions({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteKpiDefinition(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetKpiDefinitionByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiDefinition> GetKpiDefinitionByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiDefinitions({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiDefinitionByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiDefinition>(response);
        }

        partial void OnUpdateKpiDefinition(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateKpiDefinition(Guid oid = default(Guid), SGPA.Server.Models.CMU.KpiDefinition kpiDefinition = default(SGPA.Server.Models.CMU.KpiDefinition))
        {
            var uri = new Uri(baseUri, $"KpiDefinitions({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", kpiDefinition.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiDefinition), Encoding.UTF8, "application/json");

            OnUpdateKpiDefinition(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportKpiHistoryItemsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpihistoryitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpihistoryitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportKpiHistoryItemsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpihistoryitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpihistoryitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetKpiHistoryItems(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiHistoryItem>> GetKpiHistoryItems(Query query)
        {
            return await GetKpiHistoryItems(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiHistoryItem>> GetKpiHistoryItems(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"KpiHistoryItems");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiHistoryItems(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiHistoryItem>>(response);
        }

        partial void OnCreateKpiHistoryItem(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiHistoryItem> CreateKpiHistoryItem(SGPA.Server.Models.CMU.KpiHistoryItem kpiHistoryItem = default(SGPA.Server.Models.CMU.KpiHistoryItem))
        {
            var uri = new Uri(baseUri, $"KpiHistoryItems");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiHistoryItem), Encoding.UTF8, "application/json");

            OnCreateKpiHistoryItem(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiHistoryItem>(response);
        }

        partial void OnDeleteKpiHistoryItem(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteKpiHistoryItem(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiHistoryItems({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteKpiHistoryItem(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetKpiHistoryItemByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiHistoryItem> GetKpiHistoryItemByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiHistoryItems({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiHistoryItemByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiHistoryItem>(response);
        }

        partial void OnUpdateKpiHistoryItem(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateKpiHistoryItem(Guid oid = default(Guid), SGPA.Server.Models.CMU.KpiHistoryItem kpiHistoryItem = default(SGPA.Server.Models.CMU.KpiHistoryItem))
        {
            var uri = new Uri(baseUri, $"KpiHistoryItems({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", kpiHistoryItem.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiHistoryItem), Encoding.UTF8, "application/json");

            OnUpdateKpiHistoryItem(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportKpiInstancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiinstances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiinstances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportKpiInstancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiinstances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiinstances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetKpiInstances(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiInstance>> GetKpiInstances(Query query)
        {
            return await GetKpiInstances(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiInstance>> GetKpiInstances(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"KpiInstances");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiInstances(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiInstance>>(response);
        }

        partial void OnCreateKpiInstance(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiInstance> CreateKpiInstance(SGPA.Server.Models.CMU.KpiInstance kpiInstance = default(SGPA.Server.Models.CMU.KpiInstance))
        {
            var uri = new Uri(baseUri, $"KpiInstances");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiInstance), Encoding.UTF8, "application/json");

            OnCreateKpiInstance(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiInstance>(response);
        }

        partial void OnDeleteKpiInstance(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteKpiInstance(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiInstances({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteKpiInstance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetKpiInstanceByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiInstance> GetKpiInstanceByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiInstances({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiInstanceByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiInstance>(response);
        }

        partial void OnUpdateKpiInstance(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateKpiInstance(Guid oid = default(Guid), SGPA.Server.Models.CMU.KpiInstance kpiInstance = default(SGPA.Server.Models.CMU.KpiInstance))
        {
            var uri = new Uri(baseUri, $"KpiInstances({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", kpiInstance.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiInstance), Encoding.UTF8, "application/json");

            OnUpdateKpiInstance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportKpiScorecardsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecards/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecards/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportKpiScorecardsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecards/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecards/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetKpiScorecards(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiScorecard>> GetKpiScorecards(Query query)
        {
            return await GetKpiScorecards(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiScorecard>> GetKpiScorecards(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"KpiScorecards");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiScorecards(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiScorecard>>(response);
        }

        partial void OnCreateKpiScorecard(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiScorecard> CreateKpiScorecard(SGPA.Server.Models.CMU.KpiScorecard kpiScorecard = default(SGPA.Server.Models.CMU.KpiScorecard))
        {
            var uri = new Uri(baseUri, $"KpiScorecards");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiScorecard), Encoding.UTF8, "application/json");

            OnCreateKpiScorecard(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiScorecard>(response);
        }

        partial void OnDeleteKpiScorecard(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteKpiScorecard(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiScorecards({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteKpiScorecard(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetKpiScorecardByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiScorecard> GetKpiScorecardByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiScorecards({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiScorecardByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiScorecard>(response);
        }

        partial void OnUpdateKpiScorecard(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateKpiScorecard(Guid oid = default(Guid), SGPA.Server.Models.CMU.KpiScorecard kpiScorecard = default(SGPA.Server.Models.CMU.KpiScorecard))
        {
            var uri = new Uri(baseUri, $"KpiScorecards({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", kpiScorecard.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiScorecard), Encoding.UTF8, "application/json");

            OnUpdateKpiScorecard(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportKpiscorecardscorecardsKpiinstanceindicatorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecardscorecardskpiinstanceindicators/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecardscorecardskpiinstanceindicators/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportKpiscorecardscorecardsKpiinstanceindicatorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecardscorecardskpiinstanceindicators/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecardscorecardskpiinstanceindicators/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetKpiscorecardscorecardsKpiinstanceindicators(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>> GetKpiscorecardscorecardsKpiinstanceindicators(Query query)
        {
            return await GetKpiscorecardscorecardsKpiinstanceindicators(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>> GetKpiscorecardscorecardsKpiinstanceindicators(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"KpiscorecardscorecardsKpiinstanceindicators");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiscorecardscorecardsKpiinstanceindicators(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>>(response);
        }

        partial void OnCreateKpiscorecardscorecardsKpiinstanceindicator(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> CreateKpiscorecardscorecardsKpiinstanceindicator(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator kpiscorecardscorecardsKpiinstanceindicator = default(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator))
        {
            var uri = new Uri(baseUri, $"KpiscorecardscorecardsKpiinstanceindicators");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiscorecardscorecardsKpiinstanceindicator), Encoding.UTF8, "application/json");

            OnCreateKpiscorecardscorecardsKpiinstanceindicator(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>(response);
        }

        partial void OnDeleteKpiscorecardscorecardsKpiinstanceindicator(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteKpiscorecardscorecardsKpiinstanceindicator(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiscorecardscorecardsKpiinstanceindicators({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteKpiscorecardscorecardsKpiinstanceindicator(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetKpiscorecardscorecardsKpiinstanceindicatorByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> GetKpiscorecardscorecardsKpiinstanceindicatorByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"KpiscorecardscorecardsKpiinstanceindicators({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetKpiscorecardscorecardsKpiinstanceindicatorByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>(response);
        }

        partial void OnUpdateKpiscorecardscorecardsKpiinstanceindicator(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateKpiscorecardscorecardsKpiinstanceindicator(Guid oid = default(Guid), SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator kpiscorecardscorecardsKpiinstanceindicator = default(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator))
        {
            var uri = new Uri(baseUri, $"KpiscorecardscorecardsKpiinstanceindicators({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", kpiscorecardscorecardsKpiinstanceindicator.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(kpiscorecardscorecardsKpiinstanceindicator), Encoding.UTF8, "application/json");

            OnUpdateKpiscorecardscorecardsKpiinstanceindicator(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportLugarRetiroCarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/lugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/lugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLugarRetiroCarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/lugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/lugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetLugarRetiroCarnes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.LugarRetiroCarne>> GetLugarRetiroCarnes(Query query)
        {
            return await GetLugarRetiroCarnes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.LugarRetiroCarne>> GetLugarRetiroCarnes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"LugarRetiroCarnes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLugarRetiroCarnes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.LugarRetiroCarne>>(response);
        }

        partial void OnCreateLugarRetiroCarne(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.LugarRetiroCarne> CreateLugarRetiroCarne(SGPA.Server.Models.CMU.LugarRetiroCarne lugarRetiroCarne = default(SGPA.Server.Models.CMU.LugarRetiroCarne))
        {
            var uri = new Uri(baseUri, $"LugarRetiroCarnes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(lugarRetiroCarne), Encoding.UTF8, "application/json");

            OnCreateLugarRetiroCarne(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.LugarRetiroCarne>(response);
        }

        partial void OnDeleteLugarRetiroCarne(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteLugarRetiroCarne(int id = default(int))
        {
            var uri = new Uri(baseUri, $"LugarRetiroCarnes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLugarRetiroCarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetLugarRetiroCarneById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.LugarRetiroCarne> GetLugarRetiroCarneById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"LugarRetiroCarnes({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLugarRetiroCarneById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.LugarRetiroCarne>(response);
        }

        partial void OnUpdateLugarRetiroCarne(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateLugarRetiroCarne(int id = default(int), SGPA.Server.Models.CMU.LugarRetiroCarne lugarRetiroCarne = default(SGPA.Server.Models.CMU.LugarRetiroCarne))
        {
            var uri = new Uri(baseUri, $"LugarRetiroCarnes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", lugarRetiroCarne.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(lugarRetiroCarne), Encoding.UTF8, "application/json");

            OnUpdateLugarRetiroCarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMensajePushesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMensajePushesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMensajePushes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajePush>> GetMensajePushes(Query query)
        {
            return await GetMensajePushes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajePush>> GetMensajePushes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MensajePushes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMensajePushes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajePush>>(response);
        }

        partial void OnCreateMensajePush(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MensajePush> CreateMensajePush(SGPA.Server.Models.CMU.MensajePush mensajePush = default(SGPA.Server.Models.CMU.MensajePush))
        {
            var uri = new Uri(baseUri, $"MensajePushes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mensajePush), Encoding.UTF8, "application/json");

            OnCreateMensajePush(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MensajePush>(response);
        }

        partial void OnDeleteMensajePush(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMensajePush(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"MensajePushes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMensajePush(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMensajePushByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MensajePush> GetMensajePushByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"MensajePushes({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMensajePushByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MensajePush>(response);
        }

        partial void OnUpdateMensajePush(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMensajePush(int oid = default(int), SGPA.Server.Models.CMU.MensajePush mensajePush = default(SGPA.Server.Models.CMU.MensajePush))
        {
            var uri = new Uri(baseUri, $"MensajePushes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", mensajePush.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mensajePush), Encoding.UTF8, "application/json");

            OnUpdateMensajePush(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMensajePushAddsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushadds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushadds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMensajePushAddsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushadds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushadds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMensajePushAdds(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajePushAdd>> GetMensajePushAdds(Query query)
        {
            return await GetMensajePushAdds(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajePushAdd>> GetMensajePushAdds(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MensajePushAdds");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMensajePushAdds(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajePushAdd>>(response);
        }

        partial void OnCreateMensajePushAdd(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MensajePushAdd> CreateMensajePushAdd(SGPA.Server.Models.CMU.MensajePushAdd mensajePushAdd = default(SGPA.Server.Models.CMU.MensajePushAdd))
        {
            var uri = new Uri(baseUri, $"MensajePushAdds");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mensajePushAdd), Encoding.UTF8, "application/json");

            OnCreateMensajePushAdd(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MensajePushAdd>(response);
        }

        partial void OnDeleteMensajePushAdd(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMensajePushAdd(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"MensajePushAdds({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMensajePushAdd(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMensajePushAddByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MensajePushAdd> GetMensajePushAddByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"MensajePushAdds({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMensajePushAddByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MensajePushAdd>(response);
        }

        partial void OnUpdateMensajePushAdd(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMensajePushAdd(int oid = default(int), SGPA.Server.Models.CMU.MensajePushAdd mensajePushAdd = default(SGPA.Server.Models.CMU.MensajePushAdd))
        {
            var uri = new Uri(baseUri, $"MensajePushAdds({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", mensajePushAdd.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mensajePushAdd), Encoding.UTF8, "application/json");

            OnUpdateMensajePushAdd(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMensajeSegmentosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajesegmentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajesegmentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMensajeSegmentosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajesegmentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajesegmentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMensajeSegmentos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajeSegmento>> GetMensajeSegmentos(Query query)
        {
            return await GetMensajeSegmentos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajeSegmento>> GetMensajeSegmentos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MensajeSegmentos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMensajeSegmentos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MensajeSegmento>>(response);
        }

        partial void OnCreateMensajeSegmento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MensajeSegmento> CreateMensajeSegmento(SGPA.Server.Models.CMU.MensajeSegmento mensajeSegmento = default(SGPA.Server.Models.CMU.MensajeSegmento))
        {
            var uri = new Uri(baseUri, $"MensajeSegmentos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mensajeSegmento), Encoding.UTF8, "application/json");

            OnCreateMensajeSegmento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MensajeSegmento>(response);
        }

        partial void OnDeleteMensajeSegmento(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMensajeSegmento(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"MensajeSegmentos({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMensajeSegmento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMensajeSegmentoByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MensajeSegmento> GetMensajeSegmentoByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"MensajeSegmentos({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMensajeSegmentoByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MensajeSegmento>(response);
        }

        partial void OnUpdateMensajeSegmento(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMensajeSegmento(int oid = default(int), SGPA.Server.Models.CMU.MensajeSegmento mensajeSegmento = default(SGPA.Server.Models.CMU.MensajeSegmento))
        {
            var uri = new Uri(baseUri, $"MensajeSegmentos({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", mensajeSegmento.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mensajeSegmento), Encoding.UTF8, "application/json");

            OnUpdateMensajeSegmento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportModuleInfosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/moduleinfos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/moduleinfos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportModuleInfosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/moduleinfos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/moduleinfos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetModuleInfos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ModuleInfo>> GetModuleInfos(Query query)
        {
            return await GetModuleInfos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ModuleInfo>> GetModuleInfos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ModuleInfos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetModuleInfos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ModuleInfo>>(response);
        }

        partial void OnCreateModuleInfo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ModuleInfo> CreateModuleInfo(SGPA.Server.Models.CMU.ModuleInfo moduleInfo = default(SGPA.Server.Models.CMU.ModuleInfo))
        {
            var uri = new Uri(baseUri, $"ModuleInfos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(moduleInfo), Encoding.UTF8, "application/json");

            OnCreateModuleInfo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ModuleInfo>(response);
        }

        partial void OnDeleteModuleInfo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteModuleInfo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"ModuleInfos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteModuleInfo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetModuleInfoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ModuleInfo> GetModuleInfoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"ModuleInfos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetModuleInfoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ModuleInfo>(response);
        }

        partial void OnUpdateModuleInfo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateModuleInfo(int id = default(int), SGPA.Server.Models.CMU.ModuleInfo moduleInfo = default(SGPA.Server.Models.CMU.ModuleInfo))
        {
            var uri = new Uri(baseUri, $"ModuleInfos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", moduleInfo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(moduleInfo), Encoding.UTF8, "application/json");

            OnUpdateModuleInfo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMovimientoCuentaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMovimientoCuentaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMovimientoCuenta(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentum>> GetMovimientoCuenta(Query query)
        {
            return await GetMovimientoCuenta(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentum>> GetMovimientoCuenta(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MovimientoCuenta");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoCuenta(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentum>>(response);
        }

        partial void OnCreateMovimientoCuentum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentum> CreateMovimientoCuentum(SGPA.Server.Models.CMU.MovimientoCuentum movimientoCuentum = default(SGPA.Server.Models.CMU.MovimientoCuentum))
        {
            var uri = new Uri(baseUri, $"MovimientoCuenta");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoCuentum), Encoding.UTF8, "application/json");

            OnCreateMovimientoCuentum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoCuentum>(response);
        }

        partial void OnDeleteMovimientoCuentum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMovimientoCuentum(int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoCuenta({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMovimientoCuentum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMovimientoCuentumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentum> GetMovimientoCuentumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoCuenta({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoCuentumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoCuentum>(response);
        }

        partial void OnUpdateMovimientoCuentum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMovimientoCuentum(int id = default(int), SGPA.Server.Models.CMU.MovimientoCuentum movimientoCuentum = default(SGPA.Server.Models.CMU.MovimientoCuentum))
        {
            var uri = new Uri(baseUri, $"MovimientoCuenta({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", movimientoCuentum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoCuentum), Encoding.UTF8, "application/json");

            OnUpdateMovimientoCuentum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMovimientoCuentaCuotaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentacuota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentacuota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMovimientoCuentaCuotaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentacuota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentacuota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMovimientoCuentaCuota(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>> GetMovimientoCuentaCuota(Query query)
        {
            return await GetMovimientoCuentaCuota(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>> GetMovimientoCuentaCuota(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaCuota");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoCuentaCuota(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>>(response);
        }

        partial void OnCreateMovimientoCuentaCuotum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> CreateMovimientoCuentaCuotum(SGPA.Server.Models.CMU.MovimientoCuentaCuotum movimientoCuentaCuotum = default(SGPA.Server.Models.CMU.MovimientoCuentaCuotum))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaCuota");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoCuentaCuotum), Encoding.UTF8, "application/json");

            OnCreateMovimientoCuentaCuotum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>(response);
        }

        partial void OnDeleteMovimientoCuentaCuotum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMovimientoCuentaCuotum(int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaCuota({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMovimientoCuentaCuotum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMovimientoCuentaCuotumById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> GetMovimientoCuentaCuotumById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaCuota({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoCuentaCuotumById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>(response);
        }

        partial void OnUpdateMovimientoCuentaCuotum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMovimientoCuentaCuotum(int id = default(int), SGPA.Server.Models.CMU.MovimientoCuentaCuotum movimientoCuentaCuotum = default(SGPA.Server.Models.CMU.MovimientoCuentaCuotum))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaCuota({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", movimientoCuentaCuotum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoCuentaCuotum), Encoding.UTF8, "application/json");

            OnUpdateMovimientoCuentaCuotum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMovimientoCuentaManualsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentamanuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentamanuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMovimientoCuentaManualsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentamanuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentamanuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMovimientoCuentaManuals(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentaManual>> GetMovimientoCuentaManuals(Query query)
        {
            return await GetMovimientoCuentaManuals(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentaManual>> GetMovimientoCuentaManuals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaManuals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoCuentaManuals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoCuentaManual>>(response);
        }

        partial void OnCreateMovimientoCuentaManual(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaManual> CreateMovimientoCuentaManual(SGPA.Server.Models.CMU.MovimientoCuentaManual movimientoCuentaManual = default(SGPA.Server.Models.CMU.MovimientoCuentaManual))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaManuals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoCuentaManual), Encoding.UTF8, "application/json");

            OnCreateMovimientoCuentaManual(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoCuentaManual>(response);
        }

        partial void OnDeleteMovimientoCuentaManual(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMovimientoCuentaManual(int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaManuals({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMovimientoCuentaManual(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMovimientoCuentaManualById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaManual> GetMovimientoCuentaManualById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaManuals({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoCuentaManualById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoCuentaManual>(response);
        }

        partial void OnUpdateMovimientoCuentaManual(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMovimientoCuentaManual(int id = default(int), SGPA.Server.Models.CMU.MovimientoCuentaManual movimientoCuentaManual = default(SGPA.Server.Models.CMU.MovimientoCuentaManual))
        {
            var uri = new Uri(baseUri, $"MovimientoCuentaManuals({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", movimientoCuentaManual.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoCuentaManual), Encoding.UTF8, "application/json");

            OnUpdateMovimientoCuentaManual(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMovimientoTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientotipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientotipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMovimientoTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientotipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientotipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMovimientoTipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoTipo>> GetMovimientoTipos(Query query)
        {
            return await GetMovimientoTipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoTipo>> GetMovimientoTipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MovimientoTipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoTipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MovimientoTipo>>(response);
        }

        partial void OnCreateMovimientoTipo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoTipo> CreateMovimientoTipo(SGPA.Server.Models.CMU.MovimientoTipo movimientoTipo = default(SGPA.Server.Models.CMU.MovimientoTipo))
        {
            var uri = new Uri(baseUri, $"MovimientoTipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoTipo), Encoding.UTF8, "application/json");

            OnCreateMovimientoTipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoTipo>(response);
        }

        partial void OnDeleteMovimientoTipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMovimientoTipo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoTipos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMovimientoTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMovimientoTipoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MovimientoTipo> GetMovimientoTipoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"MovimientoTipos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMovimientoTipoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MovimientoTipo>(response);
        }

        partial void OnUpdateMovimientoTipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMovimientoTipo(int id = default(int), SGPA.Server.Models.CMU.MovimientoTipo movimientoTipo = default(SGPA.Server.Models.CMU.MovimientoTipo))
        {
            var uri = new Uri(baseUri, $"MovimientoTipos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", movimientoTipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(movimientoTipo), Encoding.UTF8, "application/json");

            OnUpdateMovimientoTipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMyFileDataToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/myfiledata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/myfiledata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMyFileDataToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/myfiledata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/myfiledata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMyFileData(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MyFileDatum>> GetMyFileData(Query query)
        {
            return await GetMyFileData(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MyFileDatum>> GetMyFileData(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MyFileData");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMyFileData(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.MyFileDatum>>(response);
        }

        partial void OnCreateMyFileDatum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MyFileDatum> CreateMyFileDatum(SGPA.Server.Models.CMU.MyFileDatum myFileDatum = default(SGPA.Server.Models.CMU.MyFileDatum))
        {
            var uri = new Uri(baseUri, $"MyFileData");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(myFileDatum), Encoding.UTF8, "application/json");

            OnCreateMyFileDatum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MyFileDatum>(response);
        }

        partial void OnDeleteMyFileDatum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMyFileDatum(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"MyFileData({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMyFileDatum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMyFileDatumByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.MyFileDatum> GetMyFileDatumByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"MyFileData({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMyFileDatumByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.MyFileDatum>(response);
        }

        partial void OnUpdateMyFileDatum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMyFileDatum(Guid oid = default(Guid), SGPA.Server.Models.CMU.MyFileDatum myFileDatum = default(SGPA.Server.Models.CMU.MyFileDatum))
        {
            var uri = new Uri(baseUri, $"MyFileData({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", myFileDatum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(myFileDatum), Encoding.UTF8, "application/json");

            OnUpdateMyFileDatum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportOrigenMovimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/origenmovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/origenmovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOrigenMovimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/origenmovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/origenmovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetOrigenMovimientos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.OrigenMovimiento>> GetOrigenMovimientos(Query query)
        {
            return await GetOrigenMovimientos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.OrigenMovimiento>> GetOrigenMovimientos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"OrigenMovimientos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOrigenMovimientos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.OrigenMovimiento>>(response);
        }

        partial void OnCreateOrigenMovimiento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.OrigenMovimiento> CreateOrigenMovimiento(SGPA.Server.Models.CMU.OrigenMovimiento origenMovimiento = default(SGPA.Server.Models.CMU.OrigenMovimiento))
        {
            var uri = new Uri(baseUri, $"OrigenMovimientos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(origenMovimiento), Encoding.UTF8, "application/json");

            OnCreateOrigenMovimiento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.OrigenMovimiento>(response);
        }

        partial void OnDeleteOrigenMovimiento(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteOrigenMovimiento(int id = default(int))
        {
            var uri = new Uri(baseUri, $"OrigenMovimientos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOrigenMovimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetOrigenMovimientoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.OrigenMovimiento> GetOrigenMovimientoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"OrigenMovimientos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOrigenMovimientoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.OrigenMovimiento>(response);
        }

        partial void OnUpdateOrigenMovimiento(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateOrigenMovimiento(int id = default(int), SGPA.Server.Models.CMU.OrigenMovimiento origenMovimiento = default(SGPA.Server.Models.CMU.OrigenMovimiento))
        {
            var uri = new Uri(baseUri, $"OrigenMovimientos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", origenMovimiento.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(origenMovimiento), Encoding.UTF8, "application/json");

            OnUpdateOrigenMovimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportPaisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/pais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/pais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPaisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/pais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/pais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetPais(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Pai>> GetPais(Query query)
        {
            return await GetPais(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Pai>> GetPais(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Pais");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPais(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Pai>>(response);
        }

        partial void OnCreatePai(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Pai> CreatePai(SGPA.Server.Models.CMU.Pai pai = default(SGPA.Server.Models.CMU.Pai))
        {
            var uri = new Uri(baseUri, $"Pais");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(pai), Encoding.UTF8, "application/json");

            OnCreatePai(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Pai>(response);
        }

        partial void OnDeletePai(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeletePai(int codigo = default(int))
        {
            var uri = new Uri(baseUri, $"Pais({codigo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeletePai(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetPaiByCodigo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Pai> GetPaiByCodigo(string expand = default(string), int codigo = default(int))
        {
            var uri = new Uri(baseUri, $"Pais({codigo})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPaiByCodigo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Pai>(response);
        }

        partial void OnUpdatePai(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdatePai(int codigo = default(int), SGPA.Server.Models.CMU.Pai pai = default(SGPA.Server.Models.CMU.Pai))
        {
            var uri = new Uri(baseUri, $"Pais({codigo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", pai.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(pai), Encoding.UTF8, "application/json");

            OnUpdatePai(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportParametrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportParametrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetParametros(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Parametro>> GetParametros(Query query)
        {
            return await GetParametros(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Parametro>> GetParametros(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Parametros");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetParametros(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Parametro>>(response);
        }

        partial void OnCreateParametro(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Parametro> CreateParametro(SGPA.Server.Models.CMU.Parametro parametro = default(SGPA.Server.Models.CMU.Parametro))
        {
            var uri = new Uri(baseUri, $"Parametros");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(parametro), Encoding.UTF8, "application/json");

            OnCreateParametro(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Parametro>(response);
        }

        partial void OnDeleteParametro(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteParametro(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Parametros({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteParametro(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetParametroById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Parametro> GetParametroById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Parametros({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetParametroById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Parametro>(response);
        }

        partial void OnUpdateParametro(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateParametro(int id = default(int), SGPA.Server.Models.CMU.Parametro parametro = default(SGPA.Server.Models.CMU.Parametro))
        {
            var uri = new Uri(baseUri, $"Parametros({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", parametro.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(parametro), Encoding.UTF8, "application/json");

            OnUpdateParametro(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Region>> GetRegions(Query query)
        {
            return await GetRegions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Region>> GetRegions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Regions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Region>>(response);
        }

        partial void OnCreateRegion(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Region> CreateRegion(SGPA.Server.Models.CMU.Region region = default(SGPA.Server.Models.CMU.Region))
        {
            var uri = new Uri(baseUri, $"Regions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(region), Encoding.UTF8, "application/json");

            OnCreateRegion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Region>(response);
        }

        partial void OnDeleteRegion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegion(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Regions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegionById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Region> GetRegionById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Regions({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegionById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Region>(response);
        }

        partial void OnUpdateRegion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegion(int id = default(int), SGPA.Server.Models.CMU.Region region = default(SGPA.Server.Models.CMU.Region))
        {
            var uri = new Uri(baseUri, $"Regions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", region.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(region), Encoding.UTF8, "application/json");

            OnUpdateRegion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegionalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegionalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegionals(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Regional>> GetRegionals(Query query)
        {
            return await GetRegionals(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Regional>> GetRegionals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Regionals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegionals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Regional>>(response);
        }

        partial void OnCreateRegional(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Regional> CreateRegional(SGPA.Server.Models.CMU.Regional regional = default(SGPA.Server.Models.CMU.Regional))
        {
            var uri = new Uri(baseUri, $"Regionals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regional), Encoding.UTF8, "application/json");

            OnCreateRegional(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Regional>(response);
        }

        partial void OnDeleteRegional(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegional(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Regionals({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegional(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegionalById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Regional> GetRegionalById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Regionals({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegionalById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Regional>(response);
        }

        partial void OnUpdateRegional(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegional(int id = default(int), SGPA.Server.Models.CMU.Regional regional = default(SGPA.Server.Models.CMU.Regional))
        {
            var uri = new Uri(baseUri, $"Regionals({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", regional.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regional), Encoding.UTF8, "application/json");

            OnUpdateRegional(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegionalregionalesCuentabancariacuentabancariaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionalregionalescuentabancariacuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionalregionalescuentabancariacuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegionalregionalesCuentabancariacuentabancariaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionalregionalescuentabancariacuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionalregionalescuentabancariacuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegionalregionalesCuentabancariacuentabancaria(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>> GetRegionalregionalesCuentabancariacuentabancaria(Query query)
        {
            return await GetRegionalregionalesCuentabancariacuentabancaria(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>> GetRegionalregionalesCuentabancariacuentabancaria(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RegionalregionalesCuentabancariacuentabancaria");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegionalregionalesCuentabancariacuentabancaria(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>>(response);
        }

        partial void OnCreateRegionalregionalesCuentabancariacuentabancaria(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> CreateRegionalregionalesCuentabancariacuentabancaria(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria regionalregionalesCuentabancariacuentabancaria = default(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria))
        {
            var uri = new Uri(baseUri, $"RegionalregionalesCuentabancariacuentabancaria");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regionalregionalesCuentabancariacuentabancaria), Encoding.UTF8, "application/json");

            OnCreateRegionalregionalesCuentabancariacuentabancaria(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>(response);
        }

        partial void OnDeleteRegionalregionalesCuentabancariacuentabancaria(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegionalregionalesCuentabancariacuentabancaria(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RegionalregionalesCuentabancariacuentabancaria({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegionalregionalesCuentabancariacuentabancaria(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegionalregionalesCuentabancariacuentabancariaByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> GetRegionalregionalesCuentabancariacuentabancariaByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RegionalregionalesCuentabancariacuentabancaria({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegionalregionalesCuentabancariacuentabancariaByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>(response);
        }

        partial void OnUpdateRegionalregionalesCuentabancariacuentabancaria(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegionalregionalesCuentabancariacuentabancaria(int oid = default(int), SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria regionalregionalesCuentabancariacuentabancaria = default(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria))
        {
            var uri = new Uri(baseUri, $"RegionalregionalesCuentabancariacuentabancaria({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", regionalregionalesCuentabancariacuentabancaria.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(regionalregionalesCuentabancariacuentabancaria), Encoding.UTF8, "application/json");

            OnUpdateRegionalregionalesCuentabancariacuentabancaria(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegistroColegiadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegistroColegiadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegistroColegiados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiado>> GetRegistroColegiados(Query query)
        {
            return await GetRegistroColegiados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiado>> GetRegistroColegiados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RegistroColegiados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegistroColegiados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiado>>(response);
        }

        partial void OnCreateRegistroColegiado(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiado> CreateRegistroColegiado(SGPA.Server.Models.CMU.RegistroColegiado registroColegiado = default(SGPA.Server.Models.CMU.RegistroColegiado))
        {
            var uri = new Uri(baseUri, $"RegistroColegiados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(registroColegiado), Encoding.UTF8, "application/json");

            OnCreateRegistroColegiado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegistroColegiado>(response);
        }

        partial void OnDeleteRegistroColegiado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegistroColegiado(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RegistroColegiados({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegistroColegiado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegistroColegiadoByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiado> GetRegistroColegiadoByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RegistroColegiados({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegistroColegiadoByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegistroColegiado>(response);
        }

        partial void OnUpdateRegistroColegiado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegistroColegiado(int oid = default(int), SGPA.Server.Models.CMU.RegistroColegiado registroColegiado = default(SGPA.Server.Models.CMU.RegistroColegiado))
        {
            var uri = new Uri(baseUri, $"RegistroColegiados({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", registroColegiado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(registroColegiado), Encoding.UTF8, "application/json");

            OnUpdateRegistroColegiado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegistroColegiadoNotificacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadonotificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadonotificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegistroColegiadoNotificacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadonotificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadonotificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegistroColegiadoNotificacions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>> GetRegistroColegiadoNotificacions(Query query)
        {
            return await GetRegistroColegiadoNotificacions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>> GetRegistroColegiadoNotificacions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoNotificacions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegistroColegiadoNotificacions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>>(response);
        }

        partial void OnCreateRegistroColegiadoNotificacion(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> CreateRegistroColegiadoNotificacion(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion registroColegiadoNotificacion = default(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoNotificacions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(registroColegiadoNotificacion), Encoding.UTF8, "application/json");

            OnCreateRegistroColegiadoNotificacion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>(response);
        }

        partial void OnDeleteRegistroColegiadoNotificacion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegistroColegiadoNotificacion(int id = default(int))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoNotificacions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegistroColegiadoNotificacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegistroColegiadoNotificacionById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> GetRegistroColegiadoNotificacionById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoNotificacions({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegistroColegiadoNotificacionById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>(response);
        }

        partial void OnUpdateRegistroColegiadoNotificacion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegistroColegiadoNotificacion(int id = default(int), SGPA.Server.Models.CMU.RegistroColegiadoNotificacion registroColegiadoNotificacion = default(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoNotificacions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", registroColegiadoNotificacion.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(registroColegiadoNotificacion), Encoding.UTF8, "application/json");

            OnUpdateRegistroColegiadoNotificacion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRegistroColegiadoRechazoParamsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadorechazoparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadorechazoparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRegistroColegiadoRechazoParamsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadorechazoparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadorechazoparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRegistroColegiadoRechazoParams(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>> GetRegistroColegiadoRechazoParams(Query query)
        {
            return await GetRegistroColegiadoRechazoParams(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>> GetRegistroColegiadoRechazoParams(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoRechazoParams");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegistroColegiadoRechazoParams(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>>(response);
        }

        partial void OnCreateRegistroColegiadoRechazoParam(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> CreateRegistroColegiadoRechazoParam(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam registroColegiadoRechazoParam = default(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoRechazoParams");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(registroColegiadoRechazoParam), Encoding.UTF8, "application/json");

            OnCreateRegistroColegiadoRechazoParam(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>(response);
        }

        partial void OnDeleteRegistroColegiadoRechazoParam(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRegistroColegiadoRechazoParam(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoRechazoParams({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRegistroColegiadoRechazoParam(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRegistroColegiadoRechazoParamByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> GetRegistroColegiadoRechazoParamByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoRechazoParams({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRegistroColegiadoRechazoParamByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>(response);
        }

        partial void OnUpdateRegistroColegiadoRechazoParam(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRegistroColegiadoRechazoParam(int oid = default(int), SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam registroColegiadoRechazoParam = default(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam))
        {
            var uri = new Uri(baseUri, $"RegistroColegiadoRechazoParams({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", registroColegiadoRechazoParam.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(registroColegiadoRechazoParam), Encoding.UTF8, "application/json");

            OnUpdateRegistroColegiadoRechazoParam(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportReportDataToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportReportDataToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetReportData(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ReportDatum>> GetReportData(Query query)
        {
            return await GetReportData(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ReportDatum>> GetReportData(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ReportData");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetReportData(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ReportDatum>>(response);
        }

        partial void OnCreateReportDatum(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ReportDatum> CreateReportDatum(SGPA.Server.Models.CMU.ReportDatum reportDatum = default(SGPA.Server.Models.CMU.ReportDatum))
        {
            var uri = new Uri(baseUri, $"ReportData");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(reportDatum), Encoding.UTF8, "application/json");

            OnCreateReportDatum(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ReportDatum>(response);
        }

        partial void OnDeleteReportDatum(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteReportDatum(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"ReportData({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteReportDatum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetReportDatumByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ReportDatum> GetReportDatumByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"ReportData({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetReportDatumByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ReportDatum>(response);
        }

        partial void OnUpdateReportDatum(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateReportDatum(int oid = default(int), SGPA.Server.Models.CMU.ReportDatum reportDatum = default(SGPA.Server.Models.CMU.ReportDatum))
        {
            var uri = new Uri(baseUri, $"ReportData({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", reportDatum.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(reportDatum), Encoding.UTF8, "application/json");

            OnUpdateReportDatum(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportReportDataV2SToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdatav2s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdatav2s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportReportDataV2SToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdatav2s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdatav2s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetReportDataV2S(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ReportDataV2>> GetReportDataV2S(Query query)
        {
            return await GetReportDataV2S(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ReportDataV2>> GetReportDataV2S(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ReportDataV2S");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetReportDataV2S(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.ReportDataV2>>(response);
        }

        partial void OnCreateReportDataV2(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ReportDataV2> CreateReportDataV2(SGPA.Server.Models.CMU.ReportDataV2 reportDataV2 = default(SGPA.Server.Models.CMU.ReportDataV2))
        {
            var uri = new Uri(baseUri, $"ReportDataV2S");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(reportDataV2), Encoding.UTF8, "application/json");

            OnCreateReportDataV2(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ReportDataV2>(response);
        }

        partial void OnDeleteReportDataV2(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteReportDataV2(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"ReportDataV2S({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteReportDataV2(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetReportDataV2ByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.ReportDataV2> GetReportDataV2ByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"ReportDataV2S({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetReportDataV2ByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.ReportDataV2>(response);
        }

        partial void OnUpdateReportDataV2(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateReportDataV2(Guid oid = default(Guid), SGPA.Server.Models.CMU.ReportDataV2 reportDataV2 = default(SGPA.Server.Models.CMU.ReportDataV2))
        {
            var uri = new Uri(baseUri, $"ReportDataV2S({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", reportDataV2.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(reportDataV2), Encoding.UTF8, "application/json");

            OnUpdateReportDataV2(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRolsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRolsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRols(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Rol>> GetRols(Query query)
        {
            return await GetRols(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Rol>> GetRols(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Rols");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRols(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Rol>>(response);
        }

        partial void OnCreateRol(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Rol> CreateRol(SGPA.Server.Models.CMU.Rol rol = default(SGPA.Server.Models.CMU.Rol))
        {
            var uri = new Uri(baseUri, $"Rols");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(rol), Encoding.UTF8, "application/json");

            OnCreateRol(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Rol>(response);
        }

        partial void OnDeleteRol(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRol(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"Rols({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRol(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRolByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Rol> GetRolByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"Rols({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRolByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Rol>(response);
        }

        partial void OnUpdateRol(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRol(Guid oid = default(Guid), SGPA.Server.Models.CMU.Rol rol = default(SGPA.Server.Models.CMU.Rol))
        {
            var uri = new Uri(baseUri, $"Rols({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", rol.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(rol), Encoding.UTF8, "application/json");

            OnUpdateRol(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRolrolesMovimientotipomovimientostiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rolrolesmovimientotipomovimientostipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rolrolesmovimientotipomovimientostipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRolrolesMovimientotipomovimientostiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rolrolesmovimientotipomovimientostipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rolrolesmovimientotipomovimientostipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRolrolesMovimientotipomovimientostipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>> GetRolrolesMovimientotipomovimientostipos(Query query)
        {
            return await GetRolrolesMovimientotipomovimientostipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>> GetRolrolesMovimientotipomovimientostipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RolrolesMovimientotipomovimientostipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRolrolesMovimientotipomovimientostipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>>(response);
        }

        partial void OnCreateRolrolesMovimientotipomovimientostipo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> CreateRolrolesMovimientotipomovimientostipo(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo rolrolesMovimientotipomovimientostipo = default(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo))
        {
            var uri = new Uri(baseUri, $"RolrolesMovimientotipomovimientostipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(rolrolesMovimientotipomovimientostipo), Encoding.UTF8, "application/json");

            OnCreateRolrolesMovimientotipomovimientostipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>(response);
        }

        partial void OnDeleteRolrolesMovimientotipomovimientostipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRolrolesMovimientotipomovimientostipo(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RolrolesMovimientotipomovimientostipos({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRolrolesMovimientotipomovimientostipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRolrolesMovimientotipomovimientostipoByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> GetRolrolesMovimientotipomovimientostipoByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"RolrolesMovimientotipomovimientostipos({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRolrolesMovimientotipomovimientostipoByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>(response);
        }

        partial void OnUpdateRolrolesMovimientotipomovimientostipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRolrolesMovimientotipomovimientostipo(int oid = default(int), SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo rolrolesMovimientotipomovimientostipo = default(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo))
        {
            var uri = new Uri(baseUri, $"RolrolesMovimientotipomovimientostipos({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", rolrolesMovimientotipomovimientostipo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(rolrolesMovimientotipomovimientostipo), Encoding.UTF8, "application/json");

            OnUpdateRolrolesMovimientotipomovimientostipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalaCmusToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salacmus/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salacmus/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalaCmusToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salacmus/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salacmus/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalaCmus(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaCmu>> GetSalaCmus(Query query)
        {
            return await GetSalaCmus(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaCmu>> GetSalaCmus(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalaCmus");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaCmus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaCmu>>(response);
        }

        partial void OnCreateSalaCmu(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaCmu> CreateSalaCmu(SGPA.Server.Models.CMU.SalaCmu salaCmu = default(SGPA.Server.Models.CMU.SalaCmu))
        {
            var uri = new Uri(baseUri, $"SalaCmus");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaCmu), Encoding.UTF8, "application/json");

            OnCreateSalaCmu(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaCmu>(response);
        }

        partial void OnDeleteSalaCmu(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalaCmu(int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaCmus({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalaCmu(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalaCmuById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaCmu> GetSalaCmuById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaCmus({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaCmuById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaCmu>(response);
        }

        partial void OnUpdateSalaCmu(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalaCmu(int id = default(int), SGPA.Server.Models.CMU.SalaCmu salaCmu = default(SGPA.Server.Models.CMU.SalaCmu))
        {
            var uri = new Uri(baseUri, $"SalaCmus({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", salaCmu.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaCmu), Encoding.UTF8, "application/json");

            OnUpdateSalaCmu(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalaOrganizadorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salaorganizadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salaorganizadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalaOrganizadorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salaorganizadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salaorganizadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalaOrganizadors(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaOrganizador>> GetSalaOrganizadors(Query query)
        {
            return await GetSalaOrganizadors(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaOrganizador>> GetSalaOrganizadors(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalaOrganizadors");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaOrganizadors(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaOrganizador>>(response);
        }

        partial void OnCreateSalaOrganizador(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaOrganizador> CreateSalaOrganizador(SGPA.Server.Models.CMU.SalaOrganizador salaOrganizador = default(SGPA.Server.Models.CMU.SalaOrganizador))
        {
            var uri = new Uri(baseUri, $"SalaOrganizadors");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaOrganizador), Encoding.UTF8, "application/json");

            OnCreateSalaOrganizador(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaOrganizador>(response);
        }

        partial void OnDeleteSalaOrganizador(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalaOrganizador(int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaOrganizadors({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalaOrganizador(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalaOrganizadorById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaOrganizador> GetSalaOrganizadorById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaOrganizadors({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaOrganizadorById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaOrganizador>(response);
        }

        partial void OnUpdateSalaOrganizador(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalaOrganizador(int id = default(int), SGPA.Server.Models.CMU.SalaOrganizador salaOrganizador = default(SGPA.Server.Models.CMU.SalaOrganizador))
        {
            var uri = new Uri(baseUri, $"SalaOrganizadors({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", salaOrganizador.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaOrganizador), Encoding.UTF8, "application/json");

            OnUpdateSalaOrganizador(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalaReservasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalaReservasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalaReservas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaReserva>> GetSalaReservas(Query query)
        {
            return await GetSalaReservas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaReserva>> GetSalaReservas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalaReservas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaReservas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaReserva>>(response);
        }

        partial void OnCreateSalaReserva(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaReserva> CreateSalaReserva(SGPA.Server.Models.CMU.SalaReserva salaReserva = default(SGPA.Server.Models.CMU.SalaReserva))
        {
            var uri = new Uri(baseUri, $"SalaReservas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaReserva), Encoding.UTF8, "application/json");

            OnCreateSalaReserva(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaReserva>(response);
        }

        partial void OnDeleteSalaReserva(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalaReserva(int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaReservas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalaReserva(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalaReservaById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaReserva> GetSalaReservaById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaReservas({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaReservaById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaReserva>(response);
        }

        partial void OnUpdateSalaReserva(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalaReserva(int id = default(int), SGPA.Server.Models.CMU.SalaReserva salaReserva = default(SGPA.Server.Models.CMU.SalaReserva))
        {
            var uri = new Uri(baseUri, $"SalaReservas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", salaReserva.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaReserva), Encoding.UTF8, "application/json");

            OnUpdateSalaReserva(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalaReservaRegistrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservaregistros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservaregistros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalaReservaRegistrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservaregistros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservaregistros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalaReservaRegistros(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaReservaRegistro>> GetSalaReservaRegistros(Query query)
        {
            return await GetSalaReservaRegistros(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaReservaRegistro>> GetSalaReservaRegistros(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalaReservaRegistros");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaReservaRegistros(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SalaReservaRegistro>>(response);
        }

        partial void OnCreateSalaReservaRegistro(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaReservaRegistro> CreateSalaReservaRegistro(SGPA.Server.Models.CMU.SalaReservaRegistro salaReservaRegistro = default(SGPA.Server.Models.CMU.SalaReservaRegistro))
        {
            var uri = new Uri(baseUri, $"SalaReservaRegistros");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaReservaRegistro), Encoding.UTF8, "application/json");

            OnCreateSalaReservaRegistro(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaReservaRegistro>(response);
        }

        partial void OnDeleteSalaReservaRegistro(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalaReservaRegistro(int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaReservaRegistros({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalaReservaRegistro(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalaReservaRegistroById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SalaReservaRegistro> GetSalaReservaRegistroById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"SalaReservaRegistros({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalaReservaRegistroById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SalaReservaRegistro>(response);
        }

        partial void OnUpdateSalaReservaRegistro(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalaReservaRegistro(int id = default(int), SGPA.Server.Models.CMU.SalaReservaRegistro salaReservaRegistro = default(SGPA.Server.Models.CMU.SalaReservaRegistro))
        {
            var uri = new Uri(baseUri, $"SalaReservaRegistros({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", salaReservaRegistro.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salaReservaRegistro), Encoding.UTF8, "application/json");

            OnUpdateSalaReservaRegistro(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemMemberPermissionsObjectsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemmemberpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemmemberpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemMemberPermissionsObjectsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemmemberpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemmemberpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSecuritySystemMemberPermissionsObjects(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>> GetSecuritySystemMemberPermissionsObjects(Query query)
        {
            return await GetSecuritySystemMemberPermissionsObjects(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>> GetSecuritySystemMemberPermissionsObjects(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SecuritySystemMemberPermissionsObjects");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemMemberPermissionsObjects(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>>(response);
        }

        partial void OnCreateSecuritySystemMemberPermissionsObject(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> CreateSecuritySystemMemberPermissionsObject(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject securitySystemMemberPermissionsObject = default(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject))
        {
            var uri = new Uri(baseUri, $"SecuritySystemMemberPermissionsObjects");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemMemberPermissionsObject), Encoding.UTF8, "application/json");

            OnCreateSecuritySystemMemberPermissionsObject(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>(response);
        }

        partial void OnDeleteSecuritySystemMemberPermissionsObject(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSecuritySystemMemberPermissionsObject(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemMemberPermissionsObjects({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSecuritySystemMemberPermissionsObject(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSecuritySystemMemberPermissionsObjectByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> GetSecuritySystemMemberPermissionsObjectByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemMemberPermissionsObjects({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemMemberPermissionsObjectByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>(response);
        }

        partial void OnUpdateSecuritySystemMemberPermissionsObject(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSecuritySystemMemberPermissionsObject(Guid oid = default(Guid), SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject securitySystemMemberPermissionsObject = default(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject))
        {
            var uri = new Uri(baseUri, $"SecuritySystemMemberPermissionsObjects({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", securitySystemMemberPermissionsObject.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemMemberPermissionsObject), Encoding.UTF8, "application/json");

            OnUpdateSecuritySystemMemberPermissionsObject(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemObjectPermissionsObjectsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemobjectpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemobjectpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemObjectPermissionsObjectsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemobjectpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemobjectpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSecuritySystemObjectPermissionsObjects(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>> GetSecuritySystemObjectPermissionsObjects(Query query)
        {
            return await GetSecuritySystemObjectPermissionsObjects(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>> GetSecuritySystemObjectPermissionsObjects(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SecuritySystemObjectPermissionsObjects");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemObjectPermissionsObjects(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>>(response);
        }

        partial void OnCreateSecuritySystemObjectPermissionsObject(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> CreateSecuritySystemObjectPermissionsObject(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject securitySystemObjectPermissionsObject = default(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject))
        {
            var uri = new Uri(baseUri, $"SecuritySystemObjectPermissionsObjects");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemObjectPermissionsObject), Encoding.UTF8, "application/json");

            OnCreateSecuritySystemObjectPermissionsObject(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>(response);
        }

        partial void OnDeleteSecuritySystemObjectPermissionsObject(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSecuritySystemObjectPermissionsObject(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemObjectPermissionsObjects({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSecuritySystemObjectPermissionsObject(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSecuritySystemObjectPermissionsObjectByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> GetSecuritySystemObjectPermissionsObjectByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemObjectPermissionsObjects({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemObjectPermissionsObjectByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>(response);
        }

        partial void OnUpdateSecuritySystemObjectPermissionsObject(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSecuritySystemObjectPermissionsObject(Guid oid = default(Guid), SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject securitySystemObjectPermissionsObject = default(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject))
        {
            var uri = new Uri(baseUri, $"SecuritySystemObjectPermissionsObjects({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", securitySystemObjectPermissionsObject.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemObjectPermissionsObject), Encoding.UTF8, "application/json");

            OnUpdateSecuritySystemObjectPermissionsObject(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSecuritySystemRoles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemRole>> GetSecuritySystemRoles(Query query)
        {
            return await GetSecuritySystemRoles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemRole>> GetSecuritySystemRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SecuritySystemRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemRole>>(response);
        }

        partial void OnCreateSecuritySystemRole(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemRole> CreateSecuritySystemRole(SGPA.Server.Models.CMU.SecuritySystemRole securitySystemRole = default(SGPA.Server.Models.CMU.SecuritySystemRole))
        {
            var uri = new Uri(baseUri, $"SecuritySystemRoles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemRole), Encoding.UTF8, "application/json");

            OnCreateSecuritySystemRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemRole>(response);
        }

        partial void OnDeleteSecuritySystemRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSecuritySystemRole(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemRoles({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSecuritySystemRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSecuritySystemRoleByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemRole> GetSecuritySystemRoleByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemRoles({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemRoleByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemRole>(response);
        }

        partial void OnUpdateSecuritySystemRole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSecuritySystemRole(Guid oid = default(Guid), SGPA.Server.Models.CMU.SecuritySystemRole securitySystemRole = default(SGPA.Server.Models.CMU.SecuritySystemRole))
        {
            var uri = new Uri(baseUri, $"SecuritySystemRoles({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", securitySystemRole.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemRole), Encoding.UTF8, "application/json");

            OnUpdateSecuritySystemRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSecuritysystemroleparentrolesSecuritysystemrolechildrolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSecuritysystemroleparentrolesSecuritysystemrolechildrolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSecuritysystemroleparentrolesSecuritysystemrolechildroles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>> GetSecuritysystemroleparentrolesSecuritysystemrolechildroles(Query query)
        {
            return await GetSecuritysystemroleparentrolesSecuritysystemrolechildroles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>> GetSecuritysystemroleparentrolesSecuritysystemrolechildroles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SecuritysystemroleparentrolesSecuritysystemrolechildroles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritysystemroleparentrolesSecuritysystemrolechildroles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>>(response);
        }

        partial void OnCreateSecuritysystemroleparentrolesSecuritysystemrolechildrole(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> CreateSecuritysystemroleparentrolesSecuritysystemrolechildrole(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole securitysystemroleparentrolesSecuritysystemrolechildrole = default(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole))
        {
            var uri = new Uri(baseUri, $"SecuritysystemroleparentrolesSecuritysystemrolechildroles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitysystemroleparentrolesSecuritysystemrolechildrole), Encoding.UTF8, "application/json");

            OnCreateSecuritysystemroleparentrolesSecuritysystemrolechildrole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>(response);
        }

        partial void OnDeleteSecuritysystemroleparentrolesSecuritysystemrolechildrole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritysystemroleparentrolesSecuritysystemrolechildroles({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSecuritysystemroleparentrolesSecuritysystemrolechildrole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSecuritysystemroleparentrolesSecuritysystemrolechildroleByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> GetSecuritysystemroleparentrolesSecuritysystemrolechildroleByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritysystemroleparentrolesSecuritysystemrolechildroles({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritysystemroleparentrolesSecuritysystemrolechildroleByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>(response);
        }

        partial void OnUpdateSecuritysystemroleparentrolesSecuritysystemrolechildrole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid oid = default(Guid), SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole securitysystemroleparentrolesSecuritysystemrolechildrole = default(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole))
        {
            var uri = new Uri(baseUri, $"SecuritysystemroleparentrolesSecuritysystemrolechildroles({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", securitysystemroleparentrolesSecuritysystemrolechildrole.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitysystemroleparentrolesSecuritysystemrolechildrole), Encoding.UTF8, "application/json");

            OnUpdateSecuritysystemroleparentrolesSecuritysystemrolechildrole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemTypePermissionsObjectsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemtypepermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemtypepermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemTypePermissionsObjectsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemtypepermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemtypepermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSecuritySystemTypePermissionsObjects(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>> GetSecuritySystemTypePermissionsObjects(Query query)
        {
            return await GetSecuritySystemTypePermissionsObjects(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>> GetSecuritySystemTypePermissionsObjects(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SecuritySystemTypePermissionsObjects");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemTypePermissionsObjects(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>>(response);
        }

        partial void OnCreateSecuritySystemTypePermissionsObject(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> CreateSecuritySystemTypePermissionsObject(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject securitySystemTypePermissionsObject = default(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject))
        {
            var uri = new Uri(baseUri, $"SecuritySystemTypePermissionsObjects");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemTypePermissionsObject), Encoding.UTF8, "application/json");

            OnCreateSecuritySystemTypePermissionsObject(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>(response);
        }

        partial void OnDeleteSecuritySystemTypePermissionsObject(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSecuritySystemTypePermissionsObject(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemTypePermissionsObjects({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSecuritySystemTypePermissionsObject(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSecuritySystemTypePermissionsObjectByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> GetSecuritySystemTypePermissionsObjectByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemTypePermissionsObjects({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemTypePermissionsObjectByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>(response);
        }

        partial void OnUpdateSecuritySystemTypePermissionsObject(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSecuritySystemTypePermissionsObject(Guid oid = default(Guid), SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject securitySystemTypePermissionsObject = default(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject))
        {
            var uri = new Uri(baseUri, $"SecuritySystemTypePermissionsObjects({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", securitySystemTypePermissionsObject.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemTypePermissionsObject), Encoding.UTF8, "application/json");

            OnUpdateSecuritySystemTypePermissionsObject(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSecuritySystemUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSecuritySystemUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemUser>> GetSecuritySystemUsers(Query query)
        {
            return await GetSecuritySystemUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemUser>> GetSecuritySystemUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SecuritySystemUsers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritySystemUser>>(response);
        }

        partial void OnCreateSecuritySystemUser(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemUser> CreateSecuritySystemUser(SGPA.Server.Models.CMU.SecuritySystemUser securitySystemUser = default(SGPA.Server.Models.CMU.SecuritySystemUser))
        {
            var uri = new Uri(baseUri, $"SecuritySystemUsers");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemUser), Encoding.UTF8, "application/json");

            OnCreateSecuritySystemUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemUser>(response);
        }

        partial void OnDeleteSecuritySystemUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSecuritySystemUser(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemUsers({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSecuritySystemUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSecuritySystemUserByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemUser> GetSecuritySystemUserByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritySystemUsers({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritySystemUserByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritySystemUser>(response);
        }

        partial void OnUpdateSecuritySystemUser(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSecuritySystemUser(Guid oid = default(Guid), SGPA.Server.Models.CMU.SecuritySystemUser securitySystemUser = default(SGPA.Server.Models.CMU.SecuritySystemUser))
        {
            var uri = new Uri(baseUri, $"SecuritySystemUsers({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", securitySystemUser.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitySystemUser), Encoding.UTF8, "application/json");

            OnUpdateSecuritySystemUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSecuritysystemuserusersSecuritysystemrolerolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemuseruserssecuritysystemroleroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemuseruserssecuritysystemroleroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSecuritysystemuserusersSecuritysystemrolerolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemuseruserssecuritysystemroleroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemuseruserssecuritysystemroleroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSecuritysystemuserusersSecuritysystemroleroles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>> GetSecuritysystemuserusersSecuritysystemroleroles(Query query)
        {
            return await GetSecuritysystemuserusersSecuritysystemroleroles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>> GetSecuritysystemuserusersSecuritysystemroleroles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SecuritysystemuserusersSecuritysystemroleroles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritysystemuserusersSecuritysystemroleroles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>>(response);
        }

        partial void OnCreateSecuritysystemuserusersSecuritysystemrolerole(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> CreateSecuritysystemuserusersSecuritysystemrolerole(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole securitysystemuserusersSecuritysystemrolerole = default(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole))
        {
            var uri = new Uri(baseUri, $"SecuritysystemuserusersSecuritysystemroleroles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitysystemuserusersSecuritysystemrolerole), Encoding.UTF8, "application/json");

            OnCreateSecuritysystemuserusersSecuritysystemrolerole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>(response);
        }

        partial void OnDeleteSecuritysystemuserusersSecuritysystemrolerole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSecuritysystemuserusersSecuritysystemrolerole(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritysystemuserusersSecuritysystemroleroles({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSecuritysystemuserusersSecuritysystemrolerole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSecuritysystemuserusersSecuritysystemroleroleByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> GetSecuritysystemuserusersSecuritysystemroleroleByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SecuritysystemuserusersSecuritysystemroleroles({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSecuritysystemuserusersSecuritysystemroleroleByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>(response);
        }

        partial void OnUpdateSecuritysystemuserusersSecuritysystemrolerole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSecuritysystemuserusersSecuritysystemrolerole(Guid oid = default(Guid), SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole securitysystemuserusersSecuritysystemrolerole = default(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole))
        {
            var uri = new Uri(baseUri, $"SecuritysystemuserusersSecuritysystemroleroles({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", securitysystemuserusersSecuritysystemrolerole.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(securitysystemuserusersSecuritysystemrolerole), Encoding.UTF8, "application/json");

            OnUpdateSecuritysystemuserusersSecuritysystemrolerole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSolicitudBajasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSolicitudBajasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSolicitudBajas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SolicitudBaja>> GetSolicitudBajas(Query query)
        {
            return await GetSolicitudBajas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SolicitudBaja>> GetSolicitudBajas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SolicitudBajas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSolicitudBajas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SolicitudBaja>>(response);
        }

        partial void OnCreateSolicitudBaja(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SolicitudBaja> CreateSolicitudBaja(SGPA.Server.Models.CMU.SolicitudBaja solicitudBaja = default(SGPA.Server.Models.CMU.SolicitudBaja))
        {
            var uri = new Uri(baseUri, $"SolicitudBajas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(solicitudBaja), Encoding.UTF8, "application/json");

            OnCreateSolicitudBaja(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SolicitudBaja>(response);
        }

        partial void OnDeleteSolicitudBaja(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSolicitudBaja(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"SolicitudBajas({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSolicitudBaja(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSolicitudBajaByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SolicitudBaja> GetSolicitudBajaByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"SolicitudBajas({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSolicitudBajaByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SolicitudBaja>(response);
        }

        partial void OnUpdateSolicitudBaja(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSolicitudBaja(int oid = default(int), SGPA.Server.Models.CMU.SolicitudBaja solicitudBaja = default(SGPA.Server.Models.CMU.SolicitudBaja))
        {
            var uri = new Uri(baseUri, $"SolicitudBajas({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", solicitudBaja.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(solicitudBaja), Encoding.UTF8, "application/json");

            OnUpdateSolicitudBaja(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSolicitudBajaFileAttachmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajafileattachments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajafileattachments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSolicitudBajaFileAttachmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajafileattachments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajafileattachments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSolicitudBajaFileAttachments(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>> GetSolicitudBajaFileAttachments(Query query)
        {
            return await GetSolicitudBajaFileAttachments(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>> GetSolicitudBajaFileAttachments(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SolicitudBajaFileAttachments");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSolicitudBajaFileAttachments(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>>(response);
        }

        partial void OnCreateSolicitudBajaFileAttachment(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> CreateSolicitudBajaFileAttachment(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment solicitudBajaFileAttachment = default(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment))
        {
            var uri = new Uri(baseUri, $"SolicitudBajaFileAttachments");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(solicitudBajaFileAttachment), Encoding.UTF8, "application/json");

            OnCreateSolicitudBajaFileAttachment(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>(response);
        }

        partial void OnDeleteSolicitudBajaFileAttachment(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSolicitudBajaFileAttachment(Guid guid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SolicitudBajaFileAttachments({guid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSolicitudBajaFileAttachment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSolicitudBajaFileAttachmentByGuid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> GetSolicitudBajaFileAttachmentByGuid(string expand = default(string), Guid guid = default(Guid))
        {
            var uri = new Uri(baseUri, $"SolicitudBajaFileAttachments({guid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSolicitudBajaFileAttachmentByGuid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>(response);
        }

        partial void OnUpdateSolicitudBajaFileAttachment(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSolicitudBajaFileAttachment(Guid guid = default(Guid), SGPA.Server.Models.CMU.SolicitudBajaFileAttachment solicitudBajaFileAttachment = default(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment))
        {
            var uri = new Uri(baseUri, $"SolicitudBajaFileAttachments({guid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", solicitudBajaFileAttachment.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(solicitudBajaFileAttachment), Encoding.UTF8, "application/json");

            OnUpdateSolicitudBajaFileAttachment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTmpCarneEntregadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneentregados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneentregados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTmpCarneEntregadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneentregados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneentregados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTmpCarneEntregados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpCarneEntregado>> GetTmpCarneEntregados(Query query)
        {
            return await GetTmpCarneEntregados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpCarneEntregado>> GetTmpCarneEntregados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TmpCarneEntregados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpCarneEntregados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpCarneEntregado>>(response);
        }

        partial void OnCreateTmpCarneEntregado(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpCarneEntregado> CreateTmpCarneEntregado(SGPA.Server.Models.CMU.TmpCarneEntregado tmpCarneEntregado = default(SGPA.Server.Models.CMU.TmpCarneEntregado))
        {
            var uri = new Uri(baseUri, $"TmpCarneEntregados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpCarneEntregado), Encoding.UTF8, "application/json");

            OnCreateTmpCarneEntregado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpCarneEntregado>(response);
        }

        partial void OnDeleteTmpCarneEntregado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTmpCarneEntregado(int documento = default(int))
        {
            var uri = new Uri(baseUri, $"TmpCarneEntregados({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTmpCarneEntregado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTmpCarneEntregadoByDocumento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpCarneEntregado> GetTmpCarneEntregadoByDocumento(string expand = default(string), int documento = default(int))
        {
            var uri = new Uri(baseUri, $"TmpCarneEntregados({documento})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpCarneEntregadoByDocumento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpCarneEntregado>(response);
        }

        partial void OnUpdateTmpCarneEntregado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTmpCarneEntregado(int documento = default(int), SGPA.Server.Models.CMU.TmpCarneEntregado tmpCarneEntregado = default(SGPA.Server.Models.CMU.TmpCarneEntregado))
        {
            var uri = new Uri(baseUri, $"TmpCarneEntregados({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tmpCarneEntregado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpCarneEntregado), Encoding.UTF8, "application/json");

            OnUpdateTmpCarneEntregado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTmpCarneRetirarsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneretirars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneretirars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTmpCarneRetirarsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneretirars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneretirars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTmpCarneRetirars(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpCarneRetirar>> GetTmpCarneRetirars(Query query)
        {
            return await GetTmpCarneRetirars(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpCarneRetirar>> GetTmpCarneRetirars(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TmpCarneRetirars");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpCarneRetirars(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpCarneRetirar>>(response);
        }

        partial void OnCreateTmpCarneRetirar(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpCarneRetirar> CreateTmpCarneRetirar(SGPA.Server.Models.CMU.TmpCarneRetirar tmpCarneRetirar = default(SGPA.Server.Models.CMU.TmpCarneRetirar))
        {
            var uri = new Uri(baseUri, $"TmpCarneRetirars");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpCarneRetirar), Encoding.UTF8, "application/json");

            OnCreateTmpCarneRetirar(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpCarneRetirar>(response);
        }

        partial void OnDeleteTmpCarneRetirar(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTmpCarneRetirar(int documento = default(int))
        {
            var uri = new Uri(baseUri, $"TmpCarneRetirars({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTmpCarneRetirar(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTmpCarneRetirarByDocumento(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpCarneRetirar> GetTmpCarneRetirarByDocumento(string expand = default(string), int documento = default(int))
        {
            var uri = new Uri(baseUri, $"TmpCarneRetirars({documento})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpCarneRetirarByDocumento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpCarneRetirar>(response);
        }

        partial void OnUpdateTmpCarneRetirar(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTmpCarneRetirar(int documento = default(int), SGPA.Server.Models.CMU.TmpCarneRetirar tmpCarneRetirar = default(SGPA.Server.Models.CMU.TmpCarneRetirar))
        {
            var uri = new Uri(baseUri, $"TmpCarneRetirars({documento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tmpCarneRetirar.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpCarneRetirar), Encoding.UTF8, "application/json");

            OnUpdateTmpCarneRetirar(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTmpFechasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpfechas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpfechas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTmpFechasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpfechas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpfechas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTmpFechas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpFecha>> GetTmpFechas(Query query)
        {
            return await GetTmpFechas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpFecha>> GetTmpFechas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TmpFechas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpFechas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpFecha>>(response);
        }

        partial void OnCreateTmpFecha(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpFecha> CreateTmpFecha(SGPA.Server.Models.CMU.TmpFecha tmpFecha = default(SGPA.Server.Models.CMU.TmpFecha))
        {
            var uri = new Uri(baseUri, $"TmpFechas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpFecha), Encoding.UTF8, "application/json");

            OnCreateTmpFecha(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpFecha>(response);
        }

        partial void OnDeleteTmpFecha(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTmpFecha(DateTime fecha = default(DateTime))
        {
            var uri = new Uri(baseUri, $"TmpFechas({string.Format("{0:yyyy-MM-ddTHH:mm:ss.fffZ}", fecha)})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTmpFecha(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTmpFechaByFecha(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpFecha> GetTmpFechaByFecha(string expand = default(string), DateTime fecha = default(DateTime))
        {
            var uri = new Uri(baseUri, $"TmpFechas({string.Format("{0:yyyy-MM-ddTHH:mm:ss.fffZ}", fecha)})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpFechaByFecha(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpFecha>(response);
        }

        partial void OnUpdateTmpFecha(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTmpFecha(DateTime fecha = default(DateTime), SGPA.Server.Models.CMU.TmpFecha tmpFecha = default(SGPA.Server.Models.CMU.TmpFecha))
        {
            var uri = new Uri(baseUri, $"TmpFechas({string.Format("{0:yyyy-MM-ddTHH:mm:ss.fffZ}", fecha)})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tmpFecha.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpFecha), Encoding.UTF8, "application/json");

            OnUpdateTmpFecha(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTmpMesesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpmeses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpmeses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTmpMesesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpmeses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpmeses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTmpMeses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpMese>> GetTmpMeses(Query query)
        {
            return await GetTmpMeses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpMese>> GetTmpMeses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TmpMeses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpMeses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TmpMese>>(response);
        }

        partial void OnCreateTmpMese(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpMese> CreateTmpMese(SGPA.Server.Models.CMU.TmpMese tmpMese = default(SGPA.Server.Models.CMU.TmpMese))
        {
            var uri = new Uri(baseUri, $"TmpMeses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpMese), Encoding.UTF8, "application/json");

            OnCreateTmpMese(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpMese>(response);
        }

        partial void OnDeleteTmpMese(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTmpMese(int mes = default(int), int año = default(int))
        {
            var uri = new Uri(baseUri, $"TmpMeses(Mes={mes},Año={año})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTmpMese(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTmpMeseByMesAndAño(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TmpMese> GetTmpMeseByMesAndAño(string expand = default(string), int mes = default(int), int año = default(int))
        {
            var uri = new Uri(baseUri, $"TmpMeses(Mes={mes},Año={año})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTmpMeseByMesAndAño(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TmpMese>(response);
        }

        partial void OnUpdateTmpMese(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTmpMese(int mes = default(int), int año = default(int), SGPA.Server.Models.CMU.TmpMese tmpMese = default(SGPA.Server.Models.CMU.TmpMese))
        {
            var uri = new Uri(baseUri, $"TmpMeses(Mes={mes},Año={año})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tmpMese.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tmpMese), Encoding.UTF8, "application/json");

            OnUpdateTmpMese(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntabasesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntabases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntabases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntabasesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntabases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntabases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteInfoadjuntabases(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>> GetTramiteInfoadjuntabases(Query query)
        {
            return await GetTramiteInfoadjuntabases(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>> GetTramiteInfoadjuntabases(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntabases");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntabases(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>>(response);
        }

        partial void OnCreateTramiteInfoadjuntabase(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> CreateTramiteInfoadjuntabase(SGPA.Server.Models.CMU.TramiteInfoadjuntabase tramiteInfoadjuntabase = default(SGPA.Server.Models.CMU.TramiteInfoadjuntabase))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntabases");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntabase), Encoding.UTF8, "application/json");

            OnCreateTramiteInfoadjuntabase(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>(response);
        }

        partial void OnDeleteTramiteInfoadjuntabase(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteInfoadjuntabase(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntabases({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteInfoadjuntabase(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteInfoadjuntabaseByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> GetTramiteInfoadjuntabaseByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntabases({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntabaseByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>(response);
        }

        partial void OnUpdateTramiteInfoadjuntabase(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteInfoadjuntabase(int oid = default(int), SGPA.Server.Models.CMU.TramiteInfoadjuntabase tramiteInfoadjuntabase = default(SGPA.Server.Models.CMU.TramiteInfoadjuntabase))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntabases({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteInfoadjuntabase.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntabase), Encoding.UTF8, "application/json");

            OnUpdateTramiteInfoadjuntabase(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntacedulasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntacedulas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntacedulas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntacedulasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntacedulas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntacedulas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteInfoadjuntacedulas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>> GetTramiteInfoadjuntacedulas(Query query)
        {
            return await GetTramiteInfoadjuntacedulas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>> GetTramiteInfoadjuntacedulas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntacedulas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntacedulas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>>(response);
        }

        partial void OnCreateTramiteInfoadjuntacedula(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> CreateTramiteInfoadjuntacedula(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula tramiteInfoadjuntacedula = default(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntacedulas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntacedula), Encoding.UTF8, "application/json");

            OnCreateTramiteInfoadjuntacedula(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>(response);
        }

        partial void OnDeleteTramiteInfoadjuntacedula(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteInfoadjuntacedula(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntacedulas({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteInfoadjuntacedula(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteInfoadjuntacedulaByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> GetTramiteInfoadjuntacedulaByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntacedulas({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntacedulaByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>(response);
        }

        partial void OnUpdateTramiteInfoadjuntacedula(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteInfoadjuntacedula(int oid = default(int), SGPA.Server.Models.CMU.TramiteInfoadjuntacedula tramiteInfoadjuntacedula = default(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntacedulas({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteInfoadjuntacedula.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntacedula), Encoding.UTF8, "application/json");

            OnUpdateTramiteInfoadjuntacedula(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntaespecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntaespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntaespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntaespecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntaespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntaespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteInfoadjuntaespecialidads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>> GetTramiteInfoadjuntaespecialidads(Query query)
        {
            return await GetTramiteInfoadjuntaespecialidads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>> GetTramiteInfoadjuntaespecialidads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntaespecialidads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntaespecialidads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>>(response);
        }

        partial void OnCreateTramiteInfoadjuntaespecialidad(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> CreateTramiteInfoadjuntaespecialidad(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad tramiteInfoadjuntaespecialidad = default(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntaespecialidads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntaespecialidad), Encoding.UTF8, "application/json");

            OnCreateTramiteInfoadjuntaespecialidad(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>(response);
        }

        partial void OnDeleteTramiteInfoadjuntaespecialidad(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteInfoadjuntaespecialidad(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntaespecialidads({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteInfoadjuntaespecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteInfoadjuntaespecialidadByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> GetTramiteInfoadjuntaespecialidadByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntaespecialidads({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntaespecialidadByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>(response);
        }

        partial void OnUpdateTramiteInfoadjuntaespecialidad(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteInfoadjuntaespecialidad(int oid = default(int), SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad tramiteInfoadjuntaespecialidad = default(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntaespecialidads({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteInfoadjuntaespecialidad.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntaespecialidad), Encoding.UTF8, "application/json");

            OnUpdateTramiteInfoadjuntaespecialidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntafotocarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntafotocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntafotocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntafotocarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntafotocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntafotocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteInfoadjuntafotocarnes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>> GetTramiteInfoadjuntafotocarnes(Query query)
        {
            return await GetTramiteInfoadjuntafotocarnes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>> GetTramiteInfoadjuntafotocarnes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntafotocarnes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntafotocarnes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>>(response);
        }

        partial void OnCreateTramiteInfoadjuntafotocarne(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> CreateTramiteInfoadjuntafotocarne(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne tramiteInfoadjuntafotocarne = default(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntafotocarnes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntafotocarne), Encoding.UTF8, "application/json");

            OnCreateTramiteInfoadjuntafotocarne(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>(response);
        }

        partial void OnDeleteTramiteInfoadjuntafotocarne(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteInfoadjuntafotocarne(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntafotocarnes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteInfoadjuntafotocarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteInfoadjuntafotocarneByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> GetTramiteInfoadjuntafotocarneByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntafotocarnes({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntafotocarneByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>(response);
        }

        partial void OnUpdateTramiteInfoadjuntafotocarne(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteInfoadjuntafotocarne(int oid = default(int), SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne tramiteInfoadjuntafotocarne = default(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntafotocarnes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteInfoadjuntafotocarne.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntafotocarne), Encoding.UTF8, "application/json");

            OnUpdateTramiteInfoadjuntafotocarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntatitulosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntatitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntatitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteInfoadjuntatitulosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntatitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntatitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteInfoadjuntatitulos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>> GetTramiteInfoadjuntatitulos(Query query)
        {
            return await GetTramiteInfoadjuntatitulos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>> GetTramiteInfoadjuntatitulos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntatitulos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntatitulos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>>(response);
        }

        partial void OnCreateTramiteInfoadjuntatitulo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> CreateTramiteInfoadjuntatitulo(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo tramiteInfoadjuntatitulo = default(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntatitulos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntatitulo), Encoding.UTF8, "application/json");

            OnCreateTramiteInfoadjuntatitulo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>(response);
        }

        partial void OnDeleteTramiteInfoadjuntatitulo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteInfoadjuntatitulo(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntatitulos({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteInfoadjuntatitulo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteInfoadjuntatituloByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> GetTramiteInfoadjuntatituloByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntatitulos({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteInfoadjuntatituloByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>(response);
        }

        partial void OnUpdateTramiteInfoadjuntatitulo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteInfoadjuntatitulo(int oid = default(int), SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo tramiteInfoadjuntatitulo = default(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo))
        {
            var uri = new Uri(baseUri, $"TramiteInfoadjuntatitulos({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteInfoadjuntatitulo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteInfoadjuntatitulo), Encoding.UTF8, "application/json");

            OnUpdateTramiteInfoadjuntatitulo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteCarnes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarne>> GetTramiteCarnes(Query query)
        {
            return await GetTramiteCarnes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarne>> GetTramiteCarnes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteCarnes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarnes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarne>>(response);
        }

        partial void OnCreateTramiteCarne(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarne> CreateTramiteCarne(SGPA.Server.Models.CMU.TramiteCarne tramiteCarne = default(SGPA.Server.Models.CMU.TramiteCarne))
        {
            var uri = new Uri(baseUri, $"TramiteCarnes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarne), Encoding.UTF8, "application/json");

            OnCreateTramiteCarne(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarne>(response);
        }

        partial void OnDeleteTramiteCarne(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteCarne(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarnes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteCarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteCarneByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarne> GetTramiteCarneByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarnes({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarneByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarne>(response);
        }

        partial void OnUpdateTramiteCarne(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteCarne(int oid = default(int), SGPA.Server.Models.CMU.TramiteCarne tramiteCarne = default(SGPA.Server.Models.CMU.TramiteCarne))
        {
            var uri = new Uri(baseUri, $"TramiteCarnes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteCarne.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarne), Encoding.UTF8, "application/json");

            OnUpdateTramiteCarne(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarneEstadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarneEstadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteCarneEstados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstado>> GetTramiteCarneEstados(Query query)
        {
            return await GetTramiteCarneEstados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstado>> GetTramiteCarneEstados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarneEstados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstado>>(response);
        }

        partial void OnCreateTramiteCarneEstado(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstado> CreateTramiteCarneEstado(SGPA.Server.Models.CMU.TramiteCarneEstado tramiteCarneEstado = default(SGPA.Server.Models.CMU.TramiteCarneEstado))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarneEstado), Encoding.UTF8, "application/json");

            OnCreateTramiteCarneEstado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarneEstado>(response);
        }

        partial void OnDeleteTramiteCarneEstado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteCarneEstado(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstados({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteCarneEstado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteCarneEstadoByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstado> GetTramiteCarneEstadoByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstados({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarneEstadoByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarneEstado>(response);
        }

        partial void OnUpdateTramiteCarneEstado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteCarneEstado(int oid = default(int), SGPA.Server.Models.CMU.TramiteCarneEstado tramiteCarneEstado = default(SGPA.Server.Models.CMU.TramiteCarneEstado))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstados({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteCarneEstado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarneEstado), Encoding.UTF8, "application/json");

            OnUpdateTramiteCarneEstado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarneEstadoCodigosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadocodigos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadocodigos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarneEstadoCodigosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadocodigos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadocodigos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteCarneEstadoCodigos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>> GetTramiteCarneEstadoCodigos(Query query)
        {
            return await GetTramiteCarneEstadoCodigos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>> GetTramiteCarneEstadoCodigos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoCodigos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarneEstadoCodigos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>>(response);
        }

        partial void OnCreateTramiteCarneEstadoCodigo(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> CreateTramiteCarneEstadoCodigo(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo tramiteCarneEstadoCodigo = default(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoCodigos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarneEstadoCodigo), Encoding.UTF8, "application/json");

            OnCreateTramiteCarneEstadoCodigo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>(response);
        }

        partial void OnDeleteTramiteCarneEstadoCodigo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteCarneEstadoCodigo(int id = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoCodigos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteCarneEstadoCodigo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteCarneEstadoCodigoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> GetTramiteCarneEstadoCodigoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoCodigos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarneEstadoCodigoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>(response);
        }

        partial void OnUpdateTramiteCarneEstadoCodigo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteCarneEstadoCodigo(int id = default(int), SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo tramiteCarneEstadoCodigo = default(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoCodigos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteCarneEstadoCodigo.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarneEstadoCodigo), Encoding.UTF8, "application/json");

            OnUpdateTramiteCarneEstadoCodigo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarneEstadoWorkFlowsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadoworkflows/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadoworkflows/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTramiteCarneEstadoWorkFlowsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadoworkflows/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadoworkflows/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTramiteCarneEstadoWorkFlows(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>> GetTramiteCarneEstadoWorkFlows(Query query)
        {
            return await GetTramiteCarneEstadoWorkFlows(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>> GetTramiteCarneEstadoWorkFlows(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoWorkFlows");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarneEstadoWorkFlows(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>>(response);
        }

        partial void OnCreateTramiteCarneEstadoWorkFlow(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> CreateTramiteCarneEstadoWorkFlow(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow tramiteCarneEstadoWorkFlow = default(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoWorkFlows");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarneEstadoWorkFlow), Encoding.UTF8, "application/json");

            OnCreateTramiteCarneEstadoWorkFlow(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>(response);
        }

        partial void OnDeleteTramiteCarneEstadoWorkFlow(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTramiteCarneEstadoWorkFlow(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoWorkFlows({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTramiteCarneEstadoWorkFlow(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTramiteCarneEstadoWorkFlowByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> GetTramiteCarneEstadoWorkFlowByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoWorkFlows({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTramiteCarneEstadoWorkFlowByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>(response);
        }

        partial void OnUpdateTramiteCarneEstadoWorkFlow(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTramiteCarneEstadoWorkFlow(int oid = default(int), SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow tramiteCarneEstadoWorkFlow = default(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow))
        {
            var uri = new Uri(baseUri, $"TramiteCarneEstadoWorkFlows({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tramiteCarneEstadoWorkFlow.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tramiteCarneEstadoWorkFlow), Encoding.UTF8, "application/json");

            OnUpdateTramiteCarneEstadoWorkFlow(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUniversidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUniversidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUniversidads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Universidad>> GetUniversidads(Query query)
        {
            return await GetUniversidads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Universidad>> GetUniversidads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Universidads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUniversidads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Universidad>>(response);
        }

        partial void OnCreateUniversidad(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Universidad> CreateUniversidad(SGPA.Server.Models.CMU.Universidad universidad = default(SGPA.Server.Models.CMU.Universidad))
        {
            var uri = new Uri(baseUri, $"Universidads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(universidad), Encoding.UTF8, "application/json");

            OnCreateUniversidad(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Universidad>(response);
        }

        partial void OnDeleteUniversidad(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUniversidad(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Universidads({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUniversidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUniversidadById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Universidad> GetUniversidadById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Universidads({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUniversidadById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Universidad>(response);
        }

        partial void OnUpdateUniversidad(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUniversidad(int id = default(int), SGPA.Server.Models.CMU.Universidad universidad = default(SGPA.Server.Models.CMU.Universidad))
        {
            var uri = new Uri(baseUri, $"Universidads({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", universidad.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(universidad), Encoding.UTF8, "application/json");

            OnUpdateUniversidad(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUniversidadTituloGradosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidadtitulogrados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidadtitulogrados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUniversidadTituloGradosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidadtitulogrados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidadtitulogrados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUniversidadTituloGrados(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UniversidadTituloGrado>> GetUniversidadTituloGrados(Query query)
        {
            return await GetUniversidadTituloGrados(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UniversidadTituloGrado>> GetUniversidadTituloGrados(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"UniversidadTituloGrados");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUniversidadTituloGrados(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UniversidadTituloGrado>>(response);
        }

        partial void OnCreateUniversidadTituloGrado(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UniversidadTituloGrado> CreateUniversidadTituloGrado(SGPA.Server.Models.CMU.UniversidadTituloGrado universidadTituloGrado = default(SGPA.Server.Models.CMU.UniversidadTituloGrado))
        {
            var uri = new Uri(baseUri, $"UniversidadTituloGrados");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(universidadTituloGrado), Encoding.UTF8, "application/json");

            OnCreateUniversidadTituloGrado(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UniversidadTituloGrado>(response);
        }

        partial void OnDeleteUniversidadTituloGrado(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUniversidadTituloGrado(int id = default(int))
        {
            var uri = new Uri(baseUri, $"UniversidadTituloGrados({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUniversidadTituloGrado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUniversidadTituloGradoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UniversidadTituloGrado> GetUniversidadTituloGradoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"UniversidadTituloGrados({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUniversidadTituloGradoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UniversidadTituloGrado>(response);
        }

        partial void OnUpdateUniversidadTituloGrado(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUniversidadTituloGrado(int id = default(int), SGPA.Server.Models.CMU.UniversidadTituloGrado universidadTituloGrado = default(SGPA.Server.Models.CMU.UniversidadTituloGrado))
        {
            var uri = new Uri(baseUri, $"UniversidadTituloGrados({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", universidadTituloGrado.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(universidadTituloGrado), Encoding.UTF8, "application/json");

            OnUpdateUniversidadTituloGrado(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUserResetPasswordRequestsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/userresetpasswordrequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/userresetpasswordrequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUserResetPasswordRequestsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/userresetpasswordrequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/userresetpasswordrequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUserResetPasswordRequests(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UserResetPasswordRequest>> GetUserResetPasswordRequests(Query query)
        {
            return await GetUserResetPasswordRequests(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UserResetPasswordRequest>> GetUserResetPasswordRequests(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"UserResetPasswordRequests");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserResetPasswordRequests(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UserResetPasswordRequest>>(response);
        }

        partial void OnCreateUserResetPasswordRequest(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UserResetPasswordRequest> CreateUserResetPasswordRequest(SGPA.Server.Models.CMU.UserResetPasswordRequest userResetPasswordRequest = default(SGPA.Server.Models.CMU.UserResetPasswordRequest))
        {
            var uri = new Uri(baseUri, $"UserResetPasswordRequests");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(userResetPasswordRequest), Encoding.UTF8, "application/json");

            OnCreateUserResetPasswordRequest(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UserResetPasswordRequest>(response);
        }

        partial void OnDeleteUserResetPasswordRequest(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUserResetPasswordRequest(int id = default(int))
        {
            var uri = new Uri(baseUri, $"UserResetPasswordRequests({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUserResetPasswordRequest(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUserResetPasswordRequestById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UserResetPasswordRequest> GetUserResetPasswordRequestById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"UserResetPasswordRequests({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserResetPasswordRequestById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UserResetPasswordRequest>(response);
        }

        partial void OnUpdateUserResetPasswordRequest(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUserResetPasswordRequest(int id = default(int), SGPA.Server.Models.CMU.UserResetPasswordRequest userResetPasswordRequest = default(SGPA.Server.Models.CMU.UserResetPasswordRequest))
        {
            var uri = new Uri(baseUri, $"UserResetPasswordRequests({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", userResetPasswordRequest.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(userResetPasswordRequest), Encoding.UTF8, "application/json");

            OnUpdateUserResetPasswordRequest(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUsuariosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsuariosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUsuarios(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Usuario>> GetUsuarios(Query query)
        {
            return await GetUsuarios(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Usuario>> GetUsuarios(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Usuarios");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarios(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.Usuario>>(response);
        }

        partial void OnCreateUsuario(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Usuario> CreateUsuario(SGPA.Server.Models.CMU.Usuario usuario = default(SGPA.Server.Models.CMU.Usuario))
        {
            var uri = new Uri(baseUri, $"Usuarios");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

            OnCreateUsuario(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Usuario>(response);
        }

        partial void OnDeleteUsuario(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUsuario(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"Usuarios({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUsuario(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUsuarioByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.Usuario> GetUsuarioByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"Usuarios({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarioByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.Usuario>(response);
        }

        partial void OnUpdateUsuario(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUsuario(Guid oid = default(Guid), SGPA.Server.Models.CMU.Usuario usuario = default(SGPA.Server.Models.CMU.Usuario))
        {
            var uri = new Uri(baseUri, $"Usuarios({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", usuario.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

            OnUpdateUsuario(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUsuarioAccesosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioaccesos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioaccesos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsuarioAccesosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioaccesos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioaccesos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUsuarioAccesos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioAcceso>> GetUsuarioAccesos(Query query)
        {
            return await GetUsuarioAccesos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioAcceso>> GetUsuarioAccesos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"UsuarioAccesos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarioAccesos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioAcceso>>(response);
        }

        partial void OnCreateUsuarioAcceso(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UsuarioAcceso> CreateUsuarioAcceso(SGPA.Server.Models.CMU.UsuarioAcceso usuarioAcceso = default(SGPA.Server.Models.CMU.UsuarioAcceso))
        {
            var uri = new Uri(baseUri, $"UsuarioAccesos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuarioAcceso), Encoding.UTF8, "application/json");

            OnCreateUsuarioAcceso(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UsuarioAcceso>(response);
        }

        partial void OnDeleteUsuarioAcceso(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUsuarioAcceso(int id = default(int))
        {
            var uri = new Uri(baseUri, $"UsuarioAccesos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUsuarioAcceso(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUsuarioAccesoById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UsuarioAcceso> GetUsuarioAccesoById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"UsuarioAccesos({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarioAccesoById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UsuarioAcceso>(response);
        }

        partial void OnUpdateUsuarioAcceso(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUsuarioAcceso(int id = default(int), SGPA.Server.Models.CMU.UsuarioAcceso usuarioAcceso = default(SGPA.Server.Models.CMU.UsuarioAcceso))
        {
            var uri = new Uri(baseUri, $"UsuarioAccesos({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", usuarioAcceso.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuarioAcceso), Encoding.UTF8, "application/json");

            OnUpdateUsuarioAcceso(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUsuarioInstitucionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioinstitucions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioinstitucions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsuarioInstitucionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioinstitucions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioinstitucions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUsuarioInstitucions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioInstitucion>> GetUsuarioInstitucions(Query query)
        {
            return await GetUsuarioInstitucions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioInstitucion>> GetUsuarioInstitucions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"UsuarioInstitucions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarioInstitucions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioInstitucion>>(response);
        }

        partial void OnCreateUsuarioInstitucion(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UsuarioInstitucion> CreateUsuarioInstitucion(SGPA.Server.Models.CMU.UsuarioInstitucion usuarioInstitucion = default(SGPA.Server.Models.CMU.UsuarioInstitucion))
        {
            var uri = new Uri(baseUri, $"UsuarioInstitucions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuarioInstitucion), Encoding.UTF8, "application/json");

            OnCreateUsuarioInstitucion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UsuarioInstitucion>(response);
        }

        partial void OnDeleteUsuarioInstitucion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUsuarioInstitucion(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"UsuarioInstitucions({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUsuarioInstitucion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUsuarioInstitucionByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UsuarioInstitucion> GetUsuarioInstitucionByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"UsuarioInstitucions({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarioInstitucionByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UsuarioInstitucion>(response);
        }

        partial void OnUpdateUsuarioInstitucion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUsuarioInstitucion(Guid oid = default(Guid), SGPA.Server.Models.CMU.UsuarioInstitucion usuarioInstitucion = default(SGPA.Server.Models.CMU.UsuarioInstitucion))
        {
            var uri = new Uri(baseUri, $"UsuarioInstitucions({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", usuarioInstitucion.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuarioInstitucion), Encoding.UTF8, "application/json");

            OnUpdateUsuarioInstitucion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUsuarioRegionalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioregionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioregionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsuarioRegionalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioregionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioregionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUsuarioRegionals(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioRegional>> GetUsuarioRegionals(Query query)
        {
            return await GetUsuarioRegionals(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioRegional>> GetUsuarioRegionals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"UsuarioRegionals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarioRegionals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.UsuarioRegional>>(response);
        }

        partial void OnCreateUsuarioRegional(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UsuarioRegional> CreateUsuarioRegional(SGPA.Server.Models.CMU.UsuarioRegional usuarioRegional = default(SGPA.Server.Models.CMU.UsuarioRegional))
        {
            var uri = new Uri(baseUri, $"UsuarioRegionals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuarioRegional), Encoding.UTF8, "application/json");

            OnCreateUsuarioRegional(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UsuarioRegional>(response);
        }

        partial void OnDeleteUsuarioRegional(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUsuarioRegional(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"UsuarioRegionals({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUsuarioRegional(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUsuarioRegionalByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.UsuarioRegional> GetUsuarioRegionalByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"UsuarioRegionals({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsuarioRegionalByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.UsuarioRegional>(response);
        }

        partial void OnUpdateUsuarioRegional(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUsuarioRegional(Guid oid = default(Guid), SGPA.Server.Models.CMU.UsuarioRegional usuarioRegional = default(SGPA.Server.Models.CMU.UsuarioRegional))
        {
            var uri = new Uri(baseUri, $"UsuarioRegionals({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", usuarioRegional.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(usuarioRegional), Encoding.UTF8, "application/json");

            OnUpdateUsuarioRegional(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXpObjectModifiedsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjectmodifieds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjectmodifieds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXpObjectModifiedsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjectmodifieds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjectmodifieds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXpObjectModifieds(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpObjectModified>> GetXpObjectModifieds(Query query)
        {
            return await GetXpObjectModifieds(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpObjectModified>> GetXpObjectModifieds(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XpObjectModifieds");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpObjectModifieds(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpObjectModified>>(response);
        }

        partial void OnCreateXpObjectModified(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpObjectModified> CreateXpObjectModified(SGPA.Server.Models.CMU.XpObjectModified xpObjectModified = default(SGPA.Server.Models.CMU.XpObjectModified))
        {
            var uri = new Uri(baseUri, $"XpObjectModifieds");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpObjectModified), Encoding.UTF8, "application/json");

            OnCreateXpObjectModified(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpObjectModified>(response);
        }

        partial void OnDeleteXpObjectModified(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXpObjectModified(int id = default(int))
        {
            var uri = new Uri(baseUri, $"XpObjectModifieds({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXpObjectModified(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXpObjectModifiedById(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpObjectModified> GetXpObjectModifiedById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"XpObjectModifieds({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpObjectModifiedById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpObjectModified>(response);
        }

        partial void OnUpdateXpObjectModified(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXpObjectModified(int id = default(int), SGPA.Server.Models.CMU.XpObjectModified xpObjectModified = default(SGPA.Server.Models.CMU.XpObjectModified))
        {
            var uri = new Uri(baseUri, $"XpObjectModifieds({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xpObjectModified.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpObjectModified), Encoding.UTF8, "application/json");

            OnUpdateXpObjectModified(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXpObjectTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjecttypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjecttypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXpObjectTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjecttypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjecttypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXpObjectTypes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpObjectType>> GetXpObjectTypes(Query query)
        {
            return await GetXpObjectTypes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpObjectType>> GetXpObjectTypes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XpObjectTypes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpObjectTypes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpObjectType>>(response);
        }

        partial void OnCreateXpObjectType(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpObjectType> CreateXpObjectType(SGPA.Server.Models.CMU.XpObjectType xpObjectType = default(SGPA.Server.Models.CMU.XpObjectType))
        {
            var uri = new Uri(baseUri, $"XpObjectTypes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpObjectType), Encoding.UTF8, "application/json");

            OnCreateXpObjectType(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpObjectType>(response);
        }

        partial void OnDeleteXpObjectType(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXpObjectType(int oid = default(int))
        {
            var uri = new Uri(baseUri, $"XpObjectTypes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXpObjectType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXpObjectTypeByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpObjectType> GetXpObjectTypeByOid(string expand = default(string), int oid = default(int))
        {
            var uri = new Uri(baseUri, $"XpObjectTypes({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpObjectTypeByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpObjectType>(response);
        }

        partial void OnUpdateXpObjectType(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXpObjectType(int oid = default(int), SGPA.Server.Models.CMU.XpObjectType xpObjectType = default(SGPA.Server.Models.CMU.XpObjectType))
        {
            var uri = new Uri(baseUri, $"XpObjectTypes({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xpObjectType.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpObjectType), Encoding.UTF8, "application/json");

            OnUpdateXpObjectType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXpoStatesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXpoStatesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXpoStates(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoState>> GetXpoStates(Query query)
        {
            return await GetXpoStates(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoState>> GetXpoStates(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XpoStates");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoStates(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoState>>(response);
        }

        partial void OnCreateXpoState(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoState> CreateXpoState(SGPA.Server.Models.CMU.XpoState xpoState = default(SGPA.Server.Models.CMU.XpoState))
        {
            var uri = new Uri(baseUri, $"XpoStates");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoState), Encoding.UTF8, "application/json");

            OnCreateXpoState(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoState>(response);
        }

        partial void OnDeleteXpoState(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXpoState(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoStates({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXpoState(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXpoStateByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoState> GetXpoStateByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoStates({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoStateByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoState>(response);
        }

        partial void OnUpdateXpoState(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXpoState(Guid oid = default(Guid), SGPA.Server.Models.CMU.XpoState xpoState = default(SGPA.Server.Models.CMU.XpoState))
        {
            var uri = new Uri(baseUri, $"XpoStates({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xpoState.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoState), Encoding.UTF8, "application/json");

            OnUpdateXpoState(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXpoStateAppearancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostateappearances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostateappearances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXpoStateAppearancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostateappearances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostateappearances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXpoStateAppearances(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoStateAppearance>> GetXpoStateAppearances(Query query)
        {
            return await GetXpoStateAppearances(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoStateAppearance>> GetXpoStateAppearances(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XpoStateAppearances");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoStateAppearances(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoStateAppearance>>(response);
        }

        partial void OnCreateXpoStateAppearance(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoStateAppearance> CreateXpoStateAppearance(SGPA.Server.Models.CMU.XpoStateAppearance xpoStateAppearance = default(SGPA.Server.Models.CMU.XpoStateAppearance))
        {
            var uri = new Uri(baseUri, $"XpoStateAppearances");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoStateAppearance), Encoding.UTF8, "application/json");

            OnCreateXpoStateAppearance(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoStateAppearance>(response);
        }

        partial void OnDeleteXpoStateAppearance(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXpoStateAppearance(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoStateAppearances({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXpoStateAppearance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXpoStateAppearanceByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoStateAppearance> GetXpoStateAppearanceByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoStateAppearances({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoStateAppearanceByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoStateAppearance>(response);
        }

        partial void OnUpdateXpoStateAppearance(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXpoStateAppearance(Guid oid = default(Guid), SGPA.Server.Models.CMU.XpoStateAppearance xpoStateAppearance = default(SGPA.Server.Models.CMU.XpoStateAppearance))
        {
            var uri = new Uri(baseUri, $"XpoStateAppearances({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xpoStateAppearance.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoStateAppearance), Encoding.UTF8, "application/json");

            OnUpdateXpoStateAppearance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXpoStateMachinesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostatemachines/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostatemachines/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXpoStateMachinesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostatemachines/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostatemachines/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXpoStateMachines(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoStateMachine>> GetXpoStateMachines(Query query)
        {
            return await GetXpoStateMachines(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoStateMachine>> GetXpoStateMachines(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XpoStateMachines");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoStateMachines(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoStateMachine>>(response);
        }

        partial void OnCreateXpoStateMachine(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoStateMachine> CreateXpoStateMachine(SGPA.Server.Models.CMU.XpoStateMachine xpoStateMachine = default(SGPA.Server.Models.CMU.XpoStateMachine))
        {
            var uri = new Uri(baseUri, $"XpoStateMachines");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoStateMachine), Encoding.UTF8, "application/json");

            OnCreateXpoStateMachine(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoStateMachine>(response);
        }

        partial void OnDeleteXpoStateMachine(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXpoStateMachine(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoStateMachines({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXpoStateMachine(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXpoStateMachineByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoStateMachine> GetXpoStateMachineByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoStateMachines({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoStateMachineByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoStateMachine>(response);
        }

        partial void OnUpdateXpoStateMachine(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXpoStateMachine(Guid oid = default(Guid), SGPA.Server.Models.CMU.XpoStateMachine xpoStateMachine = default(SGPA.Server.Models.CMU.XpoStateMachine))
        {
            var uri = new Uri(baseUri, $"XpoStateMachines({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xpoStateMachine.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoStateMachine), Encoding.UTF8, "application/json");

            OnUpdateXpoStateMachine(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXpoTransitionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpotransitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpotransitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXpoTransitionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpotransitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpotransitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXpoTransitions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoTransition>> GetXpoTransitions(Query query)
        {
            return await GetXpoTransitions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoTransition>> GetXpoTransitions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XpoTransitions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoTransitions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpoTransition>>(response);
        }

        partial void OnCreateXpoTransition(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoTransition> CreateXpoTransition(SGPA.Server.Models.CMU.XpoTransition xpoTransition = default(SGPA.Server.Models.CMU.XpoTransition))
        {
            var uri = new Uri(baseUri, $"XpoTransitions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoTransition), Encoding.UTF8, "application/json");

            OnCreateXpoTransition(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoTransition>(response);
        }

        partial void OnDeleteXpoTransition(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXpoTransition(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoTransitions({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXpoTransition(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXpoTransitionByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpoTransition> GetXpoTransitionByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpoTransitions({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpoTransitionByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpoTransition>(response);
        }

        partial void OnUpdateXpoTransition(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXpoTransition(Guid oid = default(Guid), SGPA.Server.Models.CMU.XpoTransition xpoTransition = default(SGPA.Server.Models.CMU.XpoTransition))
        {
            var uri = new Uri(baseUri, $"XpoTransitions({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xpoTransition.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpoTransition), Encoding.UTF8, "application/json");

            OnUpdateXpoTransition(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportXpWeakReferencesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportXpWeakReferencesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetXpWeakReferences(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpWeakReference>> GetXpWeakReferences(Query query)
        {
            return await GetXpWeakReferences(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpWeakReference>> GetXpWeakReferences(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"XpWeakReferences");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpWeakReferences(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<SGPA.Server.Models.CMU.XpWeakReference>>(response);
        }

        partial void OnCreateXpWeakReference(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpWeakReference> CreateXpWeakReference(SGPA.Server.Models.CMU.XpWeakReference xpWeakReference = default(SGPA.Server.Models.CMU.XpWeakReference))
        {
            var uri = new Uri(baseUri, $"XpWeakReferences");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpWeakReference), Encoding.UTF8, "application/json");

            OnCreateXpWeakReference(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpWeakReference>(response);
        }

        partial void OnDeleteXpWeakReference(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteXpWeakReference(Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpWeakReferences({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteXpWeakReference(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetXpWeakReferenceByOid(HttpRequestMessage requestMessage);

        public async Task<SGPA.Server.Models.CMU.XpWeakReference> GetXpWeakReferenceByOid(string expand = default(string), Guid oid = default(Guid))
        {
            var uri = new Uri(baseUri, $"XpWeakReferences({oid})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXpWeakReferenceByOid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<SGPA.Server.Models.CMU.XpWeakReference>(response);
        }

        partial void OnUpdateXpWeakReference(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateXpWeakReference(Guid oid = default(Guid), SGPA.Server.Models.CMU.XpWeakReference xpWeakReference = default(SGPA.Server.Models.CMU.XpWeakReference))
        {
            var uri = new Uri(baseUri, $"XpWeakReferences({oid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", xpWeakReference.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(xpWeakReference), Encoding.UTF8, "application/json");

            OnUpdateXpWeakReference(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}