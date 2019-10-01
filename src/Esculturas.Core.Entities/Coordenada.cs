using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace Esculturas.Core.Entities
{
    
    public class Coordenada
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; private set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; private set; }

        [JsonPropertyName("isEmpy")]
        public bool IsEmpy {
            get {
                return (Latitude + Longitude) == 0;
            }
        }

        public Coordenada()
        {

        }

        public Coordenada(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Coordenada(string coordenadas)
        {
            if (!string.IsNullOrEmpty(coordenadas))
            {
                var coordenadasArray = coordenadas.Split(',');

                if (coordenadasArray.Length > 1)
                {
                    Latitude = ToDouble(coordenadasArray[0]);
                    Longitude = ToDouble(coordenadasArray[1]);
                }
            }

        }

        private double ToDouble(string value) {
            return double.Parse(value, CultureInfo.InvariantCulture);           
        }
    }
}
