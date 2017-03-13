using Autofac;
using MultiTenancyExperiment.Dal.Multitenancy.Interfaces;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    public class TenancyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TenantCommandInterceptor>().As<ITenantCommandInterceptor>().InstancePerLifetimeScope();
            builder.RegisterType<TenantCommandTreeInterceptor>()
                .As<ITenantCommandTreeInterceptor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TenancyConfiguration>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
