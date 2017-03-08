using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Memcached.ClientLibrary;

namespace WebOA.Common
{
    public partial class MmHelper
    {
        private MemcachedClient client;
        public MmHelper()
        {
            string[] ips = System.Configuration.ConfigurationManager.AppSettings["MemcachedServers"].Split(',');

            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(ips);
            pool.Initialize();

            client=new MemcachedClient();
            client.EnableCompression = true;
        }

        public bool Set(string guid,object value,DateTime expiryTime)
        {
            return client.Set(guid, value, expiryTime);
        }

        public object Get(string guid)
        {
            return client.Get(guid);
        }

        public bool Delete(string guid)
        {
            return client.Delete(guid);
        }
    }
}
