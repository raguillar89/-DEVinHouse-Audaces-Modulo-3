using Escola.DTO;

namespace Escola.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Materia() { }

        public Materia(MateriaDTO materiaDTO)
        {
            Id = materiaDTO.Id;
            Nome = materiaDTO.Nome;
        }

        public void Update(Materia materia)
        {
            Nome = materia.Nome;
        }
    }
}
