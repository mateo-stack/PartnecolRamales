using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partnecol.Core;

public class ConsultaRamalesService
{
    private readonly IReferenciaRepository _repositorio;

    public ConsultaRamalesService(IReferenciaRepository repositorio)
    {
        _repositorio = repositorio
            ?? throw new ArgumentNullException(nameof(repositorio));
    }

    public ResultadoEquivalencia ConsultarEquivalencia(
        string idReferencia, string colorRequerido)
    {
        if (string.IsNullOrWhiteSpace(idReferencia))
            throw new ArgumentException(
                "La referencia es obligatoria.", nameof(idReferencia));

        if (string.IsNullOrWhiteSpace(colorRequerido))
            throw new ArgumentException(
                "El color requerido es obligatorio.", nameof(colorRequerido));

        string id = idReferencia.Trim();
        string color = colorRequerido.Trim();

        var referencia = _repositorio.BuscarReferencia(id);

        if (referencia is null)
            throw new KeyNotFoundException(
                $"No existe la referencia {id}.");

        var equivalencia =
            _repositorio.BuscarEquivalencia(id, color);

        if (equivalencia is null)
            return ResultadoEquivalencia.SinEquivalencia();

        return ResultadoEquivalencia.ConEquivalencia(
            equivalencia.ColorReemplazo);
    }
    public ReferenciaRamal ConsultarReferencia(string idReferencia)
    {
        if (string.IsNullOrWhiteSpace(idReferencia))
            throw new ArgumentException(
                "La referencia es obligatoria.", nameof(idReferencia));

        string id = idReferencia.Trim();

        var referencia = _repositorio.BuscarReferencia(id);

        if (referencia is null)
            throw new KeyNotFoundException(
                $"No existe la referencia {id}.");

        return referencia;
    }

    public MarcacionConector ConsultarMarcacion(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentException(
                "El número de marcación debe ser mayor que cero.",
                nameof(numero));
        }

        var marcacion = _repositorio.BuscarMarcacion(numero);

        if (marcacion is null)
        {
            throw new KeyNotFoundException(
                $"No existe una marcación registrada con el número {numero}.");
        }

        return marcacion;
    }

}
