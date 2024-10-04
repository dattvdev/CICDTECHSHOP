using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public interface IChatRepository
    {
        public Task<List<Chat>> GetChatRoomsByCollabatorId(Guid? collabatorId);
        public bool AddNewChat(Chat chat);
    }
}
