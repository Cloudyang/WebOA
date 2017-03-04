using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebOA.DAL;
using WebOA.Model;

namespace WebOA.Service
{
    public partial class UserInfoService
    {
        private UserInfoDal userInofDal = new UserInfoDal();

        //增加
        public bool Add(UserInfo u)
        {
            return userInofDal.Add(u) > 0;
        }
        //修改
        public bool Edit(UserInfo u)
        {
            return userInofDal.Edit(u) > 0;
        }
        //删除
        public bool Remove(UserInfo u)
        {
            return userInofDal.Remove(u) > 0;
        }

        public bool Remove(int id)
        {
            return userInofDal.Remove(id) > 0;
        }

        public bool Remove(int[] ids)
        {
            return userInofDal.Remove(ids) > 0;
        }

        //查询
        public UserInfo GetById(int id)
        {
            return userInofDal.GetById(id);
        }

        public List<UserInfo> GetList(System.Linq.Expressions.Expression<Func<UserInfo, bool>> whereLambda)
        {
            return userInofDal.GetList(whereLambda).ToList();
        }

        public List<UserInfo> GetPageList<TKey>(System.Linq.Expressions.Expression<Func<UserInfo, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<UserInfo, TKey>> orderLambda, int pageIndex, int pageSize)
        {
            return userInofDal.GetPageList<TKey>(whereLambda, orderLambda, pageIndex, pageSize).ToList();
        }
    }
}
