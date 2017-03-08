using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebOA.IService;
using WebOA.IDAL;
using WebOA.DalFactory;

namespace WebOA.Service
{
    public abstract partial class BaseService<T>:IService<T>  where T :class
    {
        protected IDbSession dbSession = DbSessionFactory.GetDbSession();

        protected IBaseDal<T> curDal;

        protected abstract IBaseDal<T> GetDal();

        public BaseService()
        {
            curDal = GetDal();
        }

        //增加
        public bool Add(T u)
        {
            curDal.Add(u);
            return dbSession.SaveChanges() > 0;
        }
        //修改
        public bool Edit(T u)
        {
            curDal.Edit(u);
            return dbSession.SaveChanges() > 0;
        }
        //删除
        public bool Remove(T u)
        {
            curDal.Remove(u);
            return dbSession.SaveChanges() > 0;
        }

        public bool Remove(int id)
        {
            curDal.Remove(id);
            return dbSession.SaveChanges() > 0;
        }

        public bool Remove(int[] ids)
        {
            curDal.Remove(ids);
            return dbSession.SaveChanges() > 0;
        }

        //查询
        public T GetById(int id)
        {
            return curDal.GetById(id);
        }

        public List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return curDal.GetList(whereLambda).ToList();
        }

        public List<T> GetPageList<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, TKey>> orderLambda, int pageIndex, int pageSize,out int total)
        {
            return curDal.GetPageList<TKey>(whereLambda, orderLambda, pageIndex, pageSize,out total).ToList();
        }
    }
}
