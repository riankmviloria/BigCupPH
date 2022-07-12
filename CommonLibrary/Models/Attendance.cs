using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class Attendance
    {
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Status { get; set; }
        public DateTime WorkDate { get; set; }
        public int UserId { get; set; }
    }

    public class EmpAttendance
    {
        public Employee Employee { get; set; }
        public List<Attendance> Attendances { get; set; }   
    }

    public class CutOffAttendance
    {
        public string Employee { get; set; }
        public int EmployeeId { get; set; }
        public List<Present> Attendances { get; set; }
        public string oCutOffDate { get; set; }
    }

    public class DateCutOff
    {
        public string oCutOffDate { get; set; }
    }


    public class Present
    {
        public string Date { get; set; }
        public bool isPresent { get; set; }
    }
}
