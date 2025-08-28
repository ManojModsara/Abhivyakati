using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

namespace QuizGame.Web.Code.LIBS
{
    public static class SiteKey
    {
        public static string DomainName
        {
            get { return ConfigurationManager.AppSettings["DomainName"]; }
        }
        public static string DomainNameInternalIP
        {
            get { return ConfigurationManager.AppSettings["DomainNameInternalIP"]; }
        }
        public static string AshishTeamPMUId
        {
            get { return ConfigurationManager.AppSettings["AshishTeamPMUId"]; }
        }
        public static string From { get { return ConfigurationManager.AppSettings["From"]; } }
        public static string To { get { return ConfigurationManager.AppSettings["To"]; } }
        public static string CC { get { return ConfigurationManager.AppSettings["CC"]; } }
        public static string BCC { get { return ConfigurationManager.AppSettings["BCC"]; } }
        public static string RefreshActivity { get { return ConfigurationManager.AppSettings["RefreshActivity"]; } }

        public static string ProjectClosureToEmail { get { return ConfigurationManager.AppSettings["ProjectClosureToEmail"]; } }
        public static string EscalatedToEmail { get { return ConfigurationManager.AppSettings["EscalatedToEmail"]; } }

        public static string CrmProjectList
        {
            get { return ConfigurationManager.AppSettings["crmapi"] + "projectdetail?type=projects&userid="; }
        }

        public static string CrmInvoiceList
        {
            get { return ConfigurationManager.AppSettings["crmapi"] + "updateInvoices?type=invoices&userid=@USERID&apipass=@APIPASSWORD&date=@INVOICEDATE"; }
        }

        public static string InvoiceDays
        {
            get { return ConfigurationManager.AppSettings["InvoiceDays"]; }
        }

        public static string UKDeveloperUID
        {
            get { return ConfigurationManager.AppSettings["UKDeveloperUID"]; }
        }
        public static string UKPMVirtualDeveloperID
        {
            get { return ConfigurationManager.AppSettings["UKPMVirtualDeveloperID"]; }
        }

        public enum MessageType
        {
            Info,
            Warning,
            Error,
            Success
        }

        public static string UploadResumeFolderPath
        {
            get
            {
                string folderPath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Upload", "Resume");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                return folderPath;
            }
        }
    }
}