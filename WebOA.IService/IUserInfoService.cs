using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebOA.Common;

namespace WebOA.IService
{
    public partial interface IUserInfoService
    {
        bool SetRole(int uid, string aids);

        bool SetAction(int uid, int aid, IsAllow allow);

        bool Login(Model.UserInfo userInfo, out int id);
    }
}
