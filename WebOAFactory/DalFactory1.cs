using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebOA.DAL;
using WebOA.IDAL;

namespace WebOA.DalFactory
{
  public partial  class DalFactory1
    {
        //简单工厂
        public static IUserInfoDal GetUserInfoDal()
        {
            return new UserInfoDal();
        }

        //抽象工厂
        public static IUserInfoDal GetUserInfoDal2()
        {
            //从配置文件中读取类
            string constr=System.Configuration.ConfigurationManager.AppSettings["UserInfoDal"];
            string assemblyName = constr.Split(',')[1];
            string className = constr.Split(',')[0];
            
            //1、获取程序集对象
            Assembly assembly = Assembly.Load(assemblyName);

            //2、创建对象实例
            return  assembly.CreateInstance(className) as IUserInfoDal;
        }
    }
}
