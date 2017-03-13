using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using Autofac;
using MultiTenancyExperiment.Dal;
using MultiTenancyExperiment.Dal.Multitenancy;
using MultiTenancyExperiment.IOC;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.Autofac
{
    public static class AutofacConfiguration
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

            DbConfiguration.Loaded += DbConfiguration_Loaded;
        }

        static void DbConfiguration_Loaded(object sender, DbConfigurationLoadedEventArgs e)
        {
            e.AddDependencyResolver(new EfAutofacResolver(e.DependencyResolver, Container), true);
        }

        class EfAutofacResolver : IDbDependencyResolver
        {
            private readonly IDbDependencyResolver _baseDependencyResolver;
            private readonly IContainer _autofacContainer;

            public EfAutofacResolver(IDbDependencyResolver baseDependencyResolver, IContainer autofacContainer)
            {
                _baseDependencyResolver = baseDependencyResolver;
                _autofacContainer = autofacContainer;
            }

            public object GetService(Type type, object key)
            {
                if (_autofacContainer.IsRegistered(type))
                {
                    return key != null ? _autofacContainer.ResolveKeyed(key, type) : _autofacContainer.Resolve(type);
                }

                return _baseDependencyResolver.GetService(type, key);
            }

            public IEnumerable<object> GetServices(Type type, object key)
            {
                var type1 = typeof(Enumerable)
                    .GetMethod("Cast", new[] { typeof(System.Collections.IEnumerable) })
                    .MakeGenericMethod(type);

                var searchType = type1.ReturnType;

                if (!_autofacContainer.IsRegistered(searchType))
                    return _baseDependencyResolver.GetServices(searchType, key);
                
                if (key != null)
                {
                    return (IEnumerable<object>)_autofacContainer.ResolveKeyed(key, searchType);
                }

                return (IEnumerable<object>)Container.Resolve(searchType); ;
            }
        }
    }
}
