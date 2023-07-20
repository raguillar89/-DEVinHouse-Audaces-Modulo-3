using Escola.Context;
using Escola.Models;
using Escola.Repository.Interface;

namespace Escola.Repository
{
    public class MateriaRepositoryMock : IMateriaRepository
    {
        public Materia Atualizar(Materia model)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Materia model)
        {
            throw new NotImplementedException();
        }

        public Materia Inserir(Materia materia)
        {
            materia = new Materia()
            {
                Id = 1,
                Nome = "Matemática"
            };
            return materia;
        }

        public Materia ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Materia ObterPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public List<Materia> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
