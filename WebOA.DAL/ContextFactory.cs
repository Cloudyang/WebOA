using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace WebOA.DAL
{
   public partial  class ContextFactory
    {
        public static DbContext GetContext()
        {
            return GetContext("WebOAEntities");
         }

        public static DbContext GetContext(string name)
        {
            DbContext context = CallContext.GetData(name) as DbContext;
            if (context == null)
            {
                context = new DbContext(string.Format("name={0}", name));
                CallContext.SetData(name, context);
            }
            return context;
        } 
    }
}
