using Dapper;
using EscolaWebSite.DTO;
using EscolaWebSite.Helpers;
using EscolaWebSite.Services.Interfaces;
using System.Collections.Generic;

namespace EscolaWebSite.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly DatabaseHelper _dbHelper;

        public RelatorioService()
        {
            _dbHelper = new DatabaseHelper();
        }

        public IEnumerable<RelatorioDaTurmaDTO> GetAlunosPorTurma()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = @"
                    SELECT 
                        t.Nome AS Turma,
                        COUNT(m.Id) AS TotalAlunos,
                        t.VagasDisponiveis AS VagasRestantes
                    FROM Turma t
                    LEFT JOIN Matricula m ON t.Id = m.TurmaId
                    GROUP BY t.Id, t.Nome, t.VagasDisponiveis
                    ORDER BY t.Nome";

                return connection.Query<RelatorioDaTurmaDTO>(query);
            }
        }
    }
}