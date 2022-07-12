using BigCupPayroll.DLL;
using CommonLibrary;
using CommonLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace BigCupPayroll.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
       


        [HttpPost]
        public string GetAttendanceByCutOffDate([FromBody] DateCutOff dateCutOff)
        {
            var cutOffDates = CutOffDates(dateCutOff);
            var dataManager = new DataManager();
            var dateFrom = cutOffDates.FirstOrDefault();
            var dateTo = cutOffDates.LastOrDefault();
            var employeeAttendances =  dataManager.GetEmployeeAttendances(dateFrom,dateTo);
            var cutOffAttendances = new List<CutOffAttendance>();

           

                foreach (var employeeAttendance in employeeAttendances)
                {
                    var cutOffAttendance = new CutOffAttendance();
                    cutOffAttendance.EmployeeId = employeeAttendance.employee.Id;
                    cutOffAttendance.Employee = $"{employeeAttendance.employee.LastName}, {employeeAttendance.employee.FirstName}" ;
                    cutOffAttendance.Attendances = new List<Present>();
                    var empAttendace = employeeAttendance.attendances.Where(x => x.UserId == employeeAttendance.employee.Id && x.Status == "Present").Select(x => x.WorkDate).ToList();
                    if (employeeAttendance.attendances.Count > 0)
                    {
                        foreach(var cutOffDate in cutOffDates)
                        {
                            var present = new Present();
                            present.Date = cutOffDate;
                            present.isPresent = false;
                            if (empAttendace.Contains(Convert.ToDateTime(cutOffDate)))
                            {
                                present.isPresent = true;
                            }
                            cutOffAttendance.Attendances.Add(present);
                        }

                    }
                    else
                    {
                        foreach (var cutOffDate in cutOffDates)
                        {
                            var present = new Present();
                            present.Date = cutOffDate;
                            present.isPresent = false;
                            cutOffAttendance.Attendances.Add(present);
                        }
                    }

                    cutOffAttendances.Add(cutOffAttendance);
                }

            return JsonConvert.SerializeObject(cutOffAttendances);
        }

        [HttpPost]
        public List<string> CutOffDates([FromBody] DateCutOff dateCutOff)
        {
            
            DateTime date = Convert.ToDateTime(dateCutOff.oCutOffDate);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var cutOffDayOfMonth = new DateTime(date.Year, date.Month, 15);
            var secondCutOffDayOfMonth = new DateTime(date.Year, date.Month, 16);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var dates = new List<string>();
            if (date.Day <= 15)
            {
                dates = Enumerable.Range(0, 1 + cutOffDayOfMonth.Subtract(firstDayOfMonth).Days)
                            .Select(offset => firstDayOfMonth.AddDays(offset).ToShortDateString())
                            .ToList(); 
            }
            else
            {
                dates = Enumerable.Range(0, 1 + lastDayOfMonth.Subtract(secondCutOffDayOfMonth).Days)
                            .Select(offset => secondCutOffDayOfMonth.AddDays(offset).ToShortDateString())
                            .ToList();
            }

            return dates;
        }


        [HttpPost]
        public void UpdateAttendance([FromBody] List<CutOffAttendance> employeeAttendance)
        {
            var helper = new Helper();
            var cutOffDate = employeeAttendance.First().oCutOffDate;
            DataManager dataManager = new DataManager();
            var cuttOffDates = helper.CutOffDates(cutOffDate);
            dataManager.DeletAttendancesByCutOff(cuttOffDates.First(), cuttOffDates.First());
            foreach (var item in employeeAttendance)
            {
                var attendances = new List<Attendance>();
                foreach(var i in item.Attendances)
                {
                    var attendance = new Attendance()
                    {
                        TimeIn = Convert.ToDateTime(i.Date),
                        Status = i.isPresent ? "Present" : "Absent",
                        TimeOut = Convert.ToDateTime(i.Date),
                        UserId = item.EmployeeId,
                        WorkDate = Convert.ToDateTime(i.Date)
                    };
                    attendances.Add(attendance);
                }
                dataManager.UpdateAttendance(attendances);
                attendances.Clear();

            }

        }
    }
}
