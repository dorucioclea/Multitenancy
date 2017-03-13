using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MultiTenancyExperiment.Autofac;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Interfaces;

namespace MultiTenancyExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacConfiguration.RegisterContainer();
            var context = AutofacConfiguration.Container.Resolve<IDatabaseContext>();



            Console.ReadKey();
        }
    }
}
