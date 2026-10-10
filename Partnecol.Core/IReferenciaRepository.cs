using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partnecol.Core;

public interface IReferenciaRepository
{
    ReferenciaRamal? BuscarReferencia(string idReferencia);

    EquivalenciaCable? BuscarEquivalencia(
        string idReferencia, string colorRequerido);
}
