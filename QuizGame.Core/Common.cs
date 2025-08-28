using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Core
{
    public class Common
    {
        public static string GetHourMinute(TimeSpan span)
        {
            var hours = (int)span.TotalHours;
            var minutes = span.Minutes.ToString("00");
            //return string.Format("{0} hr {1} min", hours, minutes);
            string hour = hours > 0 ? hours.ToString() + " hr " : string.Empty;
            string minute = minutes != "00" ? minutes + " min" : string.Empty;
            return hour + minute;
        }
        public static DateTime GetStartDateOfWeek(DateTime value)
        {
            // Get rid of the time part first...
            value = value.Date;
            int daysIntoWeek = (int)value.DayOfWeek;
            DateTime weekStartDate = value.AddDays(-daysIntoWeek);
            return weekStartDate;
        }
        public static DateTime GetEndDateOfWeek(DateTime value)
        {
            // Get rid of the time part last...
            value = value.Date;
            int daysIntoWeek = (int)value.DayOfWeek;
            DateTime weekStartDate = value.AddDays(-daysIntoWeek);
            DateTime weekEndDate = value.AddDays(7 - daysIntoWeek - 1);
            return weekEndDate;
        }

        public static string GetStartEndDateOfWeek(DateTime value)
        {
            // Get rid of the time part first and last date string...
            value = value.Date;
            int daysIntoWeek = (int)value.DayOfWeek;
            DateTime weekStartDate = value.AddDays(-daysIntoWeek);
            DateTime weekEndDate = value.AddDays(7 - daysIntoWeek - 1);
            return weekStartDate.ToString("dd/MM/yyyy") + " - " + weekEndDate.ToString("dd/MM/yyyy");
        }

        public static string GetUniqueNumber(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return "E" + dt + "T" + string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueNumberic(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueAlphaticLW(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "abcdefghijklmnopqrstuvwxyz", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueAlphaticUP(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueAlphaticMix(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueAlphaNumericLW(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "0123456789abcdefghijklmnopqrstuvwxyz", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueAlphaNumericUP(int length = 11)
        {
            var rndDigits = new StringBuilder().Insert(0, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueAlphaNumericMIX(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public static string GetUniqueAlphaNumeric(int length = 11)
        {
            string dt = DateTime.Now.ToString("yyMMddhhmmss");
            var rndDigits = new StringBuilder().Insert(0, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

    }
}
