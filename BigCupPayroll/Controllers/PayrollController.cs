using BigCupPayroll.DLL;
using CommonLibrary;
using CommonLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BigCupPayroll.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        [HttpPost]
        public string ComputePayroll([FromBody] DateCutOff dateCutOff)
        {
            DataManager dataManager = new DataManager();
            var helper = new Helper();
            var payrolls = new List<Payroll>();
            var cuttOffDates = helper.CutOffDates(dateCutOff.oCutOffDate);
            dataManager.DeletePayrollByCutOffDate(cuttOffDates.First(), cuttOffDates.First());
            dataManager.ComputePayroll(dateCutOff.oCutOffDate);

            return JsonConvert.SerializeObject(payrolls);
        }

        [HttpPost]
        public string GetPayrollHistory([FromBody] DateCutOff dateCutOff)
        {
            DataManager dataManager = new DataManager();
            var helper = new Helper();
            var payrolls = new List<Payroll>();
            var cutOffDate = helper.GetCutOff(dateCutOff.oCutOffDate);
            var cutOffDates = helper.CutOffDates(dateCutOff.oCutOffDate);
            payrolls = dataManager.GetPayrollHistory(cutOffDate,cutOffDates);

            return JsonConvert.SerializeObject(payrolls);
        }
        [HttpPost]
        public string SaveSalaryAdjustment([FromBody] AdjustedPayroll adjustedPayroll)
        {
            adjustedPayroll.newPay.allowances = new List<Allowance>();
            adjustedPayroll.newPay.deductions = new List<Deduction>();
            foreach (var item in adjustedPayroll.adjustment) 
            { 
                if(item.Amount > 0)
                {
                    item.PayrollId = adjustedPayroll.newPay.Id;
                    adjustedPayroll.newPay.allowances.Add(item);
                }
                else
                {
                    adjustedPayroll.newPay.deductions.Add(new Deduction{ 
                      Amount = item.Amount, 
                      Description = item.Description,
                      PayrollId = adjustedPayroll.newPay.Id
                    });
                }
            }

            DataManager dataManager = new DataManager();
            dataManager.AdjustEmployeePayroll(adjustedPayroll.newPay);


            return "";
        }
    }
}
