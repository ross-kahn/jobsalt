using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Config
{
    public class DBConnectionConfig
    {
        public string InitialCatalog { get; set; }
        public string DataSource { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool IntegratedSecuity { get; set; }
        public string ConfigConnectionStringName { get; set; }

        public DBConnectionConfig()
        {
            InitialCatalog = "";
            DataSource = "";
            UserId = "";
            Password = "";
            IntegratedSecuity = true;
            ConfigConnectionStringName = "";
        }
    }
}