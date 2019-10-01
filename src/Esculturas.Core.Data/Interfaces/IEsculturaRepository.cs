using System.Collections.Generic;
using Esculturas.Core.Entities;

namespace Esculturas.Core.Data.Interfaces
{
    public interface IEsculturaRepository
    {
        List<Escultura> GetList();
    }
}