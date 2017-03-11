using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebOA.DAL
{
    public partial class RoleInfoDal : IDAL.IRoleInfoDal
    {
        public void SetAction(int rid, int[] aids)
        {
            var roleInfo = GetById(rid);
            roleInfo.ActionInfo.Clear();
            ActionInfoDal actionInfoDal = new ActionInfoDal();
            foreach (var aid in aids)
            {
                roleInfo.ActionInfo.Add(actionInfoDal.GetById(aid));
            }
        }
    }
}
