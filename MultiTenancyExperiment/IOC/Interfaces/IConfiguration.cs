
namespace MultiTenancyExperiment.IOC.Interfaces
{
    public interface IConfiguration
    {
        string TenantValue { get; }
        string DatabaseConnection { get; }
    }
}
