using GreenSale.WebApi.Controllers.Common;
using GreenSale.WebApi.Controllers.Heat;
using Microsoft.AspNetCore.Authorization;

namespace GreenSale.WebApi.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminBaseController : SuperAdminBaseController
{ }