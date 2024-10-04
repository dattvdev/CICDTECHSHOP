using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.AreaCollaborator.Controllers
{
    [Authorize(Roles = "Collaborator")]
    [Area("AreaCollaborator")]
    public class HomeController : Controller
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly UserManager<User> _userManager;
        public HomeController(IChatRepository chatRepository, IMessageRepository messageRepository, UserManager<User> userManager, ICollaboratorRepository collaboratorRepository)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _userManager = userManager;
            _collaboratorRepository = collaboratorRepository;
        }
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                Collaborator collaborator = _collaboratorRepository.GetCollaboratorByUserId(user.Id);
                if(collaborator != null)
                {
                    List<Chat> chats = await _chatRepository.GetChatRoomsByCollabatorId(collaborator.ID);
                    return View(chats);
                } else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> MessageInRooms(string roomId)
        {
            List<Message> messages = await _messageRepository.ListMessagesByChatId(Guid.Parse(roomId));
            return Json(messages);
        }
    }
}
