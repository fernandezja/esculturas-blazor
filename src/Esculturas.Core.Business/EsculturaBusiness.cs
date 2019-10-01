using Esculturas.Core.Business.Interfaces;
using Esculturas.Core.Data.Interfaces;
using Esculturas.Core.Entities;
using Esculturas.Core.Entities.Filters;
using Microsoft.Spatial;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Esculturas.Core.Business
{
    public class EsculturaBusiness : IEsculturaBusiness
    {
        private IEsculturaRepository _esculturaRepository { get; set; }

        public EsculturaBusiness(IEsculturaRepository esculturaRepository)
        {
            _esculturaRepository = esculturaRepository;
        }

        public List<Escultura> GetList()
        {
            return _esculturaRepository.GetList();
        }

        public IEnumerable<Escultura> Search(EsculturaFiltro filtro, 
                                        IEnumerable<Escultura> esculturas)
        {
            if (string.IsNullOrEmpty(filtro.PalabrasABuscar))
            {
                return esculturas;
            }

            var query = from e in esculturas
                        where Compare(e.Nombre, filtro.PalabrasABuscar)
                            || Compare(e.Escultor.Nombre, filtro.PalabrasABuscar)
                            || Compare(e.Direccion, filtro.PalabrasABuscar)
                            || Compare(e.Material, filtro.PalabrasABuscar)
                            || Compare(e.Descripcion, filtro.PalabrasABuscar)
                            || Compare(e.Premios, filtro.PalabrasABuscar)
                        select e;

            return query.ToList();
        }

        private bool Compare(string textSource, string textValue) {

            if (string.IsNullOrEmpty(textSource))
            {
                return false;
            }

            if (string.IsNullOrEmpty(textValue))
            {
                return true;
            }

            //return RemoveDiacritics(textSource).Contains(RemoveDiacritics(textValue), CompareOptions.IgnoreNonSpace);

            var index = CultureInfo.InvariantCulture.CompareInfo.IndexOf
                (RemoveDiacritics(textSource), RemoveDiacritics(textValue), 
                    CompareOptions.IgnoreCase |
                    CompareOptions.IgnoreSymbols | 
                    CompareOptions.IgnoreNonSpace);

            return index != -1;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/359827/ignoring-accented-letters-in-string-comparison
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string RemoveDiacritics(string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                              UnicodeCategory.NonSpacingMark)
              ).Normalize(NormalizationForm.FormC);
        }


    }
}
