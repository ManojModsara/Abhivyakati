using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace QuizGame.LIBS
{
    public class SMSapiCall
    {
        LogExcept logException = new LogExcept();
        public string Mailsend(string sentmail, string mailsubject, string mailbody)
        {

            try
            {//yaha se mail jata hai 100% otp
                //smsachariyamsg sms = new smsachariyamsg();

                string Username = WebConfigurationManager.AppSettings["PFUserName"].ToString();
                string Password = WebConfigurationManager.AppSettings["PFPassWord"].ToString();
                string MailServer = WebConfigurationManager.AppSettings["MailServerName"].ToString();
                string MailPort = WebConfigurationManager.AppSettings["MailServerport"].ToString();
                string Mailsslstatus = WebConfigurationManager.AppSettings["Mailssl"].ToString();
                MailMessage mail1 = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(MailServer);
                mail1.From = new MailAddress(Username); //you have to provide your gmail address as from address
                mail1.To.Add(sentmail);
                mail1.Subject = mailsubject;
                mail1.Body = mailbody;
                SmtpServer.Port = Convert.ToInt32(MailPort);
                SmtpServer.Credentials = new System.Net.NetworkCredential(Username, Password);//you have to provide you gamil username and password
                SmtpServer.EnableSsl = Convert.ToBoolean(Mailsslstatus.ToString());
                SmtpServer.Send(mail1);
                mail1.Dispose();

            }

            catch (Exception ex)
            {
                logException.LogExceptions(ex);
                return "failed";
            }
            return "Success";

        }


        public string sendsms(string Mobileno, string message)
        {
            string result = "";
            try
            {
                DataTable dt = SmsData();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string apiurl = dt.Rows[0]["apiurl"].ToString();
                        apiurl = apiurl.Replace("mmm", Mobileno.Trim());
                        apiurl = apiurl.Replace("msg", message);
                        result = apicall(apiurl);
                        SmsDataInsert(dt.Rows[0]["smsapi_id"].ToString(), Mobileno.Trim(), message, result);

                    }
                }
                return result;


            }
            catch (Exception ex)
            {
                logException.LogExceptions(ex);
                return "FAILED";
            }

            //return result;

        }


        public string apicall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

            try
            {

                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

                StreamReader sr = new StreamReader(httpres.GetResponseStream());

                string results = sr.ReadToEnd();

                sr.Close();
                return results;



            }
            catch (Exception ex)
            {
                logException.LogExceptions(ex);
                return "Processing";
            }

        }

        public DataTable SmsData()
        {
            DataTable dsData = new DataTable();
            SqlConnection conn;
            SqlDataAdapter sqlCmd;
            try
            {
                using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();
                    sqlCmd = new SqlDataAdapter("smsapiactive", conn);
                    sqlCmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Fill(dsData);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                logException.LogExceptions(ex);
                dsData = null;
            }
            return dsData;
        }

        public void SmsDataInsert(string smsid, string mobileno, string msg, string Response)
        {
            SqlConnection conn;
            SqlCommand sqlCmd;
            try
            {
                using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();
                    sqlCmd = new SqlCommand("Smsinsert", conn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@smsid", smsid);
                    sqlCmd.Parameters.AddWithValue("@Mobileno", mobileno);
                    sqlCmd.Parameters.AddWithValue("@message", msg);
                    sqlCmd.Parameters.AddWithValue("@Response", Response);
                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                logException.LogExceptions(ex);
            }
        }
    }
}