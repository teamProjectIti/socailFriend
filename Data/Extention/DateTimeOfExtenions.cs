using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extention
{
    public static class DateTimeOfExtenions
    {
        public static int calcAge(this DateTime dte)
        {
            var today = DateTime.Today;
            var age = today.Year - dte.Year;
            if (dte.Date > today.AddYears(-age)) --age;

            return age;
        }
    }
}
