using System;

namespace EscolaWebSite.DTO
{
    public class MatriculaDTO
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }

    public class MatriculaResponseDTO
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }
        public System.DateTime DataMatricula { get; set; }
    }
}