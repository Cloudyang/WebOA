
using WebOA.Model;

namespace WebOA.IService
{
	public partial interface IUserInfoService : IService<UserInfo>
	{}
	public partial interface IRoleInfoService : IService<RoleInfo>
	{}
	public partial interface IActionInfoService : IService<ActionInfo>
	{}
	public partial interface IUserActionService : IService<UserAction>
	{}
}