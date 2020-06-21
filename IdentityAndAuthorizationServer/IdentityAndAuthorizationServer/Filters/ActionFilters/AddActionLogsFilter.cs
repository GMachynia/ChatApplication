using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.Filters.ActionFilters
{
    public class AddActionLogsFilter : IActionFilter
    {
        private readonly ILogger<AddActionLogsFilter> logger;
        public AddActionLogsFilter(ILogger<AddActionLogsFilter> logger)
        {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("After executing action {action} with parameters {@parameters}",context.ActionDescriptor.DisplayName, context.ActionDescriptor.Parameters);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Before executing action {action} with parameters {@parameters}", context.ActionDescriptor.DisplayName, context.ActionDescriptor.Parameters);
        }
    }
}
