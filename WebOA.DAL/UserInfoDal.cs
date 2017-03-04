using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebOA.Model;
using System.Linq.Expressions;

namespace WebOA.DAL
{
    public partial class UserInfoDal
    {
        DbContext context = new WebOAEntities();

        //增加
        public int Add(UserInfo userInfo)
        {
            context.Set<UserInfo>().Add(userInfo);
            return context.SaveChanges();
        }
        //修改
        public int Edit(UserInfo userInfo)
        {
            context.Entry<UserInfo>(userInfo).State = EntityState.Modified;
            return context.SaveChanges();
        }

        //删除
        public int Remove(int id)
        {
            UserInfo userInfo = context.Set<UserInfo>().Find(id);
            context.Set<UserInfo>().Remove(userInfo);
            return context.SaveChanges();
        }

        public int Remove(int[] ids)
        {
            foreach (int id in ids)
            {
                UserInfo userInfo = context.Set<UserInfo>().Find(id);
                context.Set<UserInfo>().Remove(userInfo);
            }
            return context.SaveChanges();
        }

        public int Remove(UserInfo u)
        {
            context.Set<UserInfo>().Remove(u);
            return context.SaveChanges();
        }


        //查询
        public UserInfo GetById(int id)
        {
            return context.Set<UserInfo>().Find(id);
        }

        public IQueryable<UserInfo> GetList(Expression<Func<UserInfo, bool>> whereLambda)
        {
            return context.Set<UserInfo>().Where(whereLambda);
        }

        public IQueryable<UserInfo> GetPageList<TKey>(Expression<Func<UserInfo, bool>> whereLambda
            , Expression<Func<UserInfo, TKey>> orderLambda, int pageIndex, int pageSize)
        {
            return context.Set<UserInfo>().Where(whereLambda)
                .OrderBy(orderLambda)
                .Skip<UserInfo>((pageIndex - 1) * pageSize)
                .Take<UserInfo>(pageSize);
        }
    }
}
