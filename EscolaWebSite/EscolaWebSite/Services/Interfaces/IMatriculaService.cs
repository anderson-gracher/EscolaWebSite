using EscolaWebSite.DTO;
using System.Collections.Generic;

namespace EscolaWebSite.Services.Interfaces
{
    public interface IMatriculaService
    {
        MatriculaResponseDTO RealizarMatricula(MatriculaDTO matricula);
        bool AlunoEstaMatriculadoNaTurma(int alunoId, int turmaId);
    }
}