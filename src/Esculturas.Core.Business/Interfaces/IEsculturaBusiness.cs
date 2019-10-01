using System.Collections.Generic;
using Esculturas.Core.Entities;
using Esculturas.Core.Entities.Filters;

namespace Esculturas.Core.Business.Interfaces
{
    public interface IEsculturaBusiness
    {
        List<Escultura> GetList();
        IEnumerable<Escultura> Search(EsculturaFiltro filtro, IEnumerable<Escultura> enumerable);
    }
}