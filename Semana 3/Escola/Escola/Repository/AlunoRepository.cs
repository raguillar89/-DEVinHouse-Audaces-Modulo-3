using Escola.Context;
using Escola.Models;
using Escola.Repository.Interface;

namespace Escola.Repository
{
    public class AlunoRepository : BaseRepository<Aluno,int>, IAlunoRepository
    {
        public AlunoRepository(EscolaDbContext context): base(context) { }

        public bool EmailJaCadastrado(string email) => _context.Alunos.Any(x => x.Email == email);
    }
}
