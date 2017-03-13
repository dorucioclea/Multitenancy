using System;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Multitenancy;

namespace MultiTenancyExperiment.Dal.Entities
{
    [Tenant("Tenant")]
    public class Note : IBaseEntity
    {
        public Guid Id { get; set; }

        public string Tenant { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public string Author { get; set; }
    }
}
