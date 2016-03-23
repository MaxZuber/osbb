using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCL.Common
{
    public class AppSettings
    {
        private const string ConnectionStringKey = "MainConnection";

        private static AppSettings _instance;
        private readonly string _connectionString;

        public static AppSettings GetInstance()
        {
            return _instance ?? (_instance = new AppSettings());
        }

        private AppSettings()
        {
            if (
                ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>()
                    .Any(connStr => connStr.Name == ConnectionStringKey))
            {
                _connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }
    }
}
