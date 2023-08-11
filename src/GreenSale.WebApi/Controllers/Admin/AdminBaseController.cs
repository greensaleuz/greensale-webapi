using GreenSale.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminBaseController : BaseController
{}