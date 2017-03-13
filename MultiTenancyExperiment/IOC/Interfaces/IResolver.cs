
namespace MultiTenancyExperiment.IOC.Interfaces
{
    public interface IResolver
    {
        T Resolve<T>();
        T ResolveKeyed<T>(string key);
    }
}
