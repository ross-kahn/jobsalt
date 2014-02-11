using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jobSalt.Models.UserData;

namespace jobSalt.Controllers
{
    public class UserDataController : Controller
    {
        private UserDataContext database = new UserDataContext("UserDataContextDb");
        //
        // GET: /UserData/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(UserDataRow row)
        {
            database.UserDataRows.Add(row);
            database.SaveChanges();
            return Content("Data Saved");
        }

        public ActionResult Delete(string username, string module)
        {
            UserDataRow row = new UserDataRow();
            row.UserName = username;
            row.ModuleName = module;

            database.UserDataRows.Remove(row);
            database.SaveChanges();
            return Content("Row Deleted");
        }

        public UserDataRow Read(string username, string module)
        {
            object[] paramList = {username, module};

            UserDataRow row = database.UserDataRows.Find(paramList);
            return row;
        }
    }
}
