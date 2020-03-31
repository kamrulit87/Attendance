using DAL.DB;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace IASystemWeb.Common
{
    public class CommonFunctions
    {
        InvogueEntities db = new InvogueEntities();
        public string CallReports(string DocType, List<ReportParameter> reportParameters, bool isPortrait, string reportPath, DataTable dataTable, IEnumerable<dynamic> dataObject, string reportDataSetName)
        {
            string errorMessage = string.Empty;
            string fileString = string.Empty;
            try
            {
                var org = db.Organaizations.FirstOrDefault();

                ReportParameter companyName = new ReportParameter("companyName");
                companyName.Values.Add(org.Name);
                reportParameters.Add(companyName);

                ReportParameter address = new ReportParameter("address");
                address.Values.Add(org.Address);
                reportParameters.Add(address);

                ReportParameter contactInfo = new ReportParameter("contactInfo");
                contactInfo.Values.Add("PHONE :" + org.Phone + " Email : " + org.Email + " WEB : " + org.Web);
                reportParameters.Add(contactInfo);

                LocalReport lr = new LocalReport();
                ReportDataSource rd = new ReportDataSource();
                string path = string.Empty;
                if (dataTable != null && dataTable.Rows.Count > 0 || dataObject != null && dataObject.Count() > 0)
                {
                    path = reportPath;
                }
                else
                {
                    if (isPortrait)
                    {
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/Areas/AttendanceReports/RDLCS"), "DailyAttendance.rdlc");
                    }
                    else
                    {
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/Areas/AttendanceReports/RDLCS"), "DailyAttendance.rdlc");
                    }
                }

                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                    lr.SetParameters(reportParameters);

                }
                else
                {
                    errorMessage = "File Not Exists.";
                }
                if (dataTable != null)
                {
                    rd = new ReportDataSource(reportDataSetName, dataTable);

                }
                else if (dataObject != null)
                {
                    rd = new ReportDataSource(reportDataSetName, dataObject);
                }

                lr.DataSources.Add(rd);
                // Convert Report To Base64String
                string reportType = DocType;
                string mimeType;
                string encoding;
                string fileNameExtension;
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + DocType + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                fileString = Convert.ToBase64String(renderedBytes.ToArray());
                return fileString;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return errorMessage;
            }
        }
    }

    public class Parameters
    {
        public int? EmpID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }

    }


}