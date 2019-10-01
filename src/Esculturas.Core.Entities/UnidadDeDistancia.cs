using System;
using System.Collections.Generic;
using System.Text;

namespace Esculturas.Core.Entities
{
    /// <summary>
    /// Example
    /// http://www.kylesconverter.com/length/miles-to-meters
    /// https://www.calculateme.com/length/miles/to-meters/1
    /// </summary>
    public class UnidadDeDistancia
    {
        public static UnidadDeDistancia Meters = new UnidadDeDistancia(1609.344);
        public static UnidadDeDistancia Kilometers = new UnidadDeDistancia(1.609344);
        public static UnidadDeDistancia NauticalMiles = new UnidadDeDistancia(0.8684);
        public static UnidadDeDistancia Miles = new UnidadDeDistancia(1);

        private readonly double _fromMilesFactor;

        private UnidadDeDistancia(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public double ConvertFromMiles(double input)
        {
            return input * _fromMilesFactor;
        }
    }
}
