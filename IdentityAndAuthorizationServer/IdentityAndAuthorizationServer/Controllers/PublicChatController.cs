using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAndAuthorizationServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using IdentityAndAuthorizationServer.Filters.ExceptionFilter;

namespace IdentityAndAuthorizationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ExceptionHandler))]
    public class PublicChatController : Controller
    {
        private readonly PublicConversationRepository repository;
        public PublicChatController(PublicConversationRepository repository)
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