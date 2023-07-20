using Escola.Context;
using Escola.Models;
using Escola.Repository.Interface;

namespace Escola.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario,string>, IUsuarioRepository
    {
        public UsuarioRepository(EscolaDbContext context) : base(context) { }        
    }
}
