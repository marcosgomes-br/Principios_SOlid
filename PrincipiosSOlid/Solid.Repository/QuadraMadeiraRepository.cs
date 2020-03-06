using Solid.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solid.Repository
{
    public class QuadraMadeiraRepository
    {
        private static List<QuadraMadeiraDomain> quadras = new List<QuadraMadeiraDomain>();
        public int Salvar(QuadraMadeiraDomain quadra)
        {
            if (quadra.Id > 0)
            {
                quadras.Remove(quadra);
                quadras.Add(quadra);
            }
            else
            {
                quadra.Id = quadras.Count + 1;
                quadras.Add(quadra);
            }
            return quadra.Id;
        }
        public bool Deletar(QuadraMadeiraDomain quadra)
        {
            try
            {
                quadras.Remove(quadra);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<QuadraMadeiraDomain> ListarTodos()
        {
            return quadras;
        }
        public QuadraMadeiraDomain ListarPorId(int id)
        {
            return quadras.Single(x => x.Id == id);
        }
    }
}
