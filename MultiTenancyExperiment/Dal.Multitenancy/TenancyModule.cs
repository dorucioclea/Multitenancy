using System.Data.Entity.Infrastructure.Interception;
using Autofac;
using MultiTenancyExperiment.Dal.Multitenancy.Interfaces;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    public class TenancyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<TenantCommandInterceptor>().As<ITenantCommandInterceptor>().InstancePerLifetimeScope();
            //builder.RegisterType<TenantCommandTreeInterceptor>()
            //    .As<ITenantCommandTreeInterceptor>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<TenantCommandInterceptor>().As<IDbInterceptor>().InstancePerLifetimeScope();
            builder.RegisterType<TenantCommandTreeInterceptor>()
                .As<IDbInterceptor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TenancyConfiguration>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
