using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebOA.IService
{
    public partial interface IRoleInfoService
    {
        bool SetAction(int rid, string aids);
    }
}
