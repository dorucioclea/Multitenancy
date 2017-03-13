using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using MultiTenancyExperiment.Dal.Multitenancy.Interfaces;
using MultiTenancyExperiment.IOC.Interfaces;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    /// <summary>
    /// Custom implementation of <see cref="IDbCommandTreeInterceptor"/> which filters based on tenantId.
    /// </summary>
    public class TenantCommandTreeInterceptor : ITenantCommandTreeInterceptor
    {
        private readonly IConfiguration _configuration;

        public TenantCommandTreeInterceptor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                var tenant = _configuration.TenantValue;
                // In case of query command change the query by adding a filtering based on tenantId 
                var queryCommand = interceptionContext.Result as DbQueryCommandTree;
                if (queryCommand != null)
                {
                    var newQuery = queryCommand.Query.Accept(new TenantQueryVisitor());
                    interceptionContext.Result = new DbQueryCommandTree(
                        queryCommand.MetadataWorkspace,
                        queryCommand.DataSpace,
                        newQuery);
                    return;
                }

                if (InterceptInsertCommand(interceptionContext, tenant))
                {
                    return;
                }

                if (InterceptUpdate(interceptionContext, tenant))
                {
                    return;
                }

                InterceptDeleteCommand(interceptionContext, tenant);
            }
        }

        /// <summary>
        /// In case of an insert command we always assign the correct value to the tenantId
        /// </summary>
        private static bool InterceptInsertCommand(DbCommandTreeInterceptionContext interceptionContext, string tenantValue)
        {
            var insertCommand = interceptionContext.Result as DbInsertCommandTree;
            if (insertCommand == null) return false;
            
            var column = TenantAttribute.GetTenantColumnName(insertCommand.Target.VariableType.EdmType);
            if (string.IsNullOrEmpty(column)) return false;

            // Create the variable reference in order to create the property
            var variableReference = insertCommand.Target.VariableType.Variable(insertCommand.Target.VariableName);
            // Create the property to which will assign the correct value
            var tenantProperty = variableReference.Property(column);
            // Create the set clause, object representation of sql insert command
            var tenantSetClause =
                DbExpressionBuilder.SetClause(tenantProperty, DbExpression.FromString(tenantValue));

            // Remove potential assignment of tenantId for extra safety 
            var filteredSetClauses =
                insertCommand.SetClauses.Cast<DbSetClause>()
                    .Where(sc => ((DbPropertyExpression)sc.Property).Property.Name != column);

            // Construct the final clauses, object representation of sql insert command values
            var finalSetClauses =
                new ReadOnlyCollection<DbModificationClause>(new List<DbModificationClause>(filteredSetClauses)
                {
                    tenantSetClause
                });

            // Construct the new command
            var newInsertCommand = new DbInsertCommandTree(
                insertCommand.MetadataWorkspace,
                insertCommand.DataSpace,
                insertCommand.Target,
                finalSetClauses,
                insertCommand.Returning);

            interceptionContext.Result = newInsertCommand;
            // True means an interception successfully happened so there is no need to continue
            return true;
        }

        /// <summary>
        /// In case of an update command we always filter based on the tenantId
        /// </summary>
        private static bool InterceptUpdate(DbCommandTreeInterceptionContext interceptionContext, string tenantValue)
        {
            var updateCommand = interceptionContext.Result as DbUpdateCommandTree;
            if (updateCommand == null) return false;

            var column = TenantAttribute.GetTenantColumnName(updateCommand.Target.VariableType.EdmType);
            if (string.IsNullOrEmpty(column)) return false;

            // Create the variable reference in order to create the property
            var variableReference = updateCommand.Target.VariableType.Variable(updateCommand.Target.VariableName);
            // Create the property to which will assign the correct value
            var tenantProperty = variableReference.Property(column);
            // Create the tenantId where predicate, object representation of sql where tenantId = value statement
            var tenantIdWherePredicate = tenantProperty.Equal(DbExpression.FromString(tenantValue));

            // Remove potential assignment of tenantId for extra safety
            var filteredSetClauses =
                updateCommand.SetClauses.Cast<DbSetClause>()
                    .Where(sc => ((DbPropertyExpression)sc.Property).Property.Name != column);

            // Construct the final clauses, object representation of sql insert command values
            var finalSetClauses =
                new ReadOnlyCollection<DbModificationClause>(new List<DbModificationClause>(filteredSetClauses));

            // The initial predicate is the sql where statement
            var initialPredicate = updateCommand.Predicate;
            // Add to the initial statement the tenantId statement which translates in sql AND TenantId = 'value'
            var finalPredicate = initialPredicate.And(tenantIdWherePredicate);

            var newUpdateCommand = new DbUpdateCommandTree(
                updateCommand.MetadataWorkspace,
                updateCommand.DataSpace,
                updateCommand.Target,
                finalPredicate,
                finalSetClauses,
                updateCommand.Returning);

            interceptionContext.Result = newUpdateCommand;
            // True means an interception successfully happened so there is no need to continue
            return true;
        }

        /// <summary>
        /// In case of a delete command we always filter based on the tenantId
        /// </summary>
        private static void InterceptDeleteCommand(DbCommandTreeInterceptionContext interceptionContext, string tenantValue)
        {
            var deleteCommand = interceptionContext.Result as DbDeleteCommandTree;
            if (deleteCommand == null) return;
            
            var column = TenantAttribute.GetTenantColumnName(deleteCommand.Target.VariableType.EdmType);
            if (string.IsNullOrEmpty(column)) return;
                
            // Create the variable reference in order to create the property
            var variableReference = deleteCommand.Target.VariableType.Variable(deleteCommand.Target.VariableName);
            // Create the property to which will assign the correct value
            var tenantProperty = variableReference.Property(column);
            var tenantIdWherePredicate = tenantProperty.Equal(DbExpression.FromString(tenantValue));

            // The initial predicate is the sql where statement
            var initialPredicate = deleteCommand.Predicate;
            // Add to the initial statement the tenantId statement which translates in sql AND TenantId = 'value'
            var finalPredicate = initialPredicate.And(tenantIdWherePredicate);

            var newDeleteCommand = new DbDeleteCommandTree(
                deleteCommand.MetadataWorkspace,
                deleteCommand.DataSpace,
                deleteCommand.Target,
                finalPredicate);

            interceptionContext.Result = newDeleteCommand;
        }

    }
}