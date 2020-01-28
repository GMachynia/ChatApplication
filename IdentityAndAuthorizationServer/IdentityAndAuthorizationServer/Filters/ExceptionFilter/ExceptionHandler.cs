using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.Filters.ExceptionFilter
{
    public class ExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<ExceptionHandler> logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.logger = logger;

        }
        public void OnException(ExceptionContext context)
        {
            if(context.Exception != null)
            {
                logger.LogError("Catched exeption: {err}", context.Exception.Message);
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
        }
    }
}
