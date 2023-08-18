using GreenSale.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Authorization;

namespace GreenSale.WebApi.Controllers.Client
{
    [Authorize(Roles = "User")]
    public class BaseClientController : BaseController
    { }
}
