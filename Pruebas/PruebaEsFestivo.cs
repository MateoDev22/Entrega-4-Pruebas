using Moq;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace apiFestivos.PruebasUnitarias
{
    public class PruebaEsFestivo
    {
        private Mock<IFestivoRepositorio> _mockRepositorio;
        private FestivoServicio _servicio;

        public PruebaEsFestivo()
        {
            _mockRepositorio = new Mock<IFestivoRepositorio>();
            _servicio = new FestivoServicio(_mockRepositorio.Object);
        }

        //Verificar que EsFestivo devuelve true si la fecha es un festivo
        [Fact]
        public async Task EsFestivo_FechaEsFestivo_RetornaTrue()
        {
            // Arrange
            var festivo = new Festivo { IdTipo = 1, Mes = 8, Dia = 7, Nombre = "Batalla de Boyacá" };
            var fechaFestiva = new DateTime(2025, 8, 7);

            _mockRepositorio.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo> { festivo });

            // Act
            var resultado = await _servicio.EsFestivo(fechaFestiva);

            // Assert
            Assert.True(resultado);  // Debería ser un festivo
        }

        // Verificar que EsFestivo devuelve false si la fecha no es un festivo
        [Fact]
        public async Task EsFestivo_FechaNoEsFestivo_RetornaFalse()
        {
            // Arrange
            var festivo = new Festivo { IdTipo = 1, Mes = 8, Dia = 7, Nombre = "Batalla de Boyacá" };
            var fechaNoFestiva = new DateTime(2025, 8, 8); // La fecha siguiente no es festiva

            _mockRepositorio.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo> { festivo });

            // Act
            var resultado = await _servicio.EsFestivo(fechaNoFestiva);

            // Assert
            Assert.False(resultado);  // No debería ser un festivo
        }
    }
}
