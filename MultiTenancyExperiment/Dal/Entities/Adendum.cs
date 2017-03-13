using System;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Multitenancy;

namespace MultiTenancyExperiment.Dal.Entities
{
    [Tenant("Tenant")]
    public class Adendum : IBaseEntity
    {
        public string AdendumContent { get; set; }
        public virtual Note Note { get; set; }
        public Guid NoteId { get; set; }

        public Guid Id { get; set; }

        public string Tenant { get; private set; }
    }
}
