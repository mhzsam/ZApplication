using Application.DTO.ResponseModel;
using Application.Helper;
using Application.Service.ResponseService;
using Domain.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.MiddleWare
{
    public static class PermissionControlMiddelware
    {
        public static IApplicationBuilder UsePermissionCheck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionCheck>();
        }


    }

    internal class PermissionCheck
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public PermissionCheck(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task Invoke(HttpContext httpContext)
        {


            var controllerName = httpContext.Request.RouteValues["controller"]?.ToString() + "Controller";
            var actionName = httpContext.Request.RouteValues["action"]?.ToString();
            var methodName = httpContext.Request.Method;
            var user = httpContext.User.Claims.FirstOrDefault(x=>x.Type== "userId");





            ApplicationDBContext _context = httpContext.RequestServices.GetService<ApplicationDBContext>();
            IResponseService responseService = httpContext.RequestServices.GetService<IResponseService>();
            var userId = httpContext.User.Claims.FirstOrDefault(x => x.Type == "userId").Value;



            var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(120))
                                                       .SetSlidingExpiration(TimeSpan.FromMinutes(60));



            if (userId == null)
            {
                var model = responseService.Fail(System.Net.HttpStatusCode.Unauthorized, ErrorText.Unauthorized);
                var json = JsonSerializer.Serialize(model);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync(json);
                return;
            }

            await _next(httpContext);
        }
    }
}
