using GreenSale.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Authorization;

namespace GreenSale.WebApi.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminBaseController : BaseController
{ }