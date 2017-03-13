using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace MultiTenancyExperiment.Dal.Multitenancy
{
    /// <summary>
    /// Attribute used to mark all entities which should be filtered based on tenantId
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class TenantAttribute : Attribute
    {
        public string ColumnName { get; private set; }

        public TenantAttribute(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("columnName");
            }

            ColumnName = columnName;
        }

        public static string GetTenantColumnName(EdmType type)
        {
            var annotation =
                type.MetadataProperties.SingleOrDefault(
                    p => p.Name.EndsWith(string.Format("customannotation:{0}", MultitenancyConstants.TenantAnnotation)));

            return annotation == null ? null : (string) annotation.Value;
        }
    }
}
