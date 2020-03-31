using DAL.DB;
using IASystemWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IASystemWeb.Controllers
{
    public class AddCommentsController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        // GET: AddComments
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadAttendance(int? empID, DateTime? toDate)
        {
            var data = db.vw_Attendance.Where(x => x.WDate == toDate);

            if (empID != null)
            {
                data = data.Where(x => x.EmpID == empID);
            }
            data = data.OrderByDescending(x => x.AttendanceID);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveComments(string comments,string status, string purpose, int? empID, DateTime attendanceDate)
        {
            Result result = new Result();
            try
            {
                var data = db.Attendances.Where(x => x.EmpID == empID && x.WDate == attendanceDate).FirstOrDefault();
                data.Comments = comments;
                data.LateStatus = status;
                data.Purpose = purpose;
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result.isSuccess = true;
                result.message = "Successful";
            }
            catch(Exception e)
            {
                result.isSuccess = false;
                result.message = e.Message;
            }            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}