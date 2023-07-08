using Escola.Models;

namespace Escola.DTO
{
    public class NotasMateriaDTO
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int IdMateria { get; set; }
        public int IdBoletim { get; set; }

        public NotasMateriaDTO() { }

        public NotasMateriaDTO(NotasMateria notasMateria)
        {
            Id = notasMateria.Id;
            Nota = notasMateria.Nota;
            IdMateria = notasMateria.IdMateria;
            IdBoletim = notasMateria.IdBoletim;
        }
    }
}
