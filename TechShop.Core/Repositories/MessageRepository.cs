using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly TechshopDbContext _context;
        public MessageRepository(TechshopDbContext context)
        {
            _context = context;
        }
        public async Task<List<Message>> ListMessagesByChatId(Guid? id)
        {
            return await _context.Messages.Where(msg => msg.ChatId == id).OrderBy(m => m.CreateAt).ToListAsync();
        }

        public bool AddNewMessage(Message message) { 
            _context.Messages.Add(message);
            _context.SaveChanges();
            return true;
        }
    }
}
