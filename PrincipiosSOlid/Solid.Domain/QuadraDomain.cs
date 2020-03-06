
using System;

namespace Solid.Domain
{
    public abstract class QuadraDomain
    {
        public int Id { get; set; }
        public string DimensoesEmMetros { get; set; }
        public string Endereco { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataUltimaManutencao { get; set; }
    }
}
