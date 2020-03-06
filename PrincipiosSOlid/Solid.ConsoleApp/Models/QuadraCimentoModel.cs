using Solid.Domain;
using Solid.Repository;
using System;

namespace Solid.ConsoleApp.Models
{
    class QuadraCimentoModel
    {
        public bool Reparar(QuadraCimentoDomain quadra)
        {
            quadra.QualidadePintura = "PERFEITO";
            quadra.DataUltimaManutencao = DateTime.Now;
            var ret = new QuadraCimentoRepository().Salvar(quadra);
            return ret > 0;
        }
    }
}
