using Escola.Context;
using Escola.Models;
using Escola.Repository.Interface;

namespace Escola.Repository
{
    public class TurmaRepository : BaseRepository<Turma, int>, ITurmaRepository
    {
        public TurmaRepository(EscolaDbContext context) : base(context) { }
    }
}
