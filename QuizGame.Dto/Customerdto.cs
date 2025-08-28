using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class Customerdto
    {
        public int ID { get; set; }
        public int Parentid { get; set; }
        public string Name { get; set; }
        public string EMailID { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsLocked { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
