using Esculturas.Core.Entities.Extensions;
using Microsoft.Spatial;
using System;
using System.Text.Json.Serialization;

namespace Esculturas.Core.Entities
{
    public class Escultor
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("pais")]
        public string Pais { get; set; }

        public Escultor()
        {

        }

        public Escultor(string nombre)
        {
            Nombre = nombre;
        }

        [JsonPropertyName("tienePais")]
        public bool TienePais
        {
            get{
                return !string.IsNullOrEmpty(Pais);
            }
        }
    }
}
