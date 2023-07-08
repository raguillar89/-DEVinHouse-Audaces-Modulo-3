using Escola.DTO;

namespace Escola.Models
{
    public class Boletim
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int IdAluno { get; set; }

        public virtual Aluno Aluno { get; set; }

        public List<NotasMateria> NotasMateria { get; set; }

        public Boletim() { }

        public Boletim(BoletimDTO boletim)
        {
            Id = boletim.Id;
            IdAluno = boletim.IdAluno;

            if (DateTime.TryParse(boletim.OrderDate, out var orderdate))
                OrderDate = orderdate;
            else
                throw new ArgumentException("Erro ao converter o OrderDate");
        }

        public void Update(Boletim boletim)
        {
            OrderDate = boletim.OrderDate;
            IdAluno += boletim.IdAluno;
        }
    }
}
