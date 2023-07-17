using Escola.Models;

namespace Escola.Repository.Interface
{
    public interface INotasMateriaRepository : IBaseRepository<NotasMateria,int>
    {
        public NotasMateria ObterPorBoletim(int idBoletim);
    }
}
