using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebOA.IDAL;
using WebOA.IService;
using WebOA.Model;

namespace WebOA.Service
{
    public partial class UserInfoService : IUserInfoService
    {
        public bool SetRole(int uid, string ridstr)
        {
            List<int> intList = new List<int>();
            string[] aids = ridstr.Split(',');
            foreach (var item in aids)
            {
                intList.Add(int.Parse(item));
            }
            ((IUserInfoDal)curDal).SetRole(uid, intList.ToArray());
            return dbSession.SaveChanges() > 0;
        }

        public bool SetAction(int uid, int aid, Common.IsAllow allow)
        {
            ((IUserInfoDal)curDal).SetAction(uid, aid, allow);
            return dbSession.SaveChanges() > 0;
        }

        public bool Login(UserInfo userInfo, out int id)
        {
            string username = userInfo.UserName;
            string pwd = Common.MD5Helper.EncryptString(userInfo.UserPwd);
            var result = curDal.GetList(u => (u.UserName.Equals(username) && (u.UserPwd.Equals(pwd))));
            var user1 = result.FirstOrDefault();
            id = 0;
            if (user1 != null)
            {
                id = user1.UserId;
            }
            return user1 != null;
        }
    }
}
