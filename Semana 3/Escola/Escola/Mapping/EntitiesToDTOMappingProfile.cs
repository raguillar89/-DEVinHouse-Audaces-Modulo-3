using AutoMapper;
using Escola.DTO;
using Escola.Models;

namespace LabFashion.Mapping
{
    public class EntitiesToDTOMappingProfile : Profile
    {
        public EntitiesToDTOMappingProfile()
        {
            CreateMap<Aluno, AlunoDTO>().ReverseMap();
            CreateMap<Turma, TurmaDTO>().ReverseMap();
            CreateMap<Materia, MateriaDTO>().ReverseMap();
            CreateMap<NotasMateria, NotasMateriaDTO>().ReverseMap();
            CreateMap<Boletim, BoletimDTO>().ReverseMap();
        }
    }
}
