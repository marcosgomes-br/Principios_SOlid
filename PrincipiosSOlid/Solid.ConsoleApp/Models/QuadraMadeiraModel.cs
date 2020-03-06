using Solid.Domain;
using Solid.Repository;
using System;

namespace Solid.ConsoleApp.Models
{
    class QuadraMadeiraModel
    {
        public bool Reparar(QuadraMadeiraDomain quadra)
        {
            quadra.QuantidadeMadeiraSolta = 0;
            quadra.DataUltimaManutencao = DateTime.Now;
            var ret = new QuadraMadeiraRepository().Salvar(quadra);
            return ret > 0;
        }
    }
}
