using Esculturas.Core.Data.Interfaces;
using Esculturas.Core.Entities;
using Microsoft.Spatial;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Esculturas.Core.Business.Tests
{
    public class EsculturaBusinessTest
    {
        private Mock<IEsculturaRepository> _esculturaRepository { get; set; }

        public EsculturaBusinessTest()
        {
            _esculturaRepository = new Mock<IEsculturaRepository>();


            _esculturaRepository.Setup(x => x.GetList()).Returns(GetListEsculturas(10));
        }


        [Fact]
        public void GetListTest()
        {
            var esculturaBusiness = new EsculturaBusiness(_esculturaRepository.Object);
            var result = esculturaBusiness.GetList();

            Assert.NotEmpty(result);
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void DistanceTest()
        {
            var esculturaBusiness = new EsculturaBusiness(_esculturaRepository.Object);
            var escultura = esculturaBusiness.GetList()[0];

            var pointOfReference = new Coordenada(latitude: -58.99, longitude: -27.45);

            var distance = escultura.DistanciaA(pointOfReference);

            Assert.NotNull(escultura);
            Assert.True(distance > 0);
            Assert.Equal(0.93017688334267, distance);
        }

        private List<Escultura> GetListEsculturas(int count) {
            var list = new List<Escultura>();
            for (int i = 1; i <= count; i++)
            {
                list.Add(new Escultura()
                {
                    Nombre = $"Escultura Data{i}",
                    Coordenada = new Coordenada(latitude: -58.98178609950485, longitude: -27.44692119526704)
                });
            }
            return list;
        }
    }
}
