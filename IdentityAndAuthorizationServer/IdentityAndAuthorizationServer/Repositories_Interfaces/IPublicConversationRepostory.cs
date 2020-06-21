using IdentityAndAuthorizationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.RepositoriesInterfaces
{
    public interface IPublicConversationRepostory
    {
        IEnumerable<Object> GetMessages();
        Task AddMessageAsync(PublicChatMessage message);
    }
}
