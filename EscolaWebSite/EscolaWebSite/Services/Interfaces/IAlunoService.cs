using System.Collections.Generic;
using EscolaWebSite.DTO;

namespace EscolaWebSite.Services.Interfaces
{
    public interface IAlunoService
    {
        AlunoListResponseDTO GetAll(FiltroAlunoDTO filter);
        AlunoDTO GetById(int id);
        AlunoDTO Create(CreateAlunoDTO aluno);
        AlunoDTO Update(int id, UpdateAlunoDTO aluno);
        bool Delete(int id);
        bool Exists(int id);
        bool IsActive(int id);
    }
}