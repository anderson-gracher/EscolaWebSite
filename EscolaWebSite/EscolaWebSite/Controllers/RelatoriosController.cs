using System;
using System.Web.Http;
using EscolaWebSite.Services.Interfaces;

namespace EscolaWebSite.Controllers
{
    [RoutePrefix("api/relatorios")]
    public class RelatoriosController : ApiController
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet]
        [Route("alunos-por-turma")]
        public IHttpActionResult GetAlunosPorTurma()
        {
            try
            {
                var relatorio = _relatorioService.GetAlunosPorTurma();
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}