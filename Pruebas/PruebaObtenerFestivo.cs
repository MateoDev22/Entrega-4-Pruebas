using Moq;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Dominio.Entidades;
using System;
using Xunit;
using apiFestivos.Dominio.DTOs;

namespace apiFestivos.PruebasUnitarias
{
    public class FestivoServicioPruebas
    {
        private Mock<IFestivoRepositorio> _mockRepositorio;
        private FestivoServicio _servicio;

        public FestivoServicioPruebas()
        {
            _mockRepositorio = new Mock<IFestivoRepositorio>();
            _servicio = new FestivoServicio(_mockRepositorio.Object);
        }

        [Fact]  
        public void ObtenerFestivo_Tipo1_RetornaFechaEsperada()
        {
            // Arrange
            int añoPrueba = 2025;
            var festivoTipo1 = new Festivo { IdTipo = 1, Mes = 8, Dia = 7, Nombre = "Batalla de Boyacá" };
            var fechaEsperada = new DateTime(2025, 8, 7);

            // Act
            var resultado = _servicio.ObtenerFestivo(añoPrueba, festivoTipo1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(fechaEsperada, resultado.Fecha);
            Assert.Equal(festivoTipo1.Nombre, resultado.Nombre);
        }

        [Fact]  
        public void ObtenerFestivo_Tipo2_CaeEnLunesSiguiente()
        {
            // Arrange
            int añoPrueba = 2025;
            var festivoTipo2 = new Festivo { IdTipo = 2, Mes = 1, Dia = 6, Nombre = "Día de Reyes" };
            var fechaEsperada = new DateTime(2025, 1, 6); // Ya es lunes

            var festivoTipo2_martes = new Festivo { IdTipo = 2, Mes = 1, Dia = 7, Nombre = "Otro Festivo Movible" };
            var fechaEsperada_martes = new DateTime(2025, 1, 13); // Lunes siguiente al 7 de enero

            // Act
            var resultado_lunes = _servicio.ObtenerFestivo(añoPrueba, festivoTipo2);
            var resultado_martes = _servicio.ObtenerFestivo(añoPrueba, festivoTipo2_martes);

            // Assert
            Assert.NotNull(resultado_lunes);
            Assert.Equal(fechaEsperada, resultado_lunes.Fecha);
            Assert.Equal(festivoTipo2.Nombre, resultado_lunes.Nombre);

            Assert.NotNull(resultado_martes);
            Assert.Equal(fechaEsperada_martes, resultado_martes.Fecha);
            Assert.Equal(festivoTipo2_martes.Nombre, resultado_martes.Nombre);
        }

        [Fact] 
        public void ObtenerFestivo_Tipo4_SeDesplazaALunesRelativoASemanaSanta()
        {
            // Arrange
            int añoPrueba = 2025;
            var festivoTipo4 = new Festivo { IdTipo = 4, DiasPascua = -7, Nombre = "Lunes Santo" };
            var fechaEsperada = new DateTime(2025, 4, 14);

            // Act
            var resultado = _servicio.ObtenerFestivo(añoPrueba, festivoTipo4);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(fechaEsperada, resultado.Fecha);
            Assert.Equal(festivoTipo4.Nombre, resultado.Nombre);
        }
    }
}
