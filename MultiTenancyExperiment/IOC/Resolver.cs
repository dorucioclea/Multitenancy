using System;
using Autofac;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.IOC
{
    public class Resolver : IResolver
    {
        private readonly ILifetimeScope _containerScope;

        public Resolver(ILifetimeScope containerScope)
        {
            _containerScope = containerScope;
        }


        public T Resolve<T>()
        {
            return _containerScope.Resolve<T>();
        }

        public T ResolveKeyed<T>(string key)
        {
            return _containerScope.ResolveKeyed<T>(key);
        }
    }
}
