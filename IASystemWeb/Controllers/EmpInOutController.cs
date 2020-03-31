using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IASystemWeb.Controllers
{
    public class EmpInOutController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        // GET: EmpInOut
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LoadInOutAttendance(int? empID, DateTime fromDate, DateTime toDate)
        {
            var data = db.vw_Attendance.Where(x => x.WDate >= fromDate && x.WDate <= toDate);

            if (empID != null)
            {
                data = data.Where(x => x.EmpID == empID);
            }

            data = data.OrderByDescending(x => x.AttendanceID);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}