using EscolaWebSite.Models;
using System.Collections.Generic;

namespace EscolaWebSite.Repositories.Interfaces
{
    public interface IMatriculaRepository
    {
        int Insert(Matricula matricula);
        bool Exists(int alunoId, int turmaId);
        bool DeleteByAlunoAndTurma(int alunoId, int turmaId);
        IEnumerable<Matricula> GetByAlunoId(int alunoId);
        IEnumerable<Matricula> GetByTurmaId(int turmaId);
    }
}