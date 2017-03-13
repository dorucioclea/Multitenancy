using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    /// <summary>
    /// Visitor pattern implementation class that adds filtering for tenantId column if applicable
    /// </summary>
    public class TenantQueryVisitor : DefaultExpressionVisitor
    {
        /// <summary>
        /// Flag prevents applying the custom filtering twice per query 
        /// </summary>
        private bool _injectedDynamicFilter;

        /// <summary>
        /// This method called before the one below it when a filtering is already exists in the query (e.g. fetch an entity by id)
        /// so we apply the dynamic filtering at this level
        /// </summary>
        public override DbExpression Visit(DbFilterExpression expression)
        {
            var column = TenantAttribute.GetTenantColumnName(expression.Input.Variable.ResultType.EdmType);
            if (_injectedDynamicFilter || string.IsNullOrEmpty(column))
            {
                return base.Visit(expression);
            }

            var newFilterExpression = BuildFilterExpression(expression.Input, expression.Predicate, column);
            return base.Visit(newFilterExpression ?? expression);
        }

        public override DbExpression Visit(DbScanExpression expression)
        {
            var column = TenantAttribute.GetTenantColumnName(expression.Target.ElementType);
            if (_injectedDynamicFilter || string.IsNullOrEmpty(column))
            {
                return base.Visit(expression);
            }
            
            // Get the current expression
            var dbExpression = base.Visit(expression);
            // Get the current expression binding 
            var currentExpressionBinding = dbExpression.Bind();
            var newFilterExpression = BuildFilterExpression(currentExpressionBinding, null, column);
            if (newFilterExpression != null)
            {
                //  If not null, a new DbFilterExpression has been created with our dynamic filters.
                return base.Visit(newFilterExpression);
            }

            return base.Visit(expression);
        }

        /// <summary>
        /// Helper method creating the correct filter expression based on the supplied parameters
        /// </summary>
        private DbFilterExpression BuildFilterExpression(DbExpressionBinding binding, DbExpression predicate, string column)
        {
            _injectedDynamicFilter = true;

            var variableReference = binding.VariableType.Variable(binding.VariableName);
            // Create the property based on the variable in order to apply the equality
            var tenantProperty = variableReference.Property(column);
            // Create the parameter which is an object representation of a sql parameter.
            // We have to create a parameter and not perform a direct comparison with Equal function for example
            // as this logic is cached per query and called only once
            var tenantParameter = tenantProperty.Property.TypeUsage.Parameter(MultitenancyConstants.TenantIdFilterParameterName);
            // Apply the equality between property and parameter.
            DbExpression newPredicate = tenantProperty.Equal(tenantParameter);

            // If an existing predicate exists (normally when called from DbFilterExpression) execute a logical AND to get the result
            if (predicate != null)
                newPredicate = newPredicate.And(predicate);

            return binding.Filter(newPredicate);
        }
    }
}