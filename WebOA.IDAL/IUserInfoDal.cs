using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebOA.IDAL
{
    public partial interface IUserInfoDal : IBaseDal<WebOA.Model.UserInfo>
    {
        void SetRole(int uid, int[] rids);
        void SetAction(int uid, int aid, Common.IsAllow allow);
    }
}
