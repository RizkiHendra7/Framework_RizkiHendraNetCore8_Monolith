
using FrameWorkRHP_Mono.Core.CommonFunction;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Services.ServicesImplement.GenericServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomAuthorizeFilter :IAuthorizationFilter
{
    //public void OnActionExecuting(ActionExecutingContext context)
    //{
    //    //if there is no session whitch key is "register", user will not access to specified action and redirect to login page.

    //    var result = SessionService.dtLogin;
    //    if (result == null)
    //    {
    //        context.Result = new RedirectToActionResult("Login", "Login", null);
    //    }
    //}

    //public void OnActionExecuted(ActionExecutedContext context)
    //{

    //}
    public ISessionService _sessionService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CustomAuthorizeFilter(IHttpContextAccessor httpContextAccessor,ISessionService SessionService)
    {
        _sessionService = SessionService;
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {

        var token = _httpContextAccessor.HttpContext.Session.GetString(clsGlobalConstant.SessionNameKey);
         
        if (token == null)
        {
            _sessionService.SignOutAsync();
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
}

public class CustomAuthorize : TypeFilterAttribute
{
    public CustomAuthorize() : base(typeof(CustomAuthorizeFilter))
    {
    }
}
