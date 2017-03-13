using System.Data.Entity.Infrastructure.Interception;
using Autofac;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    public class TenancyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TenantCommandInterceptor>().As<IDbInterceptor>().InstancePerLifetimeScope();
            builder.RegisterType<TenantCommandTreeInterceptor>()
                .As<IDbInterceptor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TenancyConfiguration>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
