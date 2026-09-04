using EscolaWebSite.DTO;
using System.Collections.Generic;

namespace EscolaWebSite.Services.Interfaces
{
    public interface IRelatorioService
    {
        IEnumerable<RelatorioDaTurmaDTO> GetAlunosPorTurma();
    }
}
