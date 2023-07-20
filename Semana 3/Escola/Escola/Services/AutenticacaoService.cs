using Escola.DTO;
using Escola.Services.Interface;
using Escola.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Escola.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly string _chaveJwt;

        public AutenticacaoService(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _chaveJwt = configuration.GetSection("jwtTokenChave").Get<string>();
        }

        public bool Autenticar(LoginDTO login)
        {
            var user = _usuarioService.ObterPorId(login.User);
            if (user != null)
            {
                return user.Password == Criptografia.CriptografarSenha(login.Password);
            }
            return false;
        }

        public string GerarToken(LoginDTO loginDTO)
        {
            var usuario = _usuarioService.ObterPorId(loginDTO.User);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_chaveJwt);

            // Utilização de clains Dinamicamente 
            //var clains = new Dictionary<string, object>
            //       {
            //          { ClaimTypes.Name, usuario.Login },
            //          { "Nome", usuario.Nome },
            //          { "Interno", usuario.Interno.ToString() },
            //          { ClaimTypes.Role, usuario.Permissao },
            //       };

            //if (true)
            //{
            //    clains.Add("minhachave", "Meu valor");
            //}

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(),
            //    Claims = clains,
            //    Expires = DateTime.UtcNow.AddHours(4),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                      new Claim(ClaimTypes.Name, usuario.User),
                      new Claim("Nome", usuario.Nome),
                      new Claim("Interno", usuario.Interno.ToString()),
                      new Claim(ClaimTypes.Role, usuario.TipoAcesso),
                  }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
