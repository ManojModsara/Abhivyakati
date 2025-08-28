using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace QuizGame.LIBS
{
    public class LogExcept
    {
        SqlConnection con;
        public void LogExceptions(Exception ex)
        {
            try
            {
                string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ExceptionLog/");  //Text File Path
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!System.IO.File.Exists(filepath))
                {
                    System.IO.File.Create(filepath).Dispose();
                }

                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine("================= ***EXCEPTION DETAILS" + " " + DateTime.Now.ToString() + "*** =============");
                    sw.WriteLine("Error Occured:");
                    //sw.WriteLine((SiteKey.DomainName.Contains("localhost") || SiteKey.DomainName.Contains("www.dev.") ? "Test" : "Live"));
                    sw.WriteLine();

                    sw.WriteLine("Date Time:");
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Error Code:");
                    sw.WriteLine(ex.GetHashCode().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Base Exception:");
                    sw.WriteLine(ex.GetBaseException().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Exception Type:");
                    sw.WriteLine(ex.GetType().ToString());
                    sw.WriteLine();

                    sw.WriteLine("Inner Exception:");
                    sw.WriteLine(ex.InnerException.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Exception Message: ");
                    sw.WriteLine(ex.Message);
                    sw.WriteLine();

                    sw.WriteLine("Exception Source:  ");
                    sw.WriteLine(ex.Source);
                    sw.WriteLine();

                    sw.WriteLine("Stack Trace: ");
                    sw.WriteLine(ex.StackTrace.ToString());
                    sw.WriteLine();

                    sw.WriteLine("Generic Info: ");
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine();


                    sw.WriteLine("=================================== ***End*** =============================================");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception)
            {


            }

        }

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public void ActivityLog(string Tokenid, string userid, string Type, string Message, string Location)
        {
            string ip = HttpContext.Current.Request.UserHostAddress.ToString();
            SqlCommand sqlCmd;
            try
            {
                connection();
                con.Open();
                sqlCmd = new SqlCommand("ActivityInsert", con); ///Insert On Log File
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@tokenid", Tokenid);
                sqlCmd.Parameters.AddWithValue("@userid", userid);
                sqlCmd.Parameters.AddWithValue("@Type", Type);
                sqlCmd.Parameters.AddWithValue("@Messages", Message);
                sqlCmd.Parameters.AddWithValue("@Location", Location);
                sqlCmd.Parameters.AddWithValue("@Ip", ip);
                sqlCmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                LogExceptions(ex);
            }
        }
    }
}