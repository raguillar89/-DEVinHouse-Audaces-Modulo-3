using Escola.Models;

namespace Escola.Repository.Interface
{
    public interface IMateriaRepository : IBaseRepository<Materia,int>
    {
        public Materia ObterPorNome(string nome);
    }
}
