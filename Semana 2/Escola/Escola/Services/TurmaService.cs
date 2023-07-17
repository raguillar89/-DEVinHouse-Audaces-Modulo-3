using Escola.Exceptions;
using Escola.Models;
using Escola.Repository.Interface;
using Escola.Services.Interface;

namespace Escola.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public Turma Criar(Turma turma)
        {
            var turmaExist = _turmaRepository.Equals(turma);
            if (turmaExist)
            {
                throw new RegistroDuplicadoException("Turma já cadastrado");
            }
            _turmaRepository.Inserir(turma);
            return turma;
        }

        public Turma ObterPorId(int id)
        {
            Turma turma = _turmaRepository.ObterPorId(id);
            if (turma == null)
            {
                throw new NotFoundException("Turma não encontrado");
            }
            return turma;
        }

        public List<Turma> ObterTurmas() => _turmaRepository.ObterTodos();

        public Turma Atualizar(Turma turma)
        {
            var turmaDB = _turmaRepository.Atualizar(turma);
            if (turmaDB == null) throw new NotFoundException("Turma não encontrado");
            turmaDB.Update(turma);
            return turmaDB;
        }

        public void DeletarTurma(int id)
        {
            var turmaDelete = _turmaRepository.ObterPorId(id);
            if (turmaDelete == null)
            {
                throw new NotFoundException("Turma não encontrado");
            }
            _turmaRepository.Excluir(turmaDelete);
        }
    }
}
