using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace jobSalt.Models.UserData
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(string conn)
            : base(conn)
        {
        }

        public DBSet<UserDataRow> UserDataRows { get; set; }
    }
}