using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IASystemWeb.Controllers
{
    public class AllEmpInController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        // GET: AllEmpIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadAttendance(int? empID, string status, DateTime fromDate, DateTime toDate)
        {
            var data = db.vw_Attendance.Where(x => x.WDate >= fromDate && x.WDate <= toDate);

           if (empID != null)
           {
               data = data.Where(x => x.EmpID == empID);
           }

           if (status != null)
           {
               data = data.Where(x => x.LateStatus == status);
           }
          

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}