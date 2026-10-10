using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Partnecol.Core;

namespace Partnecol.Tests;

[TestFixture]
public class ConsultaRamalesServiceTests
{
    private Mock<IReferenciaRepository> _repositorio = null!;
    private ConsultaRamalesService _servicio = null!;

    [SetUp]
    public void Preparar()
    {
        _repositorio = new Mock<IReferenciaRepository>();
        _servicio = new ConsultaRamalesService(_repositorio.Object);
    }

    [Test]
    public void ReferenciaVacia_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            _servicio.ConsultarEquivalencia(" ", "AZUL"));

        _repositorio.Verify(
            r => r.BuscarReferencia(It.IsAny<string>()),
            Times.Never);
    }

    [Test]
    public void ColorVacio_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            _servicio.ConsultarEquivalencia("REF-001", ""));

        _repositorio.Verify(
            r => r.BuscarReferencia(It.IsAny<string>()),
            Times.Never);
    }

    [Test]
    public void ReferenciaInexistente_LanzaKeyNotFoundException()
    {
        _repositorio
            .Setup(r => r.BuscarReferencia("REF-001"))
            .Returns((ReferenciaRamal?)null);

        Assert.Throws<KeyNotFoundException>(() =>
            _servicio.ConsultarEquivalencia("REF-001", "AZUL"));

        _repositorio.Verify(
            r => r.BuscarEquivalencia(
                It.IsAny<string>(), It.IsAny<string>()),
            Times.Never);
    }

    [Test]
    public void SinEquivalencia_RetornaResultadoNegativo()
    {
        _repositorio
            .Setup(r => r.BuscarReferencia("REF-001"))
            .Returns(new ReferenciaRamal
            {
                IdReferencia = "REF-001",
                Nombre = "Ramal de prueba"
            });

        _repositorio
            .Setup(r => r.BuscarEquivalencia("REF-001", "AZUL"))
            .Returns((EquivalenciaCable?)null);

        var resultado = _servicio.ConsultarEquivalencia(
            "REF-001", "AZUL");

        Assert.That(resultado.Encontrada, Is.False);
        Assert.That(resultado.ColorReemplazo, Is.Null);
    }

    [Test]
    public void EquivalenciaEncontrada_RetornaColorReemplazo()
    {
        _repositorio
            .Setup(r => r.BuscarReferencia("REF-001"))
            .Returns(new ReferenciaRamal
            {
                IdReferencia = "REF-001",
                Nombre = "Ramal de prueba"
            });

        _repositorio
            .Setup(r => r.BuscarEquivalencia("REF-001", "AZUL"))
            .Returns(new EquivalenciaCable
            {
                IdReferencia = "REF-001",
                ColorRequerido = "AZUL",
                ColorReemplazo = "VERDE"
            });

        var resultado = _servicio.ConsultarEquivalencia(
            "REF-001", "AZUL");

        Assert.That(resultado.Encontrada, Is.True);
        Assert.That(resultado.ColorReemplazo, Is.EqualTo("VERDE"));
    }

    [Test]
    public void ErrorDelRepositorio_SePropaga()
    {
        _repositorio
            .Setup(r => r.BuscarReferencia("REF-001"))
            .Throws(new InvalidOperationException("Error de acceso"));

        Assert.Throws<InvalidOperationException>(() =>
            _servicio.ConsultarEquivalencia("REF-001", "AZUL"));
    }
}