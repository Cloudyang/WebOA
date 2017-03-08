
using WebOA.IService;
using WebOA.IDAL;
using WebOA.Model;

namespace WebOA.Service
{
    public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        protected override IBaseDal<UserInfo> GetDal()
        {
            return dbSession.GetUserInfoDal();
        }
	}
    public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
        protected override IBaseDal<RoleInfo> GetDal()
        {
            return dbSession.GetRoleInfoDal();
        }
	}
    public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
        protected override IBaseDal<ActionInfo> GetDal()
        {
            return dbSession.GetActionInfoDal();
        }
	}
    public partial class UserActionService:BaseService<UserAction>,IUserActionService
    {
        protected override IBaseDal<UserAction> GetDal()
        {
            return dbSession.GetUserActionDal();
        }
	}
}