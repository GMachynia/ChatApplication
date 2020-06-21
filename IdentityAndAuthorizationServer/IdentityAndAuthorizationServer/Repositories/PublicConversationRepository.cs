using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAndAuthorizationServer.Models;
using IdentityAndAuthorizationServer.RepositoriesInterfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityAndAuthorizationServer.Repositories
{
    public class PublicConversationRepository:IPublicConversationRepostory
    {
        private AuthenticationContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public PublicConversationRepository(AuthenticationContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IEnumerable<Object> GetMessages()
        {
            return context.PublicChatMessages.Select(x => new
            {
                Date = x.Date,
                Sender = userManager.FindByIdAsync(x.UserId).Result.UserName,
                Message = x.Message
            });
         
        }
        public async Task AddMessageAsync(PublicChatMessage message)
        {
            await context.PublicChatMessages.AddAsync(message);
            await context.SaveChangesAsync();
        }
    }
}
