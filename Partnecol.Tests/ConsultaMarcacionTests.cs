
using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Partnecol.Core;

namespace Partnecol.Tests;

[TestFixture]
public class ConsultaMarcacionTests
{
    private Mock<IReferenciaRepository> _repositorio = null!;
    private ConsultaRamalesService _servicio = null!;

    [SetUp]
    public void Configurar()
    {
        _repositorio = new Mock<IReferenciaRepository>();
        _servicio = new ConsultaRamalesService(
            _repositorio.Object);
    }

    [Test]
    public void ConsultarMarcacionExistente_RetornaFuncion()
    {
        _repositorio
            .Setup(r => r.BuscarMarcacion(14))
            .Returns(new MarcacionConector
            {
                Numero = 14,
                Funcion = "CDI"
            });

        var resultado = _servicio.ConsultarMarcacion(14);

        Assert.That(resultado.Numero, Is.EqualTo(14));
        Assert.That(resultado.Funcion, Is.EqualTo("CDI"));
    }

    [Test]
    public void ConsultarMarcacionInexistente_LanzaKeyNotFoundException()
    {
        _repositorio
            .Setup(r => r.BuscarMarcacion(999))
            .Returns((MarcacionConector?)null);

        Assert.Throws<KeyNotFoundException>(
            () => _servicio.ConsultarMarcacion(999));
    }

    [Test]
    public void ConsultarMarcacionCero_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => _servicio.ConsultarMarcacion(0));

        _repositorio.Verify(
            r => r.BuscarMarcacion(It.IsAny<int>()),
            Times.Never);
    }

    [Test]
    public void ConsultarMarcacionNegativa_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => _servicio.ConsultarMarcacion(-1));

        _repositorio.Verify(
            r => r.BuscarMarcacion(It.IsAny<int>()),
            Times.Never);
    }
}
