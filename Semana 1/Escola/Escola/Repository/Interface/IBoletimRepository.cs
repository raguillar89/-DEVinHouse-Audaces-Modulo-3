using Escola.Models;

namespace Escola.Repository.Interface
{
    public interface IBoletimRepository : IBaseRepository<Boletim,int>
    {
        public List<Boletim> ObterPorIdAluno(int idAluno);
    }
}
