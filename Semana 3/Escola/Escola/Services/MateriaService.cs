using Escola.Exceptions;
using Escola.Models;
using Escola.Repository.Interface;
using Escola.Services.Interface;

namespace Escola.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }

        public Materia Criar(Materia materia)
        {
            var materiaExist = _materiaRepository.Equals(materia);
            if (materiaExist)
            {
                throw new RegistroDuplicadoException("Materia já cadastrada");
            }
            _materiaRepository.Inserir(materia);
            return materia;
        }

        public Materia ObterPorId(int id)
        {
            Materia materia = _materiaRepository.ObterPorId(id);
            if (materia == null)
            {
                throw new NotFoundException("Materia não encontrado");
            }
            return materia;
        }

        public Materia ObterPorNome(string nome)
        {
            Materia materia = _materiaRepository.ObterPorNome(nome);
            if (materia == null)
            {
                throw new NotFoundException("Nome não encontrado");
            }
            return materia;
        }

        public List<Materia> ObterMaterias() => _materiaRepository.ObterTodos();

        public Materia Atualizar(Materia materia)
        {
            var materiaDB = _materiaRepository.Atualizar(materia);
            if (materiaDB == null) throw new NotFoundException("Materia não encontrado");
            materiaDB.Update(materia);
            return materiaDB;
        }

        public void DeletarMateria(int id)
        {
            var materiaDelete = _materiaRepository.ObterPorId(id);
            if (materiaDelete == null)
            {
                throw new NotFoundException("Materia não encontrado");
            }
            _materiaRepository.Excluir(materiaDelete);
        }        
    }
}
