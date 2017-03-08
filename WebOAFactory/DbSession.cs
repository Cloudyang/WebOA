using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebOA.DAL;
using WebOA.IDAL;

namespace WebOA.DalFactory
{
    public partial class DbSession : IDbSession
    {
        public int SaveChanges()
        {
            DbContext context = ContextFactory.GetContext();
            return context.SaveChanges();
        }
    }
}
