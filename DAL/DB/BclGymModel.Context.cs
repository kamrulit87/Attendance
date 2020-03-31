﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InvogueEntities : DbContext
    {
        public InvogueEntities()
            : base("name=InvogueEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public virtual DbSet<AttenDownloadError> AttenDownloadErrors { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<LastUpdatedRecord> LastUpdatedRecords { get; set; }
        public virtual DbSet<Organaization> Organaizations { get; set; }
        public virtual DbSet<TmpAttendance> TmpAttendances { get; set; }
        public virtual DbSet<TmpAttendanceLog> TmpAttendanceLogs { get; set; }
        public virtual DbSet<View_1> View_1 { get; set; }
        public virtual DbSet<vw_Attendance> vw_Attendance { get; set; }
        public virtual DbSet<vw_AttendanceLog> vw_AttendanceLog { get; set; }
        public virtual DbSet<vw_InsideHour> vw_InsideHour { get; set; }
        public virtual DbSet<vw_OutsideHour> vw_OutsideHour { get; set; }
    
        [DbFunction("Entities", "get_AllEmpInsideHour")]
        public virtual IQueryable<get_AllEmpInsideHour_Result> get_AllEmpInsideHour(Nullable<System.DateTime> toDate)
        {
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<get_AllEmpInsideHour_Result>("[Entities].[get_AllEmpInsideHour](@toDate)", toDateParameter);
        }
    
        [DbFunction("Entities", "get_AllEmpOutsideHour")]
        public virtual IQueryable<get_AllEmpOutsideHour_Result> get_AllEmpOutsideHour(Nullable<System.DateTime> toDate)
        {
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<get_AllEmpOutsideHour_Result>("[Entities].[get_AllEmpOutsideHour](@toDate)", toDateParameter);
        }
    
        public virtual int sp_Attendance(ObjectParameter rMessage)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Attendance", rMessage);
        }
    
        public virtual ObjectResult<sp_MonthlyLate_Result> sp_MonthlyLate(Nullable<int> month)
        {
            var monthParameter = month.HasValue ?
                new ObjectParameter("month", month) :
                new ObjectParameter("month", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_MonthlyLate_Result>("sp_MonthlyLate", monthParameter);
        }
    }
}
