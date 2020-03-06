using Solid.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solid.Repository
{
    public class QuadraCimentoRepository
    {
        private static List<QuadraCimentoDomain> quadras = new List<QuadraCimentoDomain>();
        public int Salvar(QuadraCimentoDomain quadraCimento)
        {
            if(quadraCimento.Id > 0)
            {
                quadras.Remove(quadraCimento);
                quadras.Add(quadraCimento);
            }
            else
            {
                quadraCimento.Id = quadras.Count + 1;
                quadras.Add(quadraCimento);
            }
            return quadraCimento.Id;
        }
        public bool Deletar(QuadraCimentoDomain quadraCimento)
        {
            try
            {
                quadras.Remove(quadraCimento);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<QuadraCimentoDomain> ListarTodos()
        {
            return quadras;
        }
        public QuadraCimentoDomain ListarPorId(int id)
        {
            return quadras.Single(x => x.Id == id);
        }
    }
}
