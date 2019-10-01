using Esculturas.Core.Entities.Extensions;
using Microsoft.Spatial;
using System;
using System.Text.Json.Serialization;

namespace Esculturas.Core.Entities
{
    public class Escultura
    {
        [JsonPropertyName("numero")]
        public int Numero { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("coordenada")]
        public Coordenada Coordenada { get; set; }

        [JsonPropertyName("escultor")]
        public Escultor Escultor { get; set; }

        [JsonPropertyName("material")]
        public string Material { get; set; }

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }

        [JsonPropertyName("premios")]
        public string Premios { get; set; }

        public double DistanciaA(Coordenada referencia)
        {
            return Coordenada.DistanciaA(referencia, UnidadDeDistancia.Kilometers);
        }
    }
}
