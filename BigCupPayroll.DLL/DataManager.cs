using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Models;
using Dapper;

namespace BigCupPayroll.DLL
{
    public class DataManager
    {
        private string bigCupPayrollConnectionString = string.Empty;
        public DataManager()
        {
            bigCupPayrollConnectionString = Configuration.GetConnectionString("BigCupPayrollDatabase");
        }

        public List<Employee> GetEmployees()
        {
            var getQuery = "select distinct u.Id,UserName,LastName,FirstName,(select top 1 BasicPay from Salaries s where s.UserId = u.Id order by EffectiveDate desc) as BasicPay,HiredDate,Type from users u inner join roles r on r.Id = u.Role_Id left join salaries s on u.id = s.userid";
            var employees = new List<Employee>();
            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    employees = db.Query<Employee>(getQuery).ToList();
                }

            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }
            return employees;
        }

        public List<Employee> UpdateEmployeeBasicPay(Employee employee)
        {
            var getQuery = "insert into Salaries values (@BasicPay,@EffectiveDate,@UserId)";
            var employees = new List<Employee>();
            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    db.Execute(getQuery, new { BasicPay = employee.BasicPay, EffectiveDate = DateTime.Now, UserId = employee.Id });
                }

            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }
            return employees;
        }


        public List<Payroll> GetPayrollHistory(string dateCutOff,List<string> cutOffDates)
        {
            var getQuery = $"select(select LastName + ', ' + FirstName from Users u where u.id = p.userId ) as Employee,[PresentDay] = (select count(*) from Attendances where userid = p.userId and Convert(date, Workdate) >= Convert(date, '{cutOffDates.First()}') and Convert(date, Workdate) <= Convert(date, '{cutOffDates.Last()}') and status = 'Present'),[BasicPay]= (select top 1 BasicPay from Salaries s where userId = p.userId order by id desc) ,*from payrolls p where Convert(date, CutOffDate) = Convert(date, '{dateCutOff}')";
            var getAllowancesQuery = "select * from Allowances where PayrollId = @Id";
            var getDeductionsQuery = "select * from Deductions where PayrollId = @Id";


            var payrolls = new List<Payroll>();

            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    payrolls = db.Query<Payroll>(getQuery).ToList();

                    foreach (var item in payrolls)
                    {
                        var allowances = db.Query<Allowance>(getAllowancesQuery, new { Id = item.Id }).ToList();
                        var deductions = db.Query<Deduction>(getDeductionsQuery, new { Id = item.Id }).ToList();
                        item.allowances = new List<Allowance>();
                        item.deductions = new List<Deduction>();
                        item.allowances.AddRange(allowances);
                        item.deductions.AddRange(deductions);
                    }
                }
            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }


            return payrolls;
        }

        public List<Payroll> ComputePayroll(string cutoffdate)
        {
            var helper = new Helper();
            var cutOffDate = helper.CutOffDates(cutoffdate);
            var currentCutOff = helper.GetCutOff(cutoffdate);
            var employeeAttendances = GetEmployeeAttendances(cutOffDate.First(), cutOffDate.Last());

            var payrolls = new List<Payroll>();
            foreach (var empAtt in employeeAttendances)
            {
                decimal netPay = 0;
                decimal grossPay = 0;
                foreach (var att in empAtt.attendances)
                {
                    grossPay += att.Status == "Present" ? empAtt.employee.BasicPay : 0;
                }
                payrolls.Add(new Payroll
                {
                    GrossPay = grossPay,
                    NetPay = grossPay,
                    UserId = empAtt.employee.Id,
                    CutOffDate = Convert.ToDateTime(currentCutOff)
                });
            }

            RecordPayroll(payrolls);

            return payrolls;
        }
        public List<Payroll> AdjustEmployeePayroll(Payroll payroll)
        {
            using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
            {
                db.Execute("UPDATE PAYROLLS SET NetPay = @NetPay where UserId = @UserId and Id = @Id", payroll);
                db.Execute("Delete From Allowances where PayrollId = @Id", new { Id = payroll.Id });
                db.Execute("Insert into Allowances values (@Description,@Amount,@PayrollId)", payroll.allowances);
                db.Execute("Delete From Deductions where PayrollId = @Id", new { Id = payroll.Id });
                db.Execute("Insert into Deductions values (@Description,@Amount,@PayrollId)", payroll.deductions);
            }

            return null;
        }
        public void DeletePayrollByCutOffDate(string cutOffFrom, string cutOffTo)
        {
            cutOffFrom = Convert.ToDateTime(cutOffFrom).ToString("yyyy-MM-dd");
            cutOffTo = Convert.ToDateTime(cutOffTo).ToString("yyyy-MM-dd");

            var deleteQuery = $"delete from Payrolls where cutoffdate >= '{cutOffFrom}' and cutoffdate <= '{cutOffTo}'";

            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    db.Execute(deleteQuery);
                }

            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }
        }
        public void RecordPayroll(List<Payroll> payrolls)
        {
            using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
            {
                db.Execute("INSERT INTO PAYROLLS VALUES (@CutOffDate, @GrossPay,@NetPay,@UserId)", payrolls);
            }
        }

        public List<EmployeeAttendance> GetEmployeeAttendances(string dateFrom, string dateTo)
        {
            var getQuery = $"select * from attendances where convert(date,workdate) >=  convert(date,'{dateFrom}') and convert(date,workdate) <= convert(date,'{dateTo}')";
            var attendances = new List<Attendance>();
            var employees = GetEmployees();
            var employeeAttendances = new List<EmployeeAttendance>();

            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    attendances = db.Query<Attendance>(getQuery).ToList();
                }
                    foreach (var employee in employees)
                    {
                        employeeAttendances.Add(new EmployeeAttendance()
                        {
                            employee = employee,
                            attendances = attendances.Where(x => x.UserId == employee.Id).ToList()
                        });

                    }
            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }
            return employeeAttendances;
        }

        public List<Attendance> GetAttendancesByCutOffDate()
        {
            var getQuery = "select (u.FirstName +' '+ u.LastName) as Employee,a.* from Attendances a right join Users u on u.Id = a.UserId order by u.FristName";
            var empAttendances = new List<Attendance>();


            var employees = GetEmployees();
            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    foreach (var item in employees)
                    {
                        empAttendances = db.Query<Attendance>(getQuery).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }



            return empAttendances;
        }

        public class AttendanceFormat
        {
            public int UserId { get; set; }
            public DateTime TimeIn { get; set; }
            public DateTime TimeOut { get; set; }
            public string Status { get; set; }
            public DateTime WorkDate { get; set; }
        }

        public void DeletAttendancesByCutOff(string cutOffFrom, string cutOffTo)
        {
            cutOffFrom = Convert.ToDateTime(cutOffFrom).ToString("yyyy-MM-dd");
            cutOffTo = Convert.ToDateTime(cutOffTo).ToString("yyyy-MM-dd");

            var deleteQuery = $"delete from Attendances where workdate >= '{cutOffFrom}' and workdate <= '{cutOffTo}'";

            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    db.Execute(deleteQuery);
                }

            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }


        }
        public void UpdateAttendance(List<Attendance> attendances)
        {
           
            var insertQuery = "insert into Attendances values (@TimeIn,@TimeOut,@Status,@WorkDate,@UserId)";
            var newAttendances = new List<AttendanceFormat>();
            
            try
            {
                using (IDbConnection db = new SqlConnection(bigCupPayrollConnectionString))
                {
                    db.Execute(insertQuery, attendances);
                }

            }
            catch (Exception ex)
            {
                //// We create a log of the exception but we still let the caller know.
                //Helper.CreateLog(ex.ToString(), APPNAME);
                //throw ex;
            }
        }
    }
}
