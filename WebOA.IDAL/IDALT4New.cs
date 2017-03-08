using WebOA.Model;

namespace WebOA.IDAL
{
	public partial interface IUserInfoDal : IBaseDal<UserInfo>
	{}
	public partial interface IRoleInfoDal : IBaseDal<RoleInfo>
	{}
	public partial interface IActionInfoDal : IBaseDal<ActionInfo>
	{}
	public partial interface IUserActionDal : IBaseDal<UserAction>
	{}
}