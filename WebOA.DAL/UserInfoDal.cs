using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebOA.Common;

namespace WebOA.DAL
{
    public partial class UserInfoDal : WebOA.IDAL.IUserInfoDal
    {
        public void SetRole(int uid, int[] rids)
        {
            var userInfo = GetById(uid);
            userInfo.RoleInfo.Clear();

            var roleInfoDal = new RoleInfoDal();
            foreach (int rid in rids)
            {
                userInfo.RoleInfo.Add(roleInfoDal.GetById(rid));
            }
        }
        public void SetAction(int uid, int aid, IsAllow allow)
        {
            var userInfo = GetById(uid);

            var ual = userInfo.UserAction.Where(ua => ua.ActionId == aid).FirstOrDefault();

            if (ual != null)
            {
                if (allow == IsAllow.Allow)
                {
                    ual.IsAllow = true;
                }
                else if (allow == IsAllow.NoAllow)
                {
                    ual.IsAllow = false;
                }
                else
                {
                    userInfo.UserAction.Remove(ual);
                }
            }
            else
            {
                if (allow != IsAllow.UnDefAllow)
                {
                    ual = new Model.UserAction()
                    {
                        UserId = uid,
                        ActionId = aid
                    };
                    if (allow == IsAllow.Allow)
                    {
                        ual.IsAllow = true;
                    }
                    else if (allow == IsAllow.NoAllow)
                    {
                        ual.IsAllow = false;
                    }
                    userInfo.UserAction.Add(ual);
                }
            }

        }
    }
}
