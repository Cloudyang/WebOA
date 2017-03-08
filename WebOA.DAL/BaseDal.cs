using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebOA.IDAL;

namespace WebOA.DAL
{
  public partial   class BaseDal<T> 
        where T :class
    {
        // DbContext context = new WebOAEntities();
        DbContext context = ContextFactory.GetContext();

        //增加
        public void Add(T t)
        {
            context.Set<T>().Add(t);
        }
        //修改
        public void Edit(T t)
        {
            context.Entry<T>(t).State = EntityState.Modified;
        }

        //删除
        public void Remove(int id)
        {
            T t = context.Set<T>().Find(id);
            this.Remove(t);
        }

        public void Remove(int[] ids)
        {
            foreach (int id in ids)
            {
                this.Remove(id);
            }
        }

        public void Remove(T u)
        {
            //  context.Set<T>().Remove(u);


            //变更删除标记            
            context.Entry(u).Property<bool>("IsDelete").IsModified = true;
            context.Entry(u).Property<bool>("IsDelete").CurrentValue = true;
        }


        //查询
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return context.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> GetPageList<TKey>(Expression<Func<T, bool>> whereLambda
            , Expression<Func<T, TKey>> orderLambda, int pageIndex, int pageSize,out int total)
        {
            total = context.Set<T>().Where(whereLambda).Count();

            return context.Set<T>().Where(whereLambda)
                .OrderBy(orderLambda)
                .Skip<T>((pageIndex - 1) * pageSize)
                .Take<T>(pageSize);
        }
    }
}
