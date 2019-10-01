using Esculturas.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Esculturas.App.Web.Data
{
    public class EsculturasService
    {
        private IHttpClientFactory _httpClientFactory;

        public EsculturasService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Escultura>> GetEsculturasAsync(string textFilter)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001/");

            var esculturasDataJson = await httpClient.GetStringAsync("api/escultura");

            var esculturasJson = JsonSerializer.Deserialize<List<Escultura>>(esculturasDataJson);

            return esculturasJson;
        }
    }
}
