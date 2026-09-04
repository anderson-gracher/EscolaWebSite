using System.Linq;
using System.Collections.Generic;
using Dapper;
using EscolaWebSite.Helpers;
using EscolaWebSite.Models;
using EscolaWebSite.Repositories.Interfaces;

namespace EscolaWebSite.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public MatriculaRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public int Insert(Matricula matricula)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = @"
                    INSERT INTO Matricula (AlunoId, TurmaId, DataMatricula)
                    VALUES (@AlunoId, @TurmaId, @DataMatricula);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";

                return connection.QuerySingle<int>(query, matricula);
            }
        }

        public bool Exists(int alunoId, int turmaId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = @"
                    SELECT COUNT(*) 
                    FROM Matricula 
                    WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";

                var count = connection.ExecuteScalar<int>(query, new { AlunoId = alunoId, TurmaId = turmaId });
                return count > 0;
            }
        }

        public bool DeleteByAlunoAndTurma(int alunoId, int turmaId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "DELETE FROM Matricula WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
                var affectedRows = connection.Execute(query, new { AlunoId = alunoId, TurmaId = turmaId });
                return affectedRows > 0;
            }
        }

        public IEnumerable<Matricula> GetByAlunoId(int alunoId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT * FROM Matricula WHERE AlunoId = @AlunoId";
                return connection.Query<Matricula>(query, new { AlunoId = alunoId });
            }
        }

        public IEnumerable<Matricula> GetByTurmaId(int turmaId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT * FROM Matricula WHERE TurmaId = @TurmaId";
                return connection.Query<Matricula>(query, new { TurmaId = turmaId });
            }
        }
    }
}