using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IASystemWeb.Controllers
{
    public class AllEmpOutsideHourController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        // GET: AllEmpOutsideHour
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AllEmpOutsideHour(DateTime toDate )
        {
            var data = db.get_AllEmpOutsideHour(toDate).ToList();
            var InsideHr = data.OrderByDescending(x => x.Outside);
            return Json(InsideHr, JsonRequestBehavior.AllowGet);

        }
    }
}