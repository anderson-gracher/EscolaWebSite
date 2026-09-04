using System;
using System.Transactions;
using EscolaWebSite.DTO;
using EscolaWebSite.Models;
using EscolaWebSite.Repositories.Interfaces;
using EscolaWebSite.Services.Interfaces;

namespace EscolaWebSite.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IAlunoService _alunoService;
        private readonly ITurmaService _turmaService;

        public MatriculaService(
            IMatriculaRepository matriculaRepository,
            IAlunoService alunoService,
            ITurmaService turmaService)
        {
            _matriculaRepository = matriculaRepository;
            _alunoService = alunoService;
            _turmaService = turmaService;
        }

        public MatriculaResponseDTO RealizarMatricula(MatriculaDTO matriculaDTO)
        {
            // Validações
            if (matriculaDTO.AlunoId <= 0 || matriculaDTO.TurmaId <= 0)
                throw new ArgumentException("IDs inválidos");

            // Verifica se o aluno existe
            if (!_alunoService.Exists(matriculaDTO.AlunoId))
                throw new InvalidOperationException("Aluno não encontrado");

            // Verifica se o aluno está ativo
            if (!_alunoService.IsActive(matriculaDTO.AlunoId))
                throw new InvalidOperationException("Aluno inativo");

            // Verifica se a turma existe
            if (!_turmaService.Exists(matriculaDTO.TurmaId))
                throw new InvalidOperationException("Turma não encontrada");

            // Verifica se a turma tem vaga disponível
            if (!_turmaService.HasAvailableVagas(matriculaDTO.TurmaId))
                throw new InvalidOperationException("Turma sem vagas disponíveis");

            // Verifica se o aluno já está matriculado na turma
            if (AlunoEstaMatriculadoNaTurma(matriculaDTO.AlunoId, matriculaDTO.TurmaId))
                throw new InvalidOperationException("Aluno já matriculado nesta turma");

            // Executa a matrícula em transação
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // 1. Insere a matrícula
                var matricula = new Matricula
                {
                    AlunoId = matriculaDTO.AlunoId,
                    TurmaId = matriculaDTO.TurmaId,
                    DataMatricula = DateTime.Now
                };

                var matriculaId = _matriculaRepository.Insert(matricula);

                // 2. Decrementa as vagas disponíveis
                var vagasAtuais = _turmaService.GetVagasDisponiveis(matriculaDTO.TurmaId);
                var sucessoVagas = _turmaService.UpdateVagas(matriculaDTO.TurmaId, vagasAtuais - 1);

                if (!sucessoVagas)
                    throw new InvalidOperationException("Erro ao atualizar vagas");

                scope.Complete();

                // Retorna os dados da matrícula
                return new MatriculaResponseDTO
                {
                    Id = matriculaId,
                    AlunoId = matriculaDTO.AlunoId,
                    TurmaId = matriculaDTO.TurmaId,
                    DataMatricula = matricula.DataMatricula
                };
            }
        }

        public bool AlunoEstaMatriculadoNaTurma(int alunoId, int turmaId)
        {
            return _matriculaRepository.Exists(alunoId, turmaId);
        }
    }
}