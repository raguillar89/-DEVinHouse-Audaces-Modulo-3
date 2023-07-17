using Escola.DTO;

namespace Escola.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public string Genero { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }

        public Aluno() { }

        public Aluno(AlunoDTO aluno)
        {
            Id = aluno.Id;
            Nome = aluno.Nome;
            Sobrenome = aluno.Sobrenome;
            Idade = aluno.Idade;
            Genero = aluno.Genero;
            Telefone = aluno.Telefone;
            Email = aluno.Email;

            if (DateTime.TryParse(aluno.DataNascimento, out var datanascimento))
                DataNascimento = datanascimento;
            else
                throw new ArgumentException("Erro ao converter a data de nascimento");
        }

        public void Update(Aluno aluno)
        {
            Nome = aluno.Nome;
            Sobrenome = aluno.Sobrenome;
            Idade = aluno.Idade;
            Genero = aluno.Genero;
            Telefone = aluno.Telefone;
            Email = aluno.Email;
            DataNascimento = aluno.DataNascimento;
        }
    }
}
