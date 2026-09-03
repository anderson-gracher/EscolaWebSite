using System;

namespace EscolaWebSite.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int AlunoID { get; set; }
        public string TurmaID { get; set; }
        public DateTime DataMatricula { get; set; }

        public virtual Aluno Aluno { get; set; }

        public virtual Turma Turma { get; set; }

    }

}