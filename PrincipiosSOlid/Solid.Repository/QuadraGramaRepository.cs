using Solid.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solid.Repository
{
    public class QuadraGramaRepository
    {
        private static List<QuadraGramaDomain> quadras = new List<QuadraGramaDomain>();
        public int Salvar(QuadraGramaDomain quadra)
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
        public bool Deletar(QuadraGramaDomain quadra)
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
        public List<QuadraGramaDomain> ListarTodos()
        {
            return quadras;
        }
        public QuadraGramaDomain ListarPorId(int id)
        {
            return quadras.Single(x => x.Id == id);
        }
    }
}
