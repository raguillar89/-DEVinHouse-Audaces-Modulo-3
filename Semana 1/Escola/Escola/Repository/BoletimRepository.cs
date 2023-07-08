using Escola.Context;
using Escola.Models;
using Escola.Repository.Interface;

namespace Escola.Repository
{
    public class BoletimRepository : BaseRepository<Boletim, int>, IBoletimRepository
    {
        public BoletimRepository(EscolaDbContext context) : base(context) { }

        public List<Boletim> ObterPorIdAluno(int idAluno)
        {
            return _context.Set<Boletim>().Where(x => idAluno == x.IdAluno).ToList();
        }
    }
}
