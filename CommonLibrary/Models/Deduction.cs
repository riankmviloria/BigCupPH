using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class Deduction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int PayrollId { get; set; }
    }
}
