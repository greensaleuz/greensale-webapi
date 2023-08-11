using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Common
{
    [AllowAnonymous]
    public class BaseController : ControllerBase
    {}
}
