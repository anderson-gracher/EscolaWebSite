using System;
using System.Collections.Generic;
using System.Linq;
using EscolaWebSite.DTO;
using EscolaWebSite.Models;
using EscolaWebSite.Services.Interfaces;
using EscolaWebSite.Repositories.Interfaces;

namespace EscolaWebSite.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public AlunoListResponseDTO GetAll(FiltroAlunoDTO filter)
        {
            var alunos = _alunoRepository.GetAll(
                filter.Pagina,
                filter.Tamanho,
                filter.Nome,
                out int total);

            return new AlunoListResponseDTO
            {
                Total = total,
                Pagina = filter.Pagina,
                Tamanho = filter.Tamanho,
                Alunos = alunos.Select(a => new AlunoDTO
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    DataNascimento = a.DataNascimento,
                    Ativo = a.Ativo,
                    DataCadastro = a.DataCadastro
                }).ToArray()
            };
        }

        public AlunoDTO GetById(int id)
        {
            var aluno = _alunoRepository.GetById(id);
            if (aluno == null)
                return null;

            return new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                DataNascimento = aluno.DataNascimento,
                Ativo = aluno.Ativo,
                DataCadastro = aluno.DataCadastro
            };
        }

        public AlunoDTO Create(CreateAlunoDTO alunoDTO)
        {
            var aluno = new Aluno
            {
                Nome = alunoDTO.Nome,
                Email = alunoDTO.Email,
                DataNascimento = alunoDTO.DataNascimento,                
                
            };

            var id = _alunoRepository.Insert(aluno);
            return GetById(id);
        }

        public AlunoDTO Update(int id, UpdateAlunoDTO alunoDTO)
        {
            var aluno = _alunoRepository.GetById(id);
            if (aluno == null)
                return null;

            aluno.Nome = alunoDTO.Nome;
            aluno.Email = alunoDTO.Email;
            aluno.DataNascimento = alunoDTO.DataNascimento;
            aluno.Ativo = alunoDTO.Ativo;

            var success = _alunoRepository.Update(aluno);
            if (!success)
                return null;

            return GetById(id);
        }

        public bool Delete(int id)
        {
            if (!_alunoRepository.Exists(id))
                return false;

            return _alunoRepository.Delete(id);
        }

        public bool Exists(int id)
        {
            return _alunoRepository.Exists(id);
        }

        public bool IsActive(int id)
        {
            return _alunoRepository.IsActive(id);
        }
    }
}