using Escola.Models;

namespace Escola.Services.Interface
{
    public interface INotasMateriaService
    {
        public NotasMateria Criar(NotasMateria notasMateria);
        public NotasMateria ObterPorId(int id);
        public NotasMateria ObterPorBoletim(int idBoletim);
        public NotasMateria Atualizar(NotasMateria notasMateria);
        public List<NotasMateria> ObterNotasMateria();
        public void DeletarNotasMateria(int id);
    }
}
