using System.Web.Mvc;

namespace IASystemWeb.Areas.AttendanceReports
{
    public class AttendanceReportsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AttendanceReports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AttendanceReports_default",
                "AttendanceReports/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}