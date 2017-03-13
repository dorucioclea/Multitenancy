using System;
using MultiTenancyExperiment.Dal.Multitenancy;
using MultiTenancyExperiment.Dal.Multitenancy.Infrastructure;

namespace MultiTenancyExperiment.Dal.Base
{
    [Tenant("Tenant")]
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Id = SequentialGuid.NewSequentialGuid();
        }

        public Guid Id { get; private set; }

        public string Tenant { get; private set; }
    }
}