using Solid.Domain;
using Solid.Repository;
using System;

namespace Solid.ConsoleApp.Models
{
    class QuadraGramaModel
    {
        public bool Reparar(QuadraGramaDomain quadra)
        {
            quadra.AlturaGramaCm = quadra.AlturaIdealGrama;
            quadra.DataUltimaManutencao = DateTime.Now;
            var ret = new QuadraGramaRepository().Salvar(quadra);
            return ret > 0;
        }
    }
}
