using System;

namespace EscolaWebSite.DTO
{
    public class RelatorioDaTurmaDTO
    {
        public string Turma { get; set; }
        public int TotalAlunos { get; set; }
        public int VagasRestantes { get; set; }
    }
}