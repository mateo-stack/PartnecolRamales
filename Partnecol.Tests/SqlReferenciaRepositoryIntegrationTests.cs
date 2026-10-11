using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NUnit.Framework;
using Partnecol.Core;

namespace Partnecol.Tests;

[TestFixture]
[Category("Integration")]
public class SqlReferenciaRepositoryIntegrationTests
{
    private const string CadenaConexion =
        @"Server=MATEO-PC\SQLEXPRESS;Database=PartnecolRamales;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

    [Test]
    public void BuscarReferencia_ReferenciaExistente_RetornaDatos()
    {
        var repositorio = new SqlReferenciaRepository(CadenaConexion);

        var resultado = repositorio.BuscarReferencia("REF-001");

        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado!.IdReferencia, Is.EqualTo("REF-001"));
        Assert.That(resultado.Nombre, Is.EqualTo("Ramal de prueba"));
    }

    [Test]
    public void BuscarEquivalencia_EquivalenciaExistente_RetornaDatos()
    {
        var repositorio = new SqlReferenciaRepository(CadenaConexion);

        var resultado = repositorio.BuscarEquivalencia(
            "REF-001", "Rojo");

        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado!.ColorRequerido, Is.EqualTo("Rojo"));
        Assert.That(resultado.ColorReemplazo, Is.EqualTo("Azul"));
    }

    [Test]
    public void BuscarReferencia_ReferenciaInexistente_RetornaNull()
    {
        var repositorio = new SqlReferenciaRepository(CadenaConexion);

        var resultado = repositorio.BuscarReferencia("REF-INEXISTENTE");

        Assert.That(resultado, Is.Null);
    }

    [Test]
    public void BuscarEquivalencia_EquivalenciaInexistente_RetornaNull()
    {
        var repositorio = new SqlReferenciaRepository(CadenaConexion);

        var resultado = repositorio.BuscarEquivalencia(
            "REF-001", "Verde");

        Assert.That(resultado, Is.Null);
    }

}

