using EscolaWebSite.DTO;
using EscolaWebSite.Repositories.Interfaces;
using EscolaWebSite.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EscolaWebSite.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public IEnumerable<TurmaDTO> GetAll()
        {
            var turmas = _turmaRepository.GetAll();
            return turmas.Select(t => new TurmaDTO
            {
                Id = t.Id,
                Nome = t.Nome,
                Periodo = t.Periodo,
                VagasTotal = t.VagasTotal,
                VagasDisponiveis = t.VagasDisponiveis
            });
        }

        public TurmaDTO GetById(int id)
        {
            var turma = _turmaRepository.GetById(id);
            if (turma == null)
                return null;

            return new TurmaDTO
            {
                Id = turma.Id,
                Nome = turma.Nome,
                Periodo = turma.Periodo,
                VagasTotal = turma.VagasTotal,
                VagasDisponiveis = turma.VagasDisponiveis
            };
        }

        public bool Exists(int id)
        {
            return _turmaRepository.Exists(id);
        }

        public bool HasAvailableVagas(int turmaId)
        {
            return _turmaRepository.HasAvailableVagas(turmaId);
        }

        public int GetVagasDisponiveis(int turmaId)
        {
            return _turmaRepository.GetVagasDisponiveis(turmaId);
        }

        public bool UpdateVagas(int turmaId, int novasVagas)
        {
            return _turmaRepository.UpdateVagas(turmaId, novasVagas);
        }
    }
}