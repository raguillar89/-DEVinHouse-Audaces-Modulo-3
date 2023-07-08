using Escola.Exceptions;
using Escola.Models;
using Escola.Repository.Interface;
using Escola.Services.Interface;

namespace Escola.Services
{
    public class BoletimService : IBoletimService
    {
        private readonly IBoletimRepository _boletimRepository;

        public BoletimService(IBoletimRepository boletimRepository)
        {
            _boletimRepository = boletimRepository;
        }

        public Boletim Criar(Boletim boletim)
        {
            var boletimExist = _boletimRepository.Equals(boletim);
            if (boletimExist)
            {
                throw new RegistroDuplicadoException("Boletim já cadastrada");
            }
            _boletimRepository.Inserir(boletim);
            return boletim;
        }

        public Boletim ObterPorId(int id)
        {
            Boletim boletim = _boletimRepository.ObterPorId(id);
            if (boletim == null)
            {
                throw new NotFoundException("Boletim não encontrado");
            }
            return boletim;
        }

        public List<Boletim> ObterPorIdAluno(int idAluno)
        {
            List<Boletim> boletim = _boletimRepository.ObterPorIdAluno(idAluno);
            if (boletim == null)
            {
                throw new NotFoundException("Aluno não encontrado");
            }
            return boletim;
        }

        public List<Boletim> ObterBoletins() => _boletimRepository.ObterTodos();

        public Boletim Atualizar(Boletim boletim)
        {
            var boletimDB = _boletimRepository.Atualizar(boletim);
            if (boletimDB == null) throw new NotFoundException("Boletim não encontrado");
            boletimDB.Update(boletim);
            return boletimDB;
        }

        public void DeletarBoletim(int id)
        {
            var boletimDelete = _boletimRepository.ObterPorId(id);
            if (boletimDelete == null)
            {
                throw new NotFoundException("Boletim não encontrado");
            }
            _boletimRepository.Excluir(boletimDelete);
        }
    }
}
