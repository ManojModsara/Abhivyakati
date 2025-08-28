using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI;
using System.Net;
using System.Data;
using System.Reflection;
using System.IO;
using System.Globalization;
using QuizGame.Core;

namespace QuizGame.Web.LIBS
{
    public static class Common
    {
        
        public static DateTime ConvertToDateTime(string DateString, string inputFormat)
        {
            try
            {
                string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

                DateTime outDate;
                string inDate;

                if (sysFormat.ToLower() != inputFormat.ToLower())
                {
                    string[] sDate = DateString.Split('/');
                    if ((sysFormat.ToLower() == "dd/mm/yyyy" && inputFormat.ToLower() == "mm/dd/yyyy") || (sysFormat.ToLower() == "mm/dd/yyyy" && inputFormat.ToLower() == "dd/mm/yyyy"))
                    {
                        inDate = inDate = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                        outDate = Convert.ToDateTime(inDate);
                    }

                    else if (sysFormat.ToLower() == "yyyy/mm/dd" && inputFormat.ToLower() == "mm/dd/yyyy")
                    {
                        inDate = inDate = sDate[2] + '/' + sDate[0] + '/' + sDate[1];
                        outDate = Convert.ToDateTime(inDate);
                    }

                    else if (sysFormat.ToLower() == "yyyy/mm/dd" && inputFormat.ToLower() == "dd/mm/yyyy")
                    {
                        inDate = inDate = sDate[2] + '/' + sDate[1] + '/' + sDate[0];
                        outDate = Convert.ToDateTime(inDate);
                    }
                    else
                    {
                        outDate = Convert.ToDateTime(DateString);
                    }

                    return outDate;

                }

                else
                {
                    return Convert.ToDateTime(DateString);
                }

            }
            catch
            {
                return Convert.ToDateTime(DateString);
            }

        }
        
        public static void BindControl(Control CT, IEnumerable<dynamic> DATA)
        {
            PropertyInfo PI = CT.GetType().GetProperty("DataSource");
            if (PI != null)
            {
                PI.SetValue(CT, DATA, null);
                CT.DataBind();
            }
        }

        public static void BindControl(Control CT, IEnumerable<dynamic> DATA, string DataTextField, string DataValueField, string DefaultValue)
        {
            PropertyInfo PI = CT.GetType().GetProperty("DataSource");

            if (PI != null)
            {
                PI.SetValue(CT, DATA, null);

                PI = CT.GetType().GetProperty("DataTextField");
                if (PI != null) { PI.SetValue(CT, DataTextField, null); }

                PI = CT.GetType().GetProperty("DataValueField");
                if (PI != null) { PI.SetValue(CT, DataValueField, null); }

                CT.DataBind();

                if (CT.GetType().Name == "DropDownList" && !String.IsNullOrEmpty(DefaultValue))
                {
                    PI = CT.GetType().GetProperty("Items");
                    MethodInfo MI = PI.PropertyType.GetMethod("Insert", new[] { typeof(int), typeof(string) });
                    MI.Invoke(PI.GetValue(CT, null), new object[] { 0, DefaultValue });
                }
            }
        }
        
        //for Listbox:provide ListBox control.
        public static string GetSelectedItems(CheckBoxList control, bool Text = false)
        {
            if (!Text)
                return String.Join(",", control.Items.Cast<ListItem>().Where(I => I.Selected).Select(I => I.Value).ToArray());
            else
                return String.Join(",", control.Items.Cast<ListItem>().Where(I => I.Selected).Select(I => I.Text).ToArray());

        }

        public static void SetSelectedItems(CheckBoxList chkListTechnology, string TecData)
        {
            foreach (ListItem list in chkListTechnology.Items)
            {
                for (int i = 0; i < TecData.Split(',').Count(); i++)
                {
                    if (list.Value == TecData.Split(',')[i].ToString())
                    {
                        list.Selected = true;
                    }
                }
            }
        }
        
        public static void ClearInputs(ControlCollection control)
        {
            foreach (Control ctrl in control)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();
                else if (ctrl is CheckBoxList)
                    ((CheckBoxList)ctrl).ClearSelection();
                else if (ctrl is RadioButtonList)
                    ((RadioButtonList)ctrl).ClearSelection();
                ClearInputs(ctrl.Controls);
            }
        }

        public static string showNotificationMsg(string Title, string Description, SiteKey.MessageType MessageType)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<div class='" + MessageType.ToString().ToLower() + " message'>");
            str.Append("<h3>" + Title + "</h3>");
            str.Append("<p>" + Description + "</p></div>");

            return str.ToString();

        }

        public static string TrimLength(string input, int length, bool Incomplete = true)
        {
            if (String.IsNullOrEmpty(input)) { return String.Empty; }
            return input.Length > length ? String.Concat(input.Substring(0, length), Incomplete ? "..." : "") : input;
        }

        public static string getip()
        {
            var IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            string sHostName = Dns.GetHostName();
            IPHostEntry ipE = Dns.GetHostEntry(sHostName);
            IPAddress[] IpA = ipE.AddressList;
            if (IpA.Count() > 0)
                IP = Convert.ToString(IpA.FirstOrDefault(I => I.ToString().Split('.')[0] == "192"));

            return IP;
        }

        public static string WeekAndDay(int noofday)
        {
            int weeks = noofday / 5;
            int days = noofday % 5;
            string result = ""; ;
            string weektypes = "Weeks";
            string daytypes = "Days";

            if (days == 1) { daytypes = "Day"; }
            if (weeks == 1) { weektypes = "Week"; }

            if (days == 0) { daytypes = ""; }
            if (weeks == 0) { weektypes = ""; }

            if (weeks == 0)
                result = days + " " + daytypes;
            if (days == 0)
                result = weeks.ToString() + " " + weektypes;
            if (weeks != 0 && days != 0)
                result = weeks.ToString() + " " + weektypes + "  " + days + " " + daytypes;
            if (weeks == 0 && days == 0)
                result = "";

            return result;
        }

        public static string SaveFile(FileUpload FileUpload, string Folder, string prefix)
        {
            if (FileUpload.HasFile)
            {
                string Filename = prefix + Path.GetFileName(FileUpload.PostedFile.FileName.Replace('-', '_'));

                string FilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + Folder), Filename);
                FileUpload.SaveAs(FilePath);

                return Filename;
            }

            return String.Empty;
        }

        public static void CreateFile(string Content, string fileName)
        {
            using (FileStream fs = File.Create(fileName))
            {
                byte[] bytes = new byte[Content.Length * sizeof(char)];
                System.Buffer.BlockCopy(Content.ToCharArray(), 0, bytes, 0, bytes.Length);

                fs.Write(bytes, 0, Content.Length * sizeof(char));
            }
        }
        
        public static string StripTagsCharArray(string source)
        {
            if (!String.IsNullOrEmpty(source))
            {
                char[] array = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }
                }
                return new string(array, 0, arrayIndex);
            }
            else
            {
                return String.Empty;
            }
        }
        
        //Project Closer

        public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<int, string>();

            foreach (int value in Enum.GetValues(enumerationType))
            {

                var name = Enum.GetName(enumerationType, value);
                dictionary.Add(value, name);
            }

            return dictionary;
        }
        // Messages

        public const string RecordUpdateMessage = "Record has been Submitted successfully";
        public const string RequestSubmittedMessage = "Request has been submitted successfully";
        public const string RecordDeleteMessage = "Record has been deleted successfully";

      



         }



}