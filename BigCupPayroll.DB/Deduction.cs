//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BigCupPayroll.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Deduction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int PayrollId { get; set; }
    
        public virtual Payroll Payroll { get; set; }
    }
}
