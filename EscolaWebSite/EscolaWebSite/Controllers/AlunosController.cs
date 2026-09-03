using EscolaWebSite.DTO;
using EscolaWebSite.Services.Interfaces;
using System;
using System.Net;
using System.Web.Http;

namespace EscolaWebSite.Controllers
{
    [RoutePrefix("api/alunos")]
    public class AlunosController : ApiController
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll([FromUri] FiltroAlunoDTO filter)
        {
            try
            {
                if (filter == null)
                    filter = new FiltroAlunoDTO();

                if (filter.Pagina < 1)
                    filter.Pagina = 1;

                if (filter.Tamanho < 1 || filter.Tamanho > 100)
                    filter.Tamanho = 10;

                var result = _alunoService.GetAll(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido");

                var aluno = _alunoService.GetById(id);
                if (aluno == null)
                    return NotFound();

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] CreateAlunoDTO alunoDTO)
        {
            try
            {
                if (alunoDTO == null)
                    return BadRequest("Dados inválidos!");

                if (string.IsNullOrWhiteSpace(alunoDTO.Nome))
                    return BadRequest("O nome do aluno é obrigatório");

                if (string.IsNullOrWhiteSpace(alunoDTO.Email))
                    return BadRequest("O email do aluno é obrigatório");

                var result = _alunoService.Create(alunoDTO);
                return Created($"api/alunos/{result.Id}", result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] UpdateAlunoDTO alunoDTO)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido");

                if (alunoDTO == null)
                    return BadRequest("Dados inválidos");

                if (string.IsNullOrWhiteSpace(alunoDTO.Nome))
                    return BadRequest("Nome é obrigatório");

                if (string.IsNullOrWhiteSpace(alunoDTO.Email))
                    return BadRequest("Email é obrigatório");

                var aluno = _alunoService.Update(id, alunoDTO);
                if (aluno == null)
                    return NotFound();

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido");

                if (!_alunoService.Exists(id))
                    return NotFound();

                var success = _alunoService.Delete(id);
                if (!success)
                    return InternalServerError();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}