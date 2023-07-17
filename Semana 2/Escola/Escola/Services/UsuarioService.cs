using Escola.Exceptions;
using Escola.Models;
using Escola.Repository.Interface;
using Escola.Services.Interface;
using Escola.Utils;

namespace Escola.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public Usuario Atualizar(Usuario usuario)
        {

            var usuarioDb = ObterPorId(usuario.User);
            if (usuarioDb == null)
                throw new KeyNotFoundException("Usuario Não existe");

            usuarioDb.Update(usuario);
            if (!string.IsNullOrEmpty(usuario.Password))
                usuarioDb.Password = Criptografia.CriptografarSenha(usuario.Password);
            _usuarioRepository.Atualizar(usuarioDb);
            return usuario;
        }

        public Usuario Criar(Usuario usuario)
        {
            usuario.Password = Criptografia.CriptografarSenha(usuario.Password);
            return _usuarioRepository.Inserir(usuario);
        }

        public void DeletarUsuario(string login)
        {
            var usuarioDb = ObterPorId(login);
            if (usuarioDb == null)
                throw new KeyNotFoundException("Usuario Nõa existe");

            _usuarioRepository.Excluir(usuarioDb);
        }

        public List<Usuario> ObterUsuarios()
        {
            return _usuarioRepository.ObterTodos();
        }

        public Usuario ObterPorId(string login)
        {
            return _usuarioRepository.ObterPorId(login);
        }
    }
}
