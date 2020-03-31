using DAL.DB;
using IASystem.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zkemkeeper;
using IASystemWeb.Models;
using IASystemWeb.Models.UserLog;
using System.Text;

namespace IASystemWeb.Controllers
{
    public class HomeController : Controller
    {
        InvogueEntities db = new InvogueEntities();
        CZKEM objCZKEM;
        string message = string.Empty;
        //string ip = "124.6.229.3";
        string ip = "192.168.1.201";
        int dwMachineNumber = 1;
        int portNo = 4370;


         DataTable dt = new DataTable();        


        public ActionResult Index()
        {
            DownloadFromDevice();
            return View();
        }

        [HttpPost]
        public JsonResult LoadEmployee()
        {                            
            List<Employee> list = db.Employees.ToList();
            var data = list.Select(x => new { x.EmpID, x.EmpName });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadAttendance(int? empID, string status, DateTime fromDate, DateTime toDate)
        {
            DateTime today = DateTime.Now.Date;
            var log = db.Attendances.Where(x => x.WDate >= fromDate && x.WDate <= toDate);
            

            var todayLog = log.Where(x => x.WDate == today).FirstOrDefault();
            if (todayLog == null)
            {
                //DownloadFromDevice();
            }      

            var data = db.vw_Attendance.Where(x => x.WDate >= fromDate && x.WDate <= toDate);

           if (empID != null)
           {
               data = data.Where(x => x.EmpID == empID);
           }
           if (status == "A")
           {
               status = null;
           }

           if (status != null)
           {
               data = data.Where(x => x.LateStatus == status);
           }
            
            data = data.OrderByDescending(x => x.AttendanceID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadDetailsEmp(int empID, DateTime wDate)
        {
            var data = db.vw_AttendanceLog.Where(x => x.WDate == wDate && x.EmpID == empID).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
         [HttpPost]
        public JsonResult DownloadFromDevice()
        {
            // test
            dt.Clear();
            dt.Columns.Add("attendanceDate");
            dt.Columns.Add("dwEnrollNumber");
            dt.Columns.Add("dwVerifyMode");
            dt.Columns.Add("dwInOutMode");

            //message = SaveLog(DumyData());
            message = string.Empty;

            objCZKEM = new CZKEM();
            bool IsRead = false, isConnected = false;

            string dwEnrollNumber = "";
            int dwVerifyMode, dwInOutMode, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond, dwWorkcode = 0;
            int empNumber = 0, totalCount = 0;
            DateTime attendanceDate = new DateTime();
            StringBuilder sb;
            string minute = string.Empty, second = string.Empty;
            try
            {
                
                if (!isConnected)
                {
                    isConnected = objCZKEM.Connect_Net(ip, 4370);
                }

                string message2 = string.Empty;

                if (isConnected)
                {
                    IsRead = objCZKEM.ReadGeneralLogData(dwMachineNumber);
                    if (IsRead == true)
                    {
                        LogCollection logTemplates = new LogCollection();
                        while (objCZKEM.SSR_GetGeneralLogData(dwMachineNumber, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkcode))
                        {
                            if (dwYear > 2020)
                            {
                                empNumber = Convert.ToInt32(dwEnrollNumber);
                                totalCount++;
                                sb = new StringBuilder();
                                sb.Append(empNumber);
                                sb.Append(dwYear);
                                sb.Append(dwMonth);
                                sb.Append(dwDay);
                                sb.Append(dwHour);
                                sb.Append(dwMinute);
                                sb.Append(dwSecond);
                                attendanceDate = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);
                                UserLog log = new UserLog();
                                log.ADate = attendanceDate;
                                log.EmpNumber = empNumber;
                                log.MachineNO = dwMachineNumber;
                                log.VerifyMethod = dwVerifyMode;
                                log.UniqueID = sb.ToString();
                                log.PunchTime = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);
                                logTemplates.Add(log);
                            }
                           
                            //showInOut(attendanceDate, dwEnrollNumber, dwVerifyMode, dwInOutMode);

                            //GetGeneratLog(objCZKEM, dwMachineNumber, dwEnrollNumber);

                            //message2 = dwEnrollNumber + " " + dwVerifyMode + dwInOutMode;
                        }
                        message = SaveLog(logTemplates);

                       
                        DataTable newDt = dt;
                        message = message2;
                    }
                    else
                    {
                        message = "No Log Found....";
                    }
                }
                else
                {
                    message = "Device Not Connected";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

         public void GetGeneratLog(CZKEM objZkeeper, int machineNumber, string enrollNo)
         {
             string name = null;
             string password = null;
             int previlage = 0;
             bool enabled = false;
             byte[] byTmpData = new byte[2000];
             int tempLength = 0;

             int idwFingerIndex = 0;// [ <--- Enter your fingerprint index here ]
             int iFlag = 0;

             objZkeeper.ReadAllTemplate(machineNumber);

             while (objZkeeper.SSR_GetUserInfo(machineNumber, enrollNo, out name, out password, out previlage, out enabled))
             {
                 if (objZkeeper.GetUserTmpEx(machineNumber, enrollNo, idwFingerIndex, out iFlag, out byTmpData[0], out tempLength))
                 {
                     break;
                 }
             }
         }


         private DataTable showInOut(DateTime date, string dwEnrollNumber, int dwVerifyMode, int dwInOutMode)
         {  
             DataRow _ravi = dt.NewRow();
             _ravi["attendanceDate"] = date;
             _ravi["dwEnrollNumber"] = dwEnrollNumber;
             _ravi["dwVerifyMode"] = dwVerifyMode;
             _ravi["dwInOutMode"] = dwInOutMode;
             dt.Rows.Add(_ravi);
             return dt;
         }

        private string SaveLog(IASystemWeb.Models.UserLog.LogCollection logs)
        {
            string message = string.Empty;
            AttendanceEntry db = new AttendanceEntry();
            SqlCommand cmd = new SqlCommand("sp_Attendance");

            SqlParameter errorMessage = new SqlParameter("@rMessage", SqlDbType.VarChar, 200);
            errorMessage.Direction = ParameterDirection.Output;

            SqlParameter logData = new SqlParameter("@iAttendanceDetails", SqlDbType.Structured);
            logData.Direction = ParameterDirection.Input;
            logData.TypeName = "dbo.NewLog";
            logData.Value = logs;
            cmd.Parameters.Add(logData);
            cmd.Parameters.Add(errorMessage);
            message = db.ExecuteSP(cmd);
            return message;
        }
        // This function only used for Stored procedure testing purpose (Speed up testing)
        private LogCollection DumyData()
        {
            LogCollection logTemplates = new LogCollection();
            UserLog log;

            log = new UserLog();
            log.ADate = DateTime.Now;
            log.EmpNumber = 206086;
            log.MachineNO = dwMachineNumber;
            log.VerifyMethod = 102; // out
            log.UniqueID = "201902271001";
            log.PunchTime = new DateTime(2019, 2, 27, 10, 15, 30);
            logTemplates.Add(log);

            log = new UserLog();
            log.ADate = DateTime.Now;
            log.EmpNumber = 206016;
            log.MachineNO = dwMachineNumber;
            log.VerifyMethod = 101; // out
            log.UniqueID = "201902271002";
            log.PunchTime = new DateTime(2019, 2, 27, 10, 15, 30);
            logTemplates.Add(log);

            log = new UserLog();
            log.ADate = DateTime.Now;
            log.EmpNumber = 205097;
            log.MachineNO = dwMachineNumber;
            log.VerifyMethod = 102; // out
            log.UniqueID = "201902271003";
            log.PunchTime = new DateTime(2019, 2, 27, 10, 15, 30);
            logTemplates.Add(log);
            return logTemplates;
        }
    }
}