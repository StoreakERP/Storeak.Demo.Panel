using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreakApiService.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Storeak.Demo.Panel
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { set; get; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            SessionHandler _sessionHandler = new SessionHandler(new HttpContextAccessor());
            if (string.IsNullOrEmpty(_sessionHandler.Token) || _sessionHandler.UserProfile == null || string.IsNullOrEmpty(_sessionHandler.Language))
            {

                bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                if (isAjaxCall)
                {
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new JsonResult(403);
                }
                else
                {
                    context.Result = new RedirectResult("~/Account/Login");
                }
            }
            else
            {
                List<string> authorizedRoles = new List<string>();
                authorizedRoles.AddRange(Array.ConvertAll(Roles.Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), r => r.Trim()));
                bool isAuthorized = false;
                List<string> userRoles = _sessionHandler.UserProfile.Roles;
                isAuthorized = authorizedRoles.Any(x => userRoles.Any(y => x.ToLower() == y.ToLower()));
                if (!isAuthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }
    }
}