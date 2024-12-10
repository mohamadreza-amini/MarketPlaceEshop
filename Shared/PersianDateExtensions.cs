using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class PersianDateExtensions
    {
        public static DateTime ToGregorian(this DateTime persianDate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            DateTime gregorianDate = persianCalendar.ToDateTime(persianDate.Year, persianDate.Month, persianDate.Day,
                                                                  persianDate.Hour, persianDate.Minute, persianDate.Second,
                                                                  persianDate.Millisecond);

            return gregorianDate;
        }
    }
}
