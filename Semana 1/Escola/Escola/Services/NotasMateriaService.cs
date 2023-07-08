using Escola.Exceptions;
using Escola.Models;
using Escola.Repository.Interface;
using Escola.Services.Interface;

namespace Escola.Services
{
    public class NotasMateriaService : INotasMateriaService
    {
        private readonly INotasMateriaRepository _notasMateriaRepository;

        public NotasMateriaService(INotasMateriaRepository notasMateriaRepository)
        {
            _notasMateriaRepository = notasMateriaRepository;
        }

        public NotasMateria Criar(NotasMateria notasMateria)
        {
            var materiaExist = _notasMateriaRepository.Equals(notasMateria);
            if (materiaExist)
            {
                throw new RegistroDuplicadoException("Notas Materia já cadastrada");
            }
            _notasMateriaRepository.Inserir(notasMateria);
            return notasMateria;
        }

        public NotasMateria ObterPorId(int id)
        {
            NotasMateria notasMateria = _notasMateriaRepository.ObterPorId(id);
            if (notasMateria == null)
            {
                throw new NotFoundException("Notas Materia não encontrado");
            }
            return notasMateria;
        }

        public NotasMateria ObterPorBoletim(int idBoletim)
        {
            NotasMateria notasMateria = _notasMateriaRepository.ObterPorBoletim(idBoletim);
            if (notasMateria == null)
            {
                throw new NotFoundException("Id não encontrado");
            }
            return notasMateria;
        }

        public List<NotasMateria> ObterNotasMateria() => _notasMateriaRepository.ObterTodos();

        public NotasMateria Atualizar(NotasMateria notasMateria)
        {
            var notasMateriaDB = _notasMateriaRepository.Atualizar(notasMateria);
            if (notasMateriaDB == null) throw new NotFoundException("Notas Materia não encontrado");
            notasMateriaDB.Update(notasMateria);
            return notasMateriaDB;
        }

        public void DeletarNotasMateria(int id)
        {
            var notasMateriaDelete = _notasMateriaRepository.ObterPorId(id);
            if (notasMateriaDelete == null)
            {
                throw new NotFoundException("Notas Materia não encontrado");
            }
            _notasMateriaRepository.Excluir(notasMateriaDelete);
        }
    }
}
