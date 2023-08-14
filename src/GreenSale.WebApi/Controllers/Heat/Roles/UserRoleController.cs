using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Heat.Roles
{
    [Route("api/heat/superadmin/user/roles")]
    [ApiController]
    public class UserRoleController : SuperAdminBaseController
    {
        public UserRoleController()
        {
            
        }
    }
}
