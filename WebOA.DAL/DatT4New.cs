
using WebOA.IDAL;
using WebOA.Model;

namespace WebOA.DAL
{
	public partial class UserInfoDal : BaseDal<UserInfo>,IUserInfoDal
	{}
	public partial class RoleInfoDal : BaseDal<RoleInfo>,IRoleInfoDal
	{}
	public partial class ActionInfoDal : BaseDal<ActionInfo>,IActionInfoDal
	{}
	public partial class UserActionDal : BaseDal<UserAction>,IUserActionDal
	{}
}