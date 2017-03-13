using System;
using System.Collections.Generic;
using MultiTenancyExperiment.Dal.Base;

namespace MultiTenancyExperiment.Dal.Entities
{
    public class Note : BaseEntity
    {
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public string Author { get; set; }

        public virtual ICollection<Adendum> Adendums { get; set; } 
    }
}
