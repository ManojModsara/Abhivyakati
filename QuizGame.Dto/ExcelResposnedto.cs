using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
   public class ExcelResposnedto
    {
        public string DomainName { get; set; }
        public string Country { get; set; }
        public string MobileNo { get; set; }
        public string technialEmail { get; set; }
        public string Admin_Email_id { get; set; }

    }
    public class EmptyExcelResposnedto
    {
        public string domain_name { get; set; }
        public string administrative_email { get; set; }
        public string registrant_country { get; set; }
        public string registrant_phone { get; set; }
        public string technical_email { get; set; }

    }
}
