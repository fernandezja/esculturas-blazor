using Esculturas.Core.Entities.Extensions;
using System;
using Xunit;

namespace Esculturas.Core.Entities.Tests
{
    public class CoordenadaTest
    {
        [Fact]
        public void DistanciaTest()
        {
            var result = new Coordenada(48.672309, 15.695585)
                                .DistanciaA(
                                    new Coordenada(48.237867, 16.389477),
                                    UnidadDeDistancia.Kilometers
                                );

            Assert.Equal(70.367455700524275, result);
        }

        [Fact]
        public void DistanciaChacoCorrientesTest()
        {
            var point1Resistencia = new Coordenada(-27.451389, -58.986667);
            var point2Corrientes = new Coordenada(-27.483333, -58.816667);

            var result = point1Resistencia.DistanciaA(point2Corrientes, UnidadDeDistancia.Kilometers);

            Assert.Equal(17.143426058917839, result);
        }
    }
}
