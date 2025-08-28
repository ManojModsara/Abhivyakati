using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
   public class Vendordto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string GSTNO { get; set; }
        public string GSTAddress { get; set; }
        public int MediumID { get; set; }

    }
}
