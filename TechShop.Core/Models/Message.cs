using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid? ChatId { get; set; }
        public Chat? Chat { get; set; }
        public string? Text_message { get; set; }
        public DateTime? CreateAt { get; set; }
        public bool? IsCustomer { get; set; }
    }
}
