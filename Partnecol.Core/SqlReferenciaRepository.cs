using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Partnecol.Core;

public class SqlReferenciaRepository : IReferenciaRepository
{
    private readonly string _cadenaConexion;

    public SqlReferenciaRepository(string cadenaConexion)
    {
        if (string.IsNullOrWhiteSpace(cadenaConexion))
            throw new ArgumentException(
                "La cadena de conexión es obligatoria.",
                nameof(cadenaConexion));

        _cadenaConexion = cadenaConexion;
    }

    public ReferenciaRamal? BuscarReferencia(string idReferencia)
    {
        const string sql = @"
    SELECT IdReferencia, Nombre
    FROM ReferenciasRamal
    WHERE IdReferencia = @IdReferencia;";

        using var conexion = new SqlConnection(_cadenaConexion);
        using var comando = new SqlCommand(sql, conexion);

        comando.Parameters.Add(
            "@IdReferencia", SqlDbType.NVarChar, 50).Value = idReferencia;

        conexion.Open();

        using var lector = comando.ExecuteReader();

        if (!lector.Read())
            return null;

        return new ReferenciaRamal
        {
            IdReferencia = lector.GetString(0),
            Nombre = lector.GetString(1)
        };
    }

    public EquivalenciaCable? BuscarEquivalencia(
        string idReferencia, string colorRequerido)
    {
        const string sql = @"
    SELECT IdReferencia, ColorRequerido, ColorReemplazo
    FROM EquivalenciasCable
    WHERE IdReferencia = @IdReferencia
      AND ColorRequerido = @ColorRequerido;";

        using var conexion = new SqlConnection(_cadenaConexion);
        using var comando = new SqlCommand(sql, conexion);

        comando.Parameters.Add(
            "@IdReferencia", SqlDbType.NVarChar, 50).Value = idReferencia;

        comando.Parameters.Add(
            "@ColorRequerido", SqlDbType.NVarChar, 50).Value =
            colorRequerido;

        conexion.Open();

        using var lector = comando.ExecuteReader();

        if (!lector.Read())
            return null;

        return new EquivalenciaCable
        {
            IdReferencia = lector.GetString(0),
            ColorRequerido = lector.GetString(1),
            ColorReemplazo = lector.GetString(2)
        };
    }
}

