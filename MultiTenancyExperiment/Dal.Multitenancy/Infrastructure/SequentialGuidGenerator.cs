using System;
using MultiTenancyExperiment.Dal.Multitenancy.Interfaces;

namespace MultiTenancyExperiment.Dal.Multitenancy.Infrastructure
{
    public class SequentialGuidGenerator : IGuidGenerator
    {
        public Guid NewId()
        {
            return SequentialGuid.NewSequentialGuid();
        }
    }
}