using QuizGame.Data;
using QuizGame.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace QuizGame.Models.Secuirity
{
    public class CustomPrincipal : IPrincipal
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public byte[] Roles { get; set; }

        [JsonIgnore]
        public IIdentity Identity { get; private set; }

        public CustomPrincipal() { }

        public CustomPrincipal(string userName, params byte[] roleTypes)
        {
            this.Identity = new GenericIdentity(userName);
            this.UserName = userName;
            this.Roles = roleTypes;
        }

        public CustomPrincipal(User user, params byte[] roleTypes)
        {
            this.UserID = (int)user.Id;
            this.Identity = new GenericIdentity(Convert.ToString(user.Id));
            this.UserName = user.Username;
            this.RoleId = (int)user.Role.Id;
            this.Roles = roleTypes;
        }

        public bool IsInRole(Object roleType)
        {
            return Roles.Contains((byte)roleType);
        }

        public bool IsInRole(params Object[] roleTypes)
        {
            return roleTypes.Any(r => Roles.Contains((byte)r));
        }

        public bool IsInRole(string role)
        {
            //Check with enum
            //Object roleType;
            //if (Enum.TryParse(role, out roleType)) { return IsInRole(roleType); }
            return false;
        }
    }
}