using EscolaWebSite.DTO;
using System.Collections.Generic;

namespace EscolaWebSite.Services.Interfaces
{
    public interface ITurmaService
    {
        IEnumerable<TurmaDTO> GetAll();
        TurmaDTO GetById(int id);
        bool Exists(int id);
        bool HasAvailableVagas(int turmaId);
        int GetVagasDisponiveis(int turmaId);
        bool UpdateVagas(int turmaId, int novasVagas);
    }
}