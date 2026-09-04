using EscolaWebSite.Cache.Interfaces;
using EscolaWebSite.DTO;
using EscolaWebSite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EscolaWebSite.Controllers
{
    [RoutePrefix("api/turmas")]
    public class TurmasController : ApiController
    {
        private readonly ITurmaService _turmaService;
        
        private readonly ICacheService _cacheService;

        public TurmasController(ITurmaService turmaService, ICacheService  cacheService)
        {
            _turmaService = turmaService;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {
                const string cacheKey = "turmas_list";

                // Tenta buscar do cache
                
                var cachedTurmas = _cacheService.Get<IEnumerable<TurmaDTO>>(cacheKey);
                if (cachedTurmas != null)
                    return Ok(cachedTurmas);

                // Busca do banco
                var turmas = _turmaService.GetAll();

                // Armazena em cache por 5 minutos                
                _cacheService.Set(cacheKey, turmas, TimeSpan.FromMinutes(5));

                return Ok(turmas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}