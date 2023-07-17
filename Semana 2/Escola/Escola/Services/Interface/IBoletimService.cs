using Escola.Models;

namespace Escola.Services.Interface
{
    public interface IBoletimService
    {
        public Boletim Criar(Boletim boletim);
        public Boletim ObterPorId(int id);
        public List<Boletim> ObterPorIdAluno(int idAluno);
        public Boletim Atualizar(Boletim boletim);
        public List<Boletim> ObterBoletins();
        public void DeletarBoletim(int id);
    }
}
