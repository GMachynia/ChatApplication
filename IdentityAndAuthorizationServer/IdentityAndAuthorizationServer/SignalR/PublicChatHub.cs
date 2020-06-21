using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using IdentityAndAuthorizationServer.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityAndAuthorizationServer.Repositories;
using IdentityAndAuthorizationServer.RepositoriesInterfaces;

namespace IdentityAndAuthorizationServer.SignalR
{
    public class PublicChatHub:Hub
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPublicConversationRepostory repository;

        public PublicChatHub(IPublicConversationRepostory repository, UserManager<ApplicationUser> userManager):base()
        {
            this.repository = repository;
            this.userManager = userManager;
        }
    
        public async Task NewMessage(ChatMessageModel msg)
        {
            var user = await userManager.FindByNameAsync(msg.Sender);

            var publicChatMessage = new PublicChatMessage()
            {
                Date = msg.Date,
                Message = msg.Message,
                UserId = user.Id,
                
                
            };
            await repository.AddMessageAsync(publicChatMessage);
            
            await Clients.All.SendAsync("MessageReceived", msg);
        }



    }
}


