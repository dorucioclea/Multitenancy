using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MultiTenancyExperiment.Dal.Interfaces;

namespace MultiTenancyExperiment.Dal
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IDatabaseContext>().As<DatabaseContext>().InstancePerLifetimeScope();
        }
    }
}
