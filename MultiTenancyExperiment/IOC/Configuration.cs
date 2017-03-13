using System.Configuration;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.IOC
{
    public class Configuration : IConfiguration
    {
        private string _tenantValue;
        private string _databaseConnection;
        private bool _initialized;
        private readonly object _lockObject = new object();

        private void Load()
        {
            if (_initialized) return;
            
            lock (_lockObject)
            {
                if (_initialized) return;
                    
                _initialized = true;
                _databaseConnection = ConfigurationManager.AppSettings["db"];
                _tenantValue = ConfigurationManager.AppSettings["tenant"];
            }
        }

        public string TenantValue
        {
            get
            {
                Load();
                return _tenantValue;
            }
        }

        public string DatabaseConnection
        {
            get
            {
                Load(); 
                return _databaseConnection;
            }
        }
    }
}