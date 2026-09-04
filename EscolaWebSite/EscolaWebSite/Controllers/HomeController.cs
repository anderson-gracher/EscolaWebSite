using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EscolaWebSite.Controllers
{
    [RoutePrefix("")]
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            // Redireciona para o index.html
            var response = Request.CreateResponse(HttpStatusCode.Redirect);
            response.Headers.Location = new System.Uri("wwwroot/index.html", System.UriKind.Relative);
            return response;
        }

        // Opção 2: Retornar o conteúdo do index.html diretamente
        [HttpGet]
        [Route("api-home")]
        public IHttpActionResult GetApiInfo()
        {
            return Ok(new
            {
                Message = "EscolaWebSite",
                Version = "1.0",
                Endpoints = new
                {
                    Alunos = "/api/alunos",
                    Turmas = "/api/turmas",
                    Matriculas = "/api/matriculas",
                    Relatorios = "/api/relatorios/alunos-por-turma",
                    Interface = "wwwroot/index.html"
                }
            });
        }
    }
}