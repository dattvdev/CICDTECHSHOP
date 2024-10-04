using Microsoft.AspNetCore.SignalR;
using TechShop.Core.Repositories;
using TechShop.Core.Models;

namespace TechShop.Hubs
{
	public class ChatHub : Hub
	{
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        public ChatHub(IChatRepository chatRepository, IMessageRepository messageRepository, ICustomerRepository customerRepository, IUserRepository userRepository, ICollaboratorRepository collaboratorRepository)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _collaboratorRepository = collaboratorRepository;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public async Task StartConversation(string username)
        {
            await Clients.Group("Collaborators").SendAsync("NewCustomer", username, "Has started a conversation", DateTime.Now.ToString("dd/MM/yyyy HH:mm"), Guid.NewGuid());
        }
        public async Task AddCollaboratorToGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Collaborators");
        }
        public async Task AddCustomerToGroup(string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
        }
        public async Task JoinRoomChat(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }
        public async Task Accepted(string username, string message, string collabName, string roomId)
        {
            Chat newChat = new Chat();
            User userCustomer = await _userRepository.FindUserByUsername(username);
            User userCollab = await _userRepository.FindUserByUsername(collabName);
            Customer customer = await _customerRepository.FindCustomerByUserId(Guid.Parse(userCustomer.Id));
            Collaborator collaborator = await _collaboratorRepository.GetCollaboratorByUserIdAsync(userCollab.Id);
            newChat.Id = Guid.Parse(roomId);
            newChat.CollaboratorId = collaborator.ID;
            newChat.CustomerId = customer.ID;
            newChat.CreatedAt = DateTime.Now;
            Console.WriteLine(collaborator.ID);
            Console.WriteLine(customer.ID);
            _chatRepository.AddNewChat(newChat);

            Message newMessage = new Message();
            newMessage.ChatId = newChat.Id;
            newMessage.Text_message = message;
            newMessage.CreateAt = DateTime.Now;
            _messageRepository.AddNewMessage(newMessage);

            await Clients.OthersInGroup("Collaborators").SendAsync("ChatAccepted", roomId);
            await Clients.Group(username).SendAsync("ChatAccepted", message, roomId);
        }

        public async Task SendMessage(string roomId, string message, bool isCustomer)
        {
            Message newMessage = new Message();
            newMessage.ChatId = Guid.Parse(roomId);
            newMessage.Text_message = message;
            newMessage.IsCustomer = isCustomer;
            newMessage.CreateAt = DateTime.Now;
            _messageRepository.AddNewMessage(newMessage);
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveMessage", message);
        }
        public async Task RemoveUserFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
