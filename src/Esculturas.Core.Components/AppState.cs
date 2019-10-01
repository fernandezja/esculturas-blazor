using Esculturas.Core.Entities;
using Esculturas.Core.Entities.Filters;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Esculturas.Core.Components
{
    public class AppState
    {
        public IReadOnlyList<Escultura> Esculturas { get; private set; }
        public bool BusquedaEnProgreso { get; private set; }

        private IHttpClientFactory _httpClientFactory;

        public event Action OnChange;

        public AppState(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task BuscarAsync(EsculturaFiltro filtro)
        {
            Esculturas = null;
            BusquedaEnProgreso = true;

            NotifyStateChanged();

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001/");

            //var esculturasDataJson = await httpClient.GetStringAsync("api/escultura");
            //Esculturas = JsonSerializer.Deserialize<List<Escultura>>(esculturasDataJson);

            Esculturas = 
                await httpClient.PostJsonAsync<IReadOnlyList<Escultura>>("api/esculturas/search", filtro);
               

            BusquedaEnProgreso = false;
            NotifyStateChanged();
        }


        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
