using Autofac;
using MultiTenancyExperiment.Dal.Interfaces;

namespace MultiTenancyExperiment.Dal
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>().As<IDatabaseContext>().InstancePerLifetimeScope();
        }
    }
}
