using System.Data.Entity;
using MultiTenancyExperiment.Dal.Base;
using MultiTenancyExperiment.Dal.Entities;

namespace MultiTenancyExperiment.Dal.Interfaces
{
    public interface  IDatabaseContext : IDbContext
    {
        IDbSet<Note> Notes { get; set; } 
    }
}
