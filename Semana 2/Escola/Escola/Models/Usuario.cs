using Escola.DTO;

namespace Escola.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoAcesso { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool Interno { get; set; }

        public Usuario() { }

        public Usuario(UsuarioDTO usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            TipoAcesso = usuario.TipoAcesso;
            User = usuario.User;
            Password = usuario.Password;
        }

        public Usuario(UsuarioGETDTO usuarioGETDTO)
        {
            Nome = usuarioGETDTO.Nome;
            User = usuarioGETDTO.User;
        }

        public void Update(Usuario usuario)
        {
            Nome = usuario.Nome;
            User = usuario.User;
        }
    }
}
