using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebOA.IDAL
{
    public interface IBaseDal<T> where T : class
    {
        void Add(T userInfo);
        void Edit(T userInfo);
        T GetById(int id);
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetPageList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, int pageIndex, int pageSize, out int total);
        void Remove(T u);
        void Remove(int[] ids);
        void Remove(int id);
    }
}