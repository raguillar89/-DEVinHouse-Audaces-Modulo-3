using Escola.DTO;

namespace Escola.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public string Nome { get; set; }

        public Turma() { }

        public Turma(TurmaDTO turma)
        {
            Id = turma.Id;
            Curso = turma.Curso;
            Nome = turma.Nome;
        }

        public void Update(Turma turma)
        {
            Curso = turma.Curso;
            Nome = turma.Nome;
        }
    }
}
