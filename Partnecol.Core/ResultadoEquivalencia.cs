using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partnecol.Core;

public class ResultadoEquivalencia
{
    public bool Encontrada { get; private set; }
    public string? ColorReemplazo { get; private set; }

    private ResultadoEquivalencia(
        bool encontrada, string? colorReemplazo)
    {
        Encontrada = encontrada;
        ColorReemplazo = colorReemplazo;
    }

    public static ResultadoEquivalencia SinEquivalencia() =>
        new(false, null);

    public static ResultadoEquivalencia ConEquivalencia(string color) =>
        new(true, color);
}
