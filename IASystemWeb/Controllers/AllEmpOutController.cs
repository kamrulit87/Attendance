using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IASystemWeb.Controllers
{
    public class AllEmpOutController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        // GET: AllEmpOut
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
            data = data.OrderBy(x => x.OutTime );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}