﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public interface IMessageRepository
    {
        Task<List<Message>> ListMessagesByChatId(Guid? id);
        bool AddNewMessage(Message message);
    }
}
