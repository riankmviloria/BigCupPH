using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class Helper
    {
        public List<string> CutOffDates(string dateCutOff)
        {

            DateTime date = Convert.ToDateTime(dateCutOff);
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

        public string GetCutOff(string dateCutOff)
        {

            DateTime date = Convert.ToDateTime(dateCutOff);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var secondCutOffDayOfMonth = new DateTime(date.Year, date.Month, 16);

            if (date.Day <= 15)
            {
                return firstDayOfMonth.ToShortDateString();
            }
            else
            {
                return secondCutOffDayOfMonth.ToShortDateString();
            }
        }
    }
}
