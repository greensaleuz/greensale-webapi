using GreenSale.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Heat;

[Authorize(Roles = "SuperAdmin")]
public class SuperAdminBaseController : ControllerBase
{ }
