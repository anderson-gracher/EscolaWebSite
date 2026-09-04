using System.Collections.Generic;
using System.Linq;
using Dapper;
using EscolaWebSite.Helpers;
using EscolaWebSite.Models;
using EscolaWebSite.Repositories.Interfaces;

namespace EscolaWebSite.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public TurmaRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public IEnumerable<Turma> GetAll()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT * FROM Turma ORDER BY Nome";
                return connection.Query<Turma>(query);
            }
        }

        public Turma GetById(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT * FROM Turma WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Turma>(query, new { Id = id });
            }
        }

        public bool UpdateVagas(int turmaId, int vagasDisponiveis)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "UPDATE Turma SET VagasDisponiveis = @Vagas WHERE Id = @Id";
                var affectedRows = connection.Execute(query, new { Vagas = vagasDisponiveis, Id = turmaId });
                return affectedRows > 0;
            }
        }

        public bool HasAvailableVagas(int turmaId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT VagasDisponiveis FROM Turma WHERE Id = @Id";
                var vagas = connection.ExecuteScalar<int>(query, new { Id = turmaId });
                return vagas > 0;
            }
        }

        public int GetVagasDisponiveis(int turmaId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT VagasDisponiveis FROM Turma WHERE Id = @Id";
                return connection.ExecuteScalar<int>(query, new { Id = turmaId });
            }
        }

        public bool Exists(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT COUNT(*) FROM Turma WHERE Id = @Id";
                var count = connection.ExecuteScalar<int>(query, new { Id = id });
                return count > 0;
            }
        }
    }
}