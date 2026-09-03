using EscolaWebSite.Models;
using System.Collections.Generic;

namespace EscolaWebSite.Repositories.Interfaces
{
    public interface IAlunoRepository
    {
        IEnumerable<Aluno> GetAll(int page, int pageSize, string nomeFilter, out int total);
        Aluno GetById(int id);
        int Insert(Aluno aluno);
        bool Update(Aluno aluno);
        bool Delete(int id); // Exclusão lógica
        bool Exists(int id);
        bool IsActive(int id);
    }
}