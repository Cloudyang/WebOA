using WebOA.Model;

namespace WebOA.IDAL
{
	public partial interface IDbSession
	{
		IUserInfoDal  GetUserInfoDal();
		IRoleInfoDal  GetRoleInfoDal();
		IActionInfoDal  GetActionInfoDal();
		IUserActionDal  GetUserActionDal();
	}
}