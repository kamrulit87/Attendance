//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_Attendance
    {
        public Nullable<int> EmpID { get; set; }
        public Nullable<int> EmpNumber { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string WorkingHour { get; set; }
        public System.DateTime WDate { get; set; }
        public string LateStatus { get; set; }
        public string OfficeStatus { get; set; }
        public int AttendanceID { get; set; }
        public string LateNotify { get; set; }
        public string LateEmailSend { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<System.DateTime> LastInTime { get; set; }
    }
}
