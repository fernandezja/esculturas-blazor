using System;
using System.Collections.Generic;
using System.Text;

namespace Esculturas.Core.Entities.Extensions
{
    /// <summary>
    /// Example from https://stackoverflow.com/questions/6366408/calculating-distance-between-two-latitude-and-longitude-geocoordinates
    /// </summary>
    public static class CoordenadaDistanciaExtensions
    {
        public static double DistanciaA(this Coordenada baseCoordinates, Coordenada targetCoordinates)
        {
            return DistanciaA(baseCoordinates, targetCoordinates, UnidadDeDistancia.Kilometers);
        }

        public static double DistanciaA(this Coordenada baseCoordinates, Coordenada targetCoordinates, UnidadDeDistancia unidadDeDistancia)
        {
            var baseRad = Math.PI * baseCoordinates.Latitude / 180;
            var targetRad = Math.PI * targetCoordinates.Latitude / 180;
            var theta = baseCoordinates.Longitude - targetCoordinates.Longitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unidadDeDistancia.ConvertFromMiles(dist);
        }
    }
}
