using System;

namespace EscolaWebSite.DTO
{
    public enum StatusAluno
    {
        inativo = 0,
        ativo = 1,
    }

    public class AlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public StatusAluno Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class CreateAlunoDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class UpdateAlunoDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public StatusAluno Ativo { get; set; }
    }

    public class FiltroAlunoDTO
    {
        public string Nome { get; set; }
        public int Pagina { get; set; } = 1;
        public int Tamanho { get; set; } = 10;
    }

    public class AlunoListResponseDTO
    {
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int Tamanho { get; set; }
        public AlunoDTO[] Alunos { get; set; }
    }
}