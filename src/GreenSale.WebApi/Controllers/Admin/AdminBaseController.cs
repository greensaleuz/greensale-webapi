using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminBaseController : ControllerBase
{ }