using System;

namespace EscolaWebSite.DTO
{
    public class TurmaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Periodo { get; set; }
        public int VagasTotal { get; set; }
        public int VagasDisponiveis { get; set; }
    }

    public class CreateTurmaDTO
    {
        public string Nome { get; set; }
        public string Periodo { get; set; }
        public int VagasTotal { get; set; }
    }
}