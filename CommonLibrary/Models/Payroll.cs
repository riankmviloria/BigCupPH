using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public DateTime CutOffDate { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public int UserId { get; set; }
        public string Employee { get; set; }
        public decimal BasicPay { get; set; }
        public int PresentDay { get; set; }
        public List<Allowance> allowances { get; set; }
        public List<Deduction> deductions { get; set; }

    }

    public class AdjustedPayroll
    {
        public Payroll newPay { get; set; }
        public List<Allowance> adjustment { get; set; }
    }
}
