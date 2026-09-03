using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using EscolaWebSite.Helpers;
using EscolaWebSite.Models;
using EscolaWebSite.Repositories.Interfaces;

namespace EscolaWebSite.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public AlunoRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public IEnumerable<Aluno> GetAll(int page, int pageSize, string nomeFilter, out int total)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                // Query para total de registros
                var countQuery = @"
                    SELECT COUNT(*) 
                    FROM Aluno 
                    WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')";

                total = connection.ExecuteScalar<int>(countQuery, new { Nome = nomeFilter });

                // Query para dados paginados
                var query = @"
                    SELECT * 
                    FROM Aluno 
                    WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')
                    ORDER BY Id
                    OFFSET @Offset ROWS 
                    FETCH NEXT @PageSize ROWS ONLY";

                var offset = (page - 1) * pageSize;

                return connection.Query<Aluno>(query, new
                {
                    Nome = nomeFilter,
                    Offset = offset,
                    PageSize = pageSize
                });
            }
        }

        public Aluno GetById(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT * FROM Aluno WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Aluno>(query, new { Id = id });
            }
        }

        public int Insert(Aluno aluno)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = @"
                    INSERT INTO Aluno (Nome, Email, DataNascimento, Ativo, DataCadastro)
                    VALUES (@Nome, @Email, @DataNascimento, @Ativo, @DataCadastro);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";

                return connection.QuerySingle<int>(query, aluno);
            }
        }

        public bool Update(Aluno aluno)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = @"
                    UPDATE Aluno 
                    SET Nome = @Nome, 
                        Email = @Email, 
                        DataNascimento = @DataNascimento,
                        Ativo = @Ativo
                    WHERE Id = @Id";

                var affectedRows = connection.Execute(query, aluno);
                return affectedRows > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "UPDATE Aluno SET Ativo = 0 WHERE Id = @Id";
                var affectedRows = connection.Execute(query, new { Id = id });
                return affectedRows > 0;
            }
        }

        public bool Exists(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT COUNT(*) FROM Aluno WHERE Id = @Id";
                var count = connection.ExecuteScalar<int>(query, new { Id = id });
                return count > 0;
            }
        }

        public bool IsActive(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var query = "SELECT Ativo FROM Aluno WHERE Id = @Id";
                return connection.ExecuteScalar<bool>(query, new { Id = id });
            }
        }
    }
}