using Unity;
using Unity.WebApi;
using EscolaWebSite.Cache.Interfaces;
using EscolaWebSite.Helpers;
using EscolaWebSite.Repositories;
using EscolaWebSite.Repositories.Interfaces;
using EscolaWebSite.Services;
using EscolaWebSite.Services.Interfaces;


namespace EscolaWebSite.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register repositories
            container.RegisterType<IAlunoRepository, AlunoRepository>();
            container.RegisterType<ITurmaRepository, TurmaRepository>();
            container.RegisterType<IMatriculaRepository, MatriculaRepository>();

            // Register services
            container.RegisterType<IAlunoService, AlunoService>();
            container.RegisterType<ITurmaService, TurmaService>();
            container.RegisterType<IMatriculaService, MatriculaService>();
            container.RegisterType<IRelatorioService, RelatorioService>();

            // Register cache service
            container.RegisterInstance<ICacheService>(CacheHelper.GetCacheService());

            // Set dependency resolver
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}