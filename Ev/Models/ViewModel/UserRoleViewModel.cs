using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ev.Models.ViewModel
{
    public class UserRoleViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}