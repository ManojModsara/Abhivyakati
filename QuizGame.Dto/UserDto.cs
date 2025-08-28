using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class UserDto
    { 
        public int Uid { get; set; }
        public int Parentid { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsLocked { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastLogin { get; set; }

        public string EmailOffice { get; set; }
        public string MobileNumber { get; set; }
        public Boolean IsSuperAdmin { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ResetCode { get; set; }
        public string TypeMemberName { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string GSTNO { get; set; }
        public decimal SecretDeposit { get; set; }
        public List<ParentList> ParentLists { get; set; }

    }

    public class ParentList
    {
        public int Agentid { get; set; }
        public int Roleid { get; set; }
        public string ShopName { get; set; }
    }
}
