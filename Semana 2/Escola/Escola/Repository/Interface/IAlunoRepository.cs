using Escola.Models;

namespace Escola.Repository.Interface
{
    public interface IAlunoRepository : IBaseRepository<Aluno,int>
    {
        public bool EmailJaCadastrado(string email);
    }
}
