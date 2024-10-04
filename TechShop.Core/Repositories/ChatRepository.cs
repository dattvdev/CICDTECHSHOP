using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly TechshopDbContext _context;
        public ChatRepository(TechshopDbContext context)
        {
            _context = context;
        }
        public async Task<List<Chat>> GetChatRoomsByCollabatorId(Guid? collabatorId)
        {
            return await _context.Chats
                         .Where(ch => ch.CollaboratorId == collabatorId)
                         .Include(ch => ch.Customer)
                             .ThenInclude(cs => cs!.User)
                         .Include(ch => ch.Messages.OrderByDescending(m => m.CreateAt).Take(1))
                         .OrderByDescending(ch => ch.CreatedAt)
                         .ToListAsync();
        }
        public bool AddNewChat(Chat chat)
        {
            _context.Chats.Add(chat);
            _context.SaveChanges();
            return true;
        }
    }
}
