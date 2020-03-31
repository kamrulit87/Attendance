using DAL.DB;
using IASystemWeb.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IASystemWeb.Areas.AttendanceReports.Controllers
{
    public class ReportsController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        CommonFunctions commonFunctions = new CommonFunctions();
        // GET: AttendanceReports/Reports
        public ActionResult Index()
        {
            return View();
        }
        //Daily Employee Attendance Report
        [HttpPost]
        public JsonResult LoadAttendance(Parameters parameters)
        {
            string path = string.Empty;
            string reportDataSetName = string.Empty;
            string fileString = string.Empty;
            string docType = "pdf";

            var data = db.vw_Attendance.Where(x => x.WDate >= parameters.FromDate && x.WDate <= parameters.ToDate);

            if (parameters.EmpID != null)
            {
                data = data.Where(x => x.EmpID == parameters.EmpID);
            }

            if (parameters.Status == "A")
            {
                parameters.Status = null;
            }

            if (parameters.Status != null)
            {
                data = data.Where(x => x.LateStatus == parameters.Status);
            }
            data = data.OrderBy(x => x.WDate);

            path = Path.Combine(Server.MapPath("~/Areas/AttendanceReports/RDLCS"), "DailyAttendance.rdlc");

            reportDataSetName = "DSDailyAttendance";

            List<ReportParameter> reportParameters = new List<ReportParameter>();

            ReportParameter parameterr = new ReportParameter("fromDate");
            parameterr.Values.Add(parameters.FromDate.ToString());
            reportParameters.Add(parameterr);


            ReportParameter parameter = new ReportParameter("toDate");
            parameter.Values.Add(parameters.ToDate.ToString());
            reportParameters.Add(parameter);

            ReportParameter parameteer = new ReportParameter("status");
            parameters.Status = "All";
            parameteer.Values.Add(parameters.Status);
            reportParameters.Add(parameteer);

            fileString = commonFunctions.CallReports(docType,reportParameters, true, path, null, data, reportDataSetName);
            return Json(fileString);
        }

        //Employee Inside Hour Report
        [HttpPost]
        public JsonResult EmpInsideHour(Parameters parameters)
        {
            string path = string.Empty;
            string reportDataSetName = string.Empty;
            string fileString = string.Empty;
            string docType = "pdf";

            //var data = db.vw_InsideHour.Where(x => x.WDate == parameters.ToDate).ToList();
            var data = db.get_AllEmpInsideHour(parameters.ToDate).ToList();
            var InsideHr = data.OrderByDescending(x => x.Inside);


            path = Path.Combine(Server.MapPath("~/Areas/AttendanceReports/RDLCS"), "InsideHourReport.rdlc");

            reportDataSetName = "InsideHourDataset";

            List<ReportParameter> reportParameters = new List<ReportParameter>();
            ReportParameter parameter = new ReportParameter("toDate");
            parameter.Values.Add(parameters.ToDate.ToString());
            reportParameters.Add(parameter);

            fileString = commonFunctions.CallReports(docType, reportParameters, true, path, null, InsideHr, reportDataSetName);
            return Json(fileString);
        }

        //All Employee Outside hour
        [HttpPost]
        public JsonResult EmpOutsideHour(Parameters parameters)
        {
            string path = string.Empty;
            string reportDataSetName = string.Empty;
            string fileString = string.Empty;
            string docType = "pdf";

            //var data = db.vw_InsideHour.Where(x => x.WDate == parameters.ToDate).ToList();
            var data = db.get_AllEmpOutsideHour(parameters.ToDate).ToList();
            var OutsideHr = data.OrderByDescending(x => x.Outside);


            path = Path.Combine(Server.MapPath("~/Areas/AttendanceReports/RDLCS"), "OutsideHourReport.rdlc");

            reportDataSetName = "OutsideHourDataset";

            List<ReportParameter> reportParameters = new List<ReportParameter>();
            ReportParameter parameter = new ReportParameter("toDate");
            parameter.Values.Add(parameters.ToDate.ToString());
            reportParameters.Add(parameter);

            fileString = commonFunctions.CallReports(docType, reportParameters, true, path, null, OutsideHr, reportDataSetName);
            return Json(fileString);
        }
        
        [HttpGet]
        public ActionResult MonthlyLateCount()
        {
            return View();
        }
     
        [HttpPost]
        public JsonResult LoadMonthlyLateCount(Parameters parameters)
        {
            string path = string.Empty;
            string reportDataSetName = string.Empty;
            string fileString = string.Empty;
            string docType = "pdf";

            var data = db.sp_MonthlyLate(parameters.MonthID).ToList();
            var InsideHr = data.OrderByDescending(x => x.DayDeduct);

            path = Path.Combine(Server.MapPath("~/Areas/AttendanceReports/RDLCS"), "EmpMonthlyLate.rdlc");

            reportDataSetName = "MonthlyLateDataset";

            List<ReportParameter> reportParameters = new List<ReportParameter>();

            

            //ReportParameter parameter = new ReportParameter("monthID");
            //parameter.Values.Add(parameters.MonthID.ToString());
            //reportParameters.Add(parameter);


            ReportParameter parameterr = new ReportParameter("monthName");
            parameterr.Values.Add(parameters.MonthName.ToString());
            reportParameters.Add(parameterr);


            fileString = commonFunctions.CallReports(docType, reportParameters, true, path, null, InsideHr, reportDataSetName);
            return Json(fileString);
        }

    }
}