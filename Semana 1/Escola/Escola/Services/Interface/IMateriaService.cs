using Escola.Models;

namespace Escola.Services.Interface
{
    public interface IMateriaService
    {
        public Materia Criar(Materia materia);
        public Materia ObterPorId(int id);
        public Materia ObterPorNome(string nome);
        public Materia Atualizar(Materia materia);
        public List<Materia> ObterMaterias();
        public void DeletarMateria(int id);
    }
}
