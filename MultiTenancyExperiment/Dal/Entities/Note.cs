using System;
using MultiTenancyExperiment.Dal.Base;

namespace MultiTenancyExperiment.Dal.Entities
{
    public class Note : IBaseEntity
    {
        public Guid Id { get; set; }

        public string Tenant { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public string Author { get; set; }
    }
}
