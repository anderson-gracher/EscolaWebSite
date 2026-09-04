using EscolaWebSite.Models;
using System.Collections.Generic;

namespace EscolaWebSite.Repositories.Interfaces
{
    public interface ITurmaRepository
    {
        IEnumerable<Turma> GetAll();
        Turma GetById(int id);
        bool UpdateVagas(int turmaId, int vagasDisponiveis);
        bool HasAvailableVagas(int turmaId);
        int GetVagasDisponiveis(int turmaId);
        bool Exists(int id);
    }
}