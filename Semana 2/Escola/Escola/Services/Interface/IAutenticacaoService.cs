using Escola.DTO;

namespace Escola.Services.Interface
{
    public interface IAutenticacaoService
    {
        bool Autenticar(LoginDTO login);
        string GerarToken(LoginDTO loginDTO);
    }
}
