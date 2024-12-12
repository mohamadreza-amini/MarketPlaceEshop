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
        public static DateTime ToSolar(this DateTime persianDate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            DateTime gregorianDate = persianCalendar.ToDateTime(persianDate.Year, persianDate.Month, persianDate.Day,
                                                                  persianDate.Hour, persianDate.Minute, persianDate.Second,
                                                                  persianDate.Millisecond);

            return gregorianDate;
        }



        public static DateTime ToAD(this DateTime persianDate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(persianDate);
            int month = persianCalendar.GetMonth(persianDate);
            int day = persianCalendar.GetDayOfMonth(persianDate);
            int hour = persianCalendar.GetHour(persianDate);
            int minute = persianCalendar.GetMinute(persianDate);
            int second = persianCalendar.GetSecond(persianDate);

            DateTime gregorianDate = new DateTime(year, month, day, hour, minute, second, new GregorianCalendar());
            return gregorianDate;
        }
    }
}

