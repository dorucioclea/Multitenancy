using System;
using MultiTenancyExperiment.Dal.Multitenancy;

namespace MultiTenancyExperiment.Dal.Base
{
    [Tenant("Tenant")]
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        public string Tenant { get; set; }
    }
}