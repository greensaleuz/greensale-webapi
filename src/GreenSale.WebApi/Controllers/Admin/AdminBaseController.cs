using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin;

[Authorize(Roles = "admin, SuperAdmin")]
public class AdminBaseController : ControllerBase
{ }