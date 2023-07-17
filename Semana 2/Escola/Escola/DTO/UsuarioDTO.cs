using Escola.Models;

namespace Escola.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoAcesso { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool Interno { get; set; }

        public UsuarioDTO() { }

        public UsuarioDTO(Usuario usuario)
        {
            Nome = usuario.Nome;
            TipoAcesso = usuario.TipoAcesso;
            User = usuario.User;
            Password = usuario.Password;
        }
    }
}
