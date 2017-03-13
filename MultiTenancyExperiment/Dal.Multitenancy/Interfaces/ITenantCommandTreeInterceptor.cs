using System.Data.Entity.Infrastructure.Interception;

namespace MultiTenancyExperiment.Dal.Multitenancy.Interfaces
{
    public interface ITenantCommandTreeInterceptor : IDbCommandTreeInterceptor
    {
    }
}