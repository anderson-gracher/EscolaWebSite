using System;
using System.Net;
using System.Web.Http;
using EscolaWebSite.Cache.Interfaces;
using EscolaWebSite.DTO;
using EscolaWebSite.Services.Interfaces;

namespace EscolaWebSite.Controllers
{
    [RoutePrefix("api/matriculas")]
    public class MatriculasController : ApiController
    {
        private readonly IMatriculaService _matriculaService;
        private readonly ICacheService _cacheService;

        public MatriculasController(IMatriculaService matriculaService, ICacheService cacheService)
        {
            _matriculaService = matriculaService;
            _cacheService = cacheService;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult RealizarMatricula([FromBody] MatriculaDTO matriculaDTO)
        {
            try
            {
                if (matriculaDTO == null)
                    return BadRequest("Dados inválidos");

                if (matriculaDTO.AlunoId <= 0)
                    return BadRequest("ID do aluno inválido");

                if (matriculaDTO.TurmaId <= 0)
                    return BadRequest("ID da turma inválido");

                var result = _matriculaService.RealizarMatricula(matriculaDTO);

                // Invalida o cache das turmas
                _cacheService.Remove("turmas_list");

                return Created($"api/matriculas/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.Conflict, new ErrorResponseDTO
                {
                    Error = "Conflito",
                    Message = ex.Message,
                    StatusCode = (int)HttpStatusCode.Conflict
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}