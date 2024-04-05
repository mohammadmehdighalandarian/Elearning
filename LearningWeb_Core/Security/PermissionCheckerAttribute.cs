using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWeb_Core.Security
{
    public class PermissionCheckerAttribute: AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermitionServices _permissionService;
        private long _permissionId = 0;

        public PermissionCheckerAttribute(long permissionId)
        {
            _permissionId = permissionId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService =
                (IPermitionServices)context.HttpContext.RequestServices.GetService(typeof(IPermitionServices));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;

                if (!_permissionService.CheckPermission(_permissionId, userName))
                {
                    context.Result = new RedirectResult("./Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("./Login");
            }
        }
    }
}
