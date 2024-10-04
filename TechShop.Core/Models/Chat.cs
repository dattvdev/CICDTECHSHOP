using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Chat
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public Guid? CollaboratorId { get; set; }
        public Collaborator? Collaborator { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
