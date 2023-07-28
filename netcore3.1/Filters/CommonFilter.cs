using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace netcore3._1.Filters
{
    public class CommonExpFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
    public class CommonAuthFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new System.NotImplementedException();
        }       
    }


    public class CommonActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CommonResouceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CommonResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new System.NotImplementedException();
        }
    }


    public class CustomServiceFactory : IFilterFactory
    {
        public bool IsReusable => true;
        private Type _type;
        public CustomServiceFactory(Type type)
        {
            this._type = type;
        }



        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(_type) as IFilterMetadata;
        }
    }
}
