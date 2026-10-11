
using System;
using System.Collections.Generic;
using Partnecol.Core;

const string cadenaConexion =
    @"Server=MATEO-PC\SQLEXPRESS;Database=PartnecolRamales;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

var repositorio = new SqlReferenciaRepository(cadenaConexion);
var servicio = new ConsultaRamalesService(repositorio);

bool continuar = true;

while (continuar)
{
    Console.Clear();

    Console.WriteLine("==========================================");
    Console.WriteLine("       PARTNECOL S.A.S.");
    Console.WriteLine("       SISTEMA DE CONSULTA DE RAMALES");
    Console.WriteLine("==========================================");
    Console.WriteLine("1. Consultar equivalencia de cable");
    Console.WriteLine("2. Consultar datos de una referencia");
    Console.WriteLine("3. Consultar marcación de conectores");
    Console.WriteLine("0. Salir");
    Console.WriteLine("------------------------------------------");
    Console.Write("Seleccione una opción: ");

    string? opcion = Console.ReadLine();

    switch (opcion?.Trim())
    {
        case "1":
            ConsultarEquivalencia();
            break;

        case "2":
            ConsultarReferencia();
            break;

        case "3":
            ConsultarMarcacion();
            break;

        case "0":
            continuar = false;
            Console.WriteLine("\nGracias por utilizar el sistema.");
            break;

        default:
            Console.WriteLine(
                "\nOpción inválida. Seleccione 1, 2, 3 o 0.");
            Pausar();
            break;
    }
}

void ConsultarEquivalencia()
{
    Console.WriteLine("\n--- CONSULTAR EQUIVALENCIA ---");

    string referencia = LeerDato("Código de referencia: ");
    string color = LeerDato("Color de cable requerido: ");

    if (string.IsNullOrWhiteSpace(referencia) ||
        string.IsNullOrWhiteSpace(color))
    {
        Console.WriteLine(
            "\nDebe ingresar la referencia y el color.");
        Pausar();
        return;
    }

    try
    {
        var resultado = servicio.ConsultarEquivalencia(
            referencia, color);

        Console.WriteLine();

        if (resultado.Encontrada)
        {
            Console.WriteLine("EQUIVALENCIA ENCONTRADA");
            Console.WriteLine($"Referencia: {referencia.Trim()}");
            Console.WriteLine($"Color requerido: {color.Trim()}");
            Console.WriteLine(
                $"Color de reemplazo: {resultado.ColorReemplazo}");
        }
        else
        {
            Console.WriteLine(
                "No existe una equivalencia registrada para ese color.");
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\nDato inválido: {ex.Message}");
    }
    catch (KeyNotFoundException ex)
    {
        Console.WriteLine($"\nReferencia no encontrada: {ex.Message}");
    }
    catch (Exception)
    {
        Console.WriteLine(
            "\nNo fue posible consultar los datos. " +
            "Verifique la conexión con la base de datos.");
    }

    Pausar();
}

void ConsultarReferencia()
{
    Console.WriteLine("\n--- CONSULTAR REFERENCIA ---");

    string referencia = LeerDato("Código de referencia: ");

    if (string.IsNullOrWhiteSpace(referencia))
    {
        Console.WriteLine("\nDebe ingresar un código de referencia.");
        Pausar();
        return;
    }

    try
    {
        var resultado = servicio.ConsultarReferencia(referencia);

        Console.WriteLine("\nREFERENCIA ENCONTRADA");
        Console.WriteLine($"Código: {resultado.IdReferencia}");
        Console.WriteLine($"Nombre: {resultado.Nombre}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\nDato inválido: {ex.Message}");
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine(
            "\nLa referencia indicada no está registrada.");
    }
    catch (Exception)
    {
        Console.WriteLine(
            "\nNo fue posible consultar los datos. " +
            "Verifique la conexión con la base de datos.");
    }

    Pausar();
}

void ConsultarMarcacion()
{
    Console.WriteLine("\n--- CONSULTAR MARCACIÓN DE CONECTORES ---");

    string entrada = LeerDato("Número de marcación: ");

    if (!int.TryParse(entrada, out int numero))
    {
        Console.WriteLine(
            "\nEntrada inválida. Debe ingresar un número entero.");
        Pausar();
        return;
    }

    try
    {
        var resultado = servicio.ConsultarMarcacion(numero);

        Console.WriteLine("\nMARCACIÓN ENCONTRADA");
        Console.WriteLine($"Número: {resultado.Numero}");
        Console.WriteLine($"Función: {resultado.Funcion}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\nDato inválido: {ex.Message}");
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine(
            "\nEl número de marcación no está registrado en el catálogo.");
    }
    catch (Exception)
    {
        Console.WriteLine(
            "\nNo fue posible consultar los datos. " +
            "Verifique la conexión con la base de datos.");
    }

    Pausar();
}


string LeerDato(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine() ?? "";
}

void Pausar()
{
    Console.WriteLine("\nPresione ENTER para continuar...");
    Console.ReadLine();
}
