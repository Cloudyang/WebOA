
using WebOA.IDAL;
using WebOA.DAL;

namespace WebOA.DalFactory
{
	public partial class DbSession : IDbSession
	{
		  public IUserInfoDal GetUserInfoDal()
		  {
				return new UserInfoDal();
		  }
		  public IRoleInfoDal GetRoleInfoDal()
		  {
				return new RoleInfoDal();
		  }
		  public IActionInfoDal GetActionInfoDal()
		  {
				return new ActionInfoDal();
		  }
		  public IUserActionDal GetUserActionDal()
		  {
				return new UserActionDal();
		  }
	}
}