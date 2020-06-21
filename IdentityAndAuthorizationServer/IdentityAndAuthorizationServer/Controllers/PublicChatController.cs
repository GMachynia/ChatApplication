using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using IdentityAndAuthorizationServer.Filters.ExceptionFilter;
using IdentityAndAuthorizationServer.RepositoriesInterfaces;

namespace IdentityAndAuthorizationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    public class PublicChatController : Controller
    {
        private readonly IPublicConversationRepostory repository;
        public PublicChatController(IPublicConversationRepostory repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("MessagesHistory")]
        public IActionResult GetHistoryMessages()
        {
            var messages = repository.GetMessages();
            return Ok(messages);
        }

    }

}