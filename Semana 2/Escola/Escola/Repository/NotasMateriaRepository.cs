using Escola.Context;
using Escola.Models;
using Escola.Repository.Interface;

namespace Escola.Repository
{
    public class NotasMateriaRepository : BaseRepository<NotasMateria, int>, INotasMateriaRepository
    {
        public NotasMateriaRepository(EscolaDbContext context) : base(context) { }

        public NotasMateria ObterPorBoletim(int idBoletim)
        {
            return _context.NotasMaterias.FirstOrDefault(x => idBoletim == x.IdBoletim);
        }
    }
}
