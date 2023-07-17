using Escola.Models;

namespace Escola.Services.Interface
{
    public interface IUsuarioService
    {
        public Usuario Criar(Usuario usuario);
        public Usuario ObterPorId(string login);
        public Usuario Atualizar(Usuario usuario);
        public List<Usuario> ObterUsuarios();
        public void DeletarUsuario(string login);
    }
}
