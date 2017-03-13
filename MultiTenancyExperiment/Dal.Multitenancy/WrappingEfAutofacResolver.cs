using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using Autofac;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    public class WrappingEfAutofacResolver : IDbDependencyResolver
    {
        private readonly IDbDependencyResolver _snapshot;
        private readonly IContainer _autofacContainer;

        public WrappingEfAutofacResolver(IDbDependencyResolver snapshot, IContainer autofacContainer)
        {
            _snapshot = snapshot;
            _autofacContainer = autofacContainer;
        }

        public object GetService(Type type, object key)
        {
            if (_autofacContainer.IsRegistered(type))
            {
                return key != null ? _autofacContainer.ResolveKeyed(key, type) : _autofacContainer.Resolve(type);
            }

            return _snapshot.GetService(type, key);
        }

        public IEnumerable<object> GetServices(Type type, object key)
        {
            var type1 = typeof(Enumerable)
                .GetMethod("Cast", new[] { typeof(IEnumerable) })
                .MakeGenericMethod(type);

            var searchType = type1.ReturnType;

            if (!_autofacContainer.IsRegistered(searchType))
                return _snapshot.GetServices(searchType, key);

            if (key != null)
            {
                return (IEnumerable<object>)_autofacContainer.ResolveKeyed(key, searchType);
            }

            return (IEnumerable<object>)_autofacContainer.Resolve(searchType);
        }
    }
}