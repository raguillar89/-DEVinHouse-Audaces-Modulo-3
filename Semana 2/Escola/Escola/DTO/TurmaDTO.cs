using Escola.Models;

namespace Escola.DTO
{
    public class TurmaDTO
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public string Nome { get; set; }

        public TurmaDTO() { }

        public TurmaDTO(Turma turma)
        {
            Id = turma.Id;
            Curso = turma.Curso;
            Nome = turma.Nome;
        }
    }    
}
