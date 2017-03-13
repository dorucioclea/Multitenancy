using System;

namespace MultiTenancyExperiment.Dal.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        string Tenant { get;}
    }
}
