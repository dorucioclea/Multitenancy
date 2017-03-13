using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    /// <summary>
    /// Custom implementation of <see cref="IDbCommandInterceptor"/>.
    /// In this class we set the actual value of the tenantId when querying the database as the command tree is cached  
    /// </summary>
    internal class TenantCommandInterceptor : IDbCommandInterceptor
    {
        private readonly IConfiguration _configuration;

        public TenantCommandInterceptor(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            SetTenantParameterValue(command);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            SetTenantParameterValue(command);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            SetTenantParameterValue(command);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        private void SetTenantParameterValue(DbCommand command)
        {
            if ((command == null) || (command.Parameters.Count == 0))
            {
                return;
            }

            // Enumerate all command parameters and assign the correct value in the one we added inside query visitor
            foreach (var param in command.Parameters.Cast<DbParameter>()
                                                    .Where(param => param.ParameterName == MultitenancyConstants.TenantIdFilterParameterName))
            {
                param.Value = _configuration.TenantValue;
            }
        }
    }
}