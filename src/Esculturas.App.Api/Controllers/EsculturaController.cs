using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Esculturas.Core.Business.Interfaces;
using Esculturas.Core.Configuration;
using Esculturas.Core.Entities;
using Esculturas.Core.Entities.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Esculturas.App.Api.Controllers
{
    [ApiController]
    [Route("api/esculturas")]
    public class EsculturaController : ControllerBase
    {

        private ICurrentConfiguration _currentConfiguration { get; set; }
        private IEsculturaBusiness _esculturaBusiness { get; set; }
        private IMemoryCache _cache { get; set; }
        private Serilog.ILogger _logger { get; set; }

        public EsculturaController(ICurrentConfiguration currentConfiguration,
                                   IEsculturaBusiness esculturaBusiness,
                                   IMemoryCache cache,
                                   Serilog.ILogger logger)
        {
            _currentConfiguration = currentConfiguration;
            _esculturaBusiness = esculturaBusiness;
            _cache = cache;
            _logger = logger;
        }



        [HttpGet]
        public IEnumerable<Escultura> Get()
        {
            return EsculturasFromCache();
        }

        [HttpPost]
        [Route("search")]
        public IEnumerable<Escultura> PostSearch(EsculturaFiltro filtro)
        {
            return _esculturaBusiness.Search(filtro, EsculturasFromCache());
        }

        private IEnumerable<Escultura> EsculturasFromCache() {

            var key = "EsculturasList";

            IEnumerable<Escultura> cacheEntry = null;

            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = _esculturaBusiness.GetList(); 

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                            .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;

        }
    }
}
