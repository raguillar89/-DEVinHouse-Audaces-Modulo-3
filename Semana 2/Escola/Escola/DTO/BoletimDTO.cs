using Escola.Models;

namespace Escola.DTO
{
    public class BoletimDTO
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public int IdAluno { get; set; }

        public BoletimDTO() { }

        public BoletimDTO(Boletim boletim)
        {
            Id = boletim.Id;
            OrderDate = boletim.OrderDate.ToString("dd/MM/yy");
            IdAluno = boletim.IdAluno;
        }
    }
}
