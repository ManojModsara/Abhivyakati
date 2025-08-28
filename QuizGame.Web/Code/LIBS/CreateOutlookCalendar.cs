using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Text;

namespace QuizGame.Web.Code.LIBS
{
    public class CreateOutlookCalendar
    {

        private static string GetFormatedDate(DateTime date)
        {
            var yy = date.Year.ToString(CultureInfo.InvariantCulture);
            string mm;
            string dd;

            if (date.Month < 10 && date.Month.ToString().Length == 1) mm = "0" + date.Month.ToString(CultureInfo.InvariantCulture);
            else mm = date.Month.ToString(CultureInfo.InvariantCulture);

            if (date.Day < 10 && date.Day.ToString().Length == 1) dd = "0" + date.Day.ToString(CultureInfo.InvariantCulture);
            else dd = date.Day.ToString(CultureInfo.InvariantCulture);

            return yy + mm + dd;
        }
        private static string GetFormattedTime(string time)
        {
            var times = time.Split(':'); //string[]
            string hh;
            string mm;

            if (Convert.ToInt32(times[0]) < 10 && times[0].Length == 1) hh = "0" + times[0];
            else hh = times[0];

            if (Convert.ToInt32(times[1]) < 10 && times[1].Length == 1) mm = "0" + times[1];
            else mm = times[1];

            return hh + mm + "1";
        }
    }
}