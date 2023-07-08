using Escola.Context;
using Escola.Models;
using Escola.Repository.Interface;

namespace Escola.Repository
{
    public class MateriaRepository : BaseRepository<Materia, int>, IMateriaRepository
    {
        public MateriaRepository(EscolaDbContext context) : base(context) { }

        public Materia ObterPorNome(string nome)
        {
            return _context.Materias.FirstOrDefault(x => nome == x.Nome);
        }
    }
}
