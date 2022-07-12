using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class EmployeeAttendance
    {
        public Employee employee { get; set; }
        public List<Attendance> attendances { get; set; }
    }
}
