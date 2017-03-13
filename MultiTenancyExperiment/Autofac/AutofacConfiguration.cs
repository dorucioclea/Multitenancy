using Autofac;
using MultiTenancyExperiment.Dal;
using MultiTenancyExperiment.Dal.Multitenancy;
using MultiTenancyExperiment.IOC;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.Autofac
{
    public class AutofacConfiguration
    {
        public static IContainer Container { get; private set; }
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register<IConfiguration>(c => new Configuration()).SingleInstance();

            builder.RegisterModule(new TenancyModule());    
            builder.RegisterModule(new DalModule());    
            // register resolver
            builder.Register<IResolver>(c => new Resolver(Container)).SingleInstance();
            Container = builder.Build();
        }
    }
}
