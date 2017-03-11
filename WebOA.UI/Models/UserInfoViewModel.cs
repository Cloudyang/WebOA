using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOA.UI.Models
{
    [Serializable]
    public class UserInfoViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }
}