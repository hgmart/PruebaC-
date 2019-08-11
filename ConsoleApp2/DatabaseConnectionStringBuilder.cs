using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace NHibernate5
{
    public class DatabaseConnectionStringBuilder
    {
        private const char SEPARATOR = ';';
        private IDictionary<string, string> Properties;

        public DatabaseConnectionStringBuilder()
        {
            Properties = LoadProperties();
        }

        public string Build()
        {
            var stringifiedProperties = Properties.Keys.Select(key => ProcessProperty(key));
            var resultado = string.Join(SEPARATOR, stringifiedProperties.ToArray());
            return resultado;
        }

        private string ProcessProperty(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(Properties[key]);
            return String.IsNullOrEmpty(value) ? string.Empty : $"{key}={value}";
        }
        private IDictionary<string, string> LoadProperties()
        {
            return new Dictionary<string, string>
            {
                { "Data Source", "DB_Server" },
                { "Initial Catalog", "DB_Resource" },
                { "User ID", "DB_LoginUsername" },
                { "Password", "DB_LoginPassword" },
                { "Persist Security Info", "DB_PersistSecurityInfo" }
            };
        }
    }
}
