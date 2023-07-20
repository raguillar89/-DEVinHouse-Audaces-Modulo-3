using Escola.DTO;

namespace Escola.Models
{
    public class NotasMateria
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int IdMateria { get; set; }
        public int IdBoletim { get; set; }

        public virtual Materia Materia { get; set; }
        public virtual Boletim Boletim { get; set; }

        public NotasMateria() { }

        public NotasMateria(NotasMateriaDTO notasMateriaDTO)
        {
            Id = notasMateriaDTO.Id;
            Nota = notasMateriaDTO.Nota;
            IdMateria = notasMateriaDTO.IdMateria;
            IdBoletim = notasMateriaDTO.IdBoletim;
        }

        public void Update(NotasMateria notasMateria)
        {
            Nota = notasMateria.Id;
            IdMateria = notasMateria.IdMateria;
            IdBoletim = notasMateria.IdBoletim;
        }
    }
}
