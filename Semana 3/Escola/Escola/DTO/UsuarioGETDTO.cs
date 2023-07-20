using Escola.Models;

namespace Escola.DTO
{
    public class UsuarioGETDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoAcesso { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool Interno { get; set; }

        public UsuarioGETDTO() { }

        public UsuarioGETDTO(Usuario usuario)
        {
            Nome = usuario.Nome;
            User = usuario.User;
            TipoAcesso = usuario.TipoAcesso;
            Interno = usuario.Interno;
        }
    }
}
