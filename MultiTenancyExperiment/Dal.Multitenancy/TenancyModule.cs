using Autofac;
using MultiTenancyExperiment.Dal.Multitenancy.Interfaces;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    public class TenancyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ITenantCommandInterceptor>().As<TenantCommandInterceptor>().InstancePerLifetimeScope();
            builder.RegisterType<ITenantCommandTreeInterceptor>()
                .As<TenantCommandTreeInterceptor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TenancyConfiguration>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
