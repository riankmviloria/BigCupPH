using BigCupPayroll.DLL;
using CommonLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BigCupPayroll.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public string GetEmployees()
        {
            DataManager dataManager = new DataManager();
            var employees = dataManager.GetEmployees();
            return JsonConvert.SerializeObject(employees);

        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void UpdateEmployeeBasicPay([FromBody] Employee employee)
        {
            DataManager dataManager = new DataManager();
            dataManager.UpdateEmployeeBasicPay(employee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
