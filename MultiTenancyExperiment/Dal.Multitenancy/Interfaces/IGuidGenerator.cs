using System;

namespace MultiTenancyExperiment.Dal.Multitenancy.Interfaces
{
    public interface IGuidGenerator
    {
        Guid NewId();
    }
}
