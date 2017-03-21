using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using FluentScheduler;

namespace OnlinerNotifier.Scheduler
{
    public class JobFactory : IJobFactory
    {
        private HttpConfiguration Config { get; set; }

        public JobFactory(HttpConfiguration config)
        {
            Config = config;
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            var scope = Config.DependencyResolver.GetRootLifetimeScope();
            return scope.Resolve<T>();
        }
    }
}