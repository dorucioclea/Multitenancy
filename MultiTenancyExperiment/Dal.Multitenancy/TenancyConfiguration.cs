using System.Data.Entity;
using MultiTenancyExperiment.Dal.Multitenancy.Interfaces;
// ReSharper disable SuggestBaseTypeForParameter

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    public class TenancyConfiguration : DbConfiguration
    {
        public TenancyConfiguration(ITenantCommandInterceptor tenantCommandInterceptor, 
            ITenantCommandTreeInterceptor tenantCommandTreeInterceptor)
        {
            AddInterceptor(tenantCommandInterceptor);
            AddInterceptor(tenantCommandTreeInterceptor);
        }
    }
}
