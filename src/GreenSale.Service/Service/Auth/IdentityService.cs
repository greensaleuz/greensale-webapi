using GreenSale.Service.Interfaces.Auth;
using Microsoft.AspNetCore.Http;

namespace GreenSale.Service.Service.Auth;

public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _accessor;

    public IdentityService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public long Id
    {
        get
        {
            if (_accessor.HttpContext is null)
                return 0;

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Id");

            if (claim is null)
                return 0;
            else
                return long.Parse(claim.Value);
        }
    }

    public string RoleName
    {
        get
        {
            if (_accessor.HttpContext is null)
                return "";

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "RoleName");

            if (claim is null)
                return "";
            else
                return claim.Value;
        }
    }

    public string FirstName
    {
        get
        {
            if (_accessor.HttpContext is null)
                return "";

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "FirstName");

            if (claim is null)
                return "";
            else
                return claim.Value;
        }
    }

    public string LastName
    {
        get
        {
            if (_accessor.HttpContext is null)
                return "";

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "LatName");

            if (claim is null)
                return "";
            else
                return claim.Value;
        }
    }

    public string PhoneNumber
    {
        get
        {
            if (_accessor.HttpContext is null)
                return "";

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "PhoneNumber");

            if (claim is null)
                return "";
            else
                return claim.Value;
        }
    }
}
