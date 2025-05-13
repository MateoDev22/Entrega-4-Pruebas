using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Dominio.Entidades;

namespace MyTestProject
{
    public class UnitTest1
    {
        private readonly Mock<IFestivoRepositorio> _repositorioMock;
        private readonly FestivoServicio _servicio;

        public UnitTest1()
        {
            _repositorioMock = new Mock<IFestivoRepositorio>();
            _servicio = new FestivoServicio(_repositorioMock.Object);
        }

        [Fact]
        public async Task EsFestivo_FechaEsFestiva_RetornaTrue()
        {
            // Arrange
            var fechaFestiva = new DateTime(2025, 1, 1);
            var festivos = new List<Festivo>
            {
                new Festivo { Nombre = "Año Nuevo", IdTipo = 1, Dia = 1, Mes = 1 }
            };
            _repositorioMock.Setup(r => r.ObtenerTodos()).ReturnsAsync(festivos);

            // Act
            var resultado = await _servicio.EsFestivo(fechaFestiva);

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task EsFestivo_FechaNoEsFestiva_RetornaFalse()
        {
            // Arrange
            var fecha = new DateTime(2025, 2, 2);
            var festivos = new List<Festivo>
            {
                new Festivo { Nombre = "Año Nuevo", IdTipo = 1, Dia = 1, Mes = 1 }
            };
            _repositorioMock.Setup(r => r.ObtenerTodos()).ReturnsAsync(festivos);

            // Act
            var resultado = await _servicio.EsFestivo(fecha);

            // Assert
            resultado.Should().BeFalse();
        }
    }
}
