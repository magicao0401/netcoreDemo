using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace netcore3._1
{
    internal class JwtTokenAuth
    {

        RequestDelegate _next;

        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            //PreProceed(httpContext);


            if(!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                //PostProceed(httpContext);

              await _next(httpContext);
            }

            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var claimList = new List<Claim>();

                var claim = new Claim(ClaimTypes.Role, "Admins");
                claimList.Add(claim);
                var identity = new ClaimsIdentity(claimList);
                var pricipal = new ClaimsPrincipal(identity);
                httpContext.User = pricipal;

            }
            catch (System.Exception e)
            {

                Console.WriteLine($"{DateTime.Now} - middleware wrong: {e.Message}");
            }

            //PostProceed(httpContext) ;

            await _next(httpContext) ;
        }
    }
}