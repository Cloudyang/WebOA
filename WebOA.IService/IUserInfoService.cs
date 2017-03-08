using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebOA.Model;

namespace WebOA.IService
{
    public interface IService<T> where T :class
    {
        bool Add(T u);
        bool Edit(T u);
        T GetById(int id);
        List<T> GetList(Expression<Func<T, bool>> whereLambda);
        List<T> GetPageList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, int pageIndex, int pageSize,out int total);
        bool Remove(T u);
        bool Remove(int[] ids);
        bool Remove(int id);
    }
}