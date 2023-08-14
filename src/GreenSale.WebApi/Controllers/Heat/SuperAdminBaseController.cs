using GreenSale.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Authorization;

namespace GreenSale.WebApi.Controllers.Heat;

[Authorize(Roles = "SuperAdmin")]
public class SuperAdminBaseController : BaseController
{ }
