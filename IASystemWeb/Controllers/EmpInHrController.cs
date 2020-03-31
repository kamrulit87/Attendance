using DAL.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IASystemWeb.Controllers
{
    public class EmpInHrController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        // GET: EmpInHr
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EmpInsideHour(DateTime toDate)
        {
            var data = db.get_AllEmpInsideHour(toDate ).ToList();
            var InsideHr = data.OrderByDescending(x => x.Inside);
            return Json(InsideHr, JsonRequestBehavior.AllowGet);
        }
    }
}