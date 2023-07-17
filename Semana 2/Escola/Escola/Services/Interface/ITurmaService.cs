using Escola.Models;

namespace Escola.Services.Interface
{
    public interface ITurmaService
    {
        public Turma Criar(Turma turma);
        public Turma ObterPorId(int id);
        public Turma Atualizar(Turma turma);
        public List<Turma> ObterTurmas();
        public void DeletarTurma(int id);
    }
}
