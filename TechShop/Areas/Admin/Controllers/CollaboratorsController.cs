using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechShop.Areas.Admin.ViewModels;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CollaboratorsController : Controller
    {
        private readonly TechshopDbContext _context;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        public CollaboratorsController(TechshopDbContext context, IPasswordHasher<User> passwordHasher, ICollaboratorRepository collaboratorRepository)
        {
            _context = context;
            _collaboratorRepository = collaboratorRepository;
            _passwordHasher = passwordHasher;
        }

        // GET: Admin/Collaborators
        public async Task<IActionResult> Index()
        {
            var techshopDbContext = await _context.Collaborators.Include(c => c.User).ToListAsync();
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View(techshopDbContext);
        }

        // GET: Admin/Collaborators/GetCollaborator/5
        [HttpGet]
        public IActionResult GetCollaborator(Guid id)
        {
            var collaborator = _collaboratorRepository.FindCollaborator(id);

            if (collaborator == null)
            {
                return NotFound();
            }

            return Json(collaborator);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllCollaborator(DataTableParameters parameters)
        {
            await Task.Delay(200); 
            // Check DataTableParameters 
            if (parameters == null)
            {
                return BadRequest("Invalid parameters");
            }

            var collaborators = await _collaboratorRepository.GetDataCollaborators(parameters);

            if (collaborators == null)
            {
                return NotFound();
            }

            return Json(collaborators);
        }


        // GET: Admin/Collaborators/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboratorRespository = await _context.Collaborators
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collaboratorRespository == null)
            {
                return NotFound();
            }

            return View(collaboratorRespository);
        }

        // GET: Admin/Collaborators/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Collaborators/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,StartDate,EndDate")] Collaborator collaboratorRespository, User addUser)
        {
            if (_context.Users.Any(u => u.UserName == addUser.UserName))
            {
                ModelState.AddModelError("UserName", "UserName already exists.");
            }
            if (_context.Users.Any(u => u.Email == addUser.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }
            if (_context.Users.Any(u => u.PhoneNumber == addUser.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone Number already exists.");
            }

            var user = new User()
            {
                FullName = addUser.FullName,
                Address = addUser.Address,
                BirthDay = addUser.BirthDay,
                PhoneNumber = addUser.PhoneNumber,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserName = addUser.UserName,
                Email = addUser.Email
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, addUser.PasswordHash);

            if (!ModelState.IsValid || !TryValidateModel(user))
            {
                // Trả về lỗi validation dưới dạng JSON
                return Json(new
                {
                    success = false,
                    errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                )
                });
            }
            else
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                collaboratorRespository.UserID = user.Id;
                _collaboratorRepository.AddCollaborator(collaboratorRespository);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            
        }

        // GET: Admin/Collaborators/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboratorRespository = await _context.Collaborators.Include(c => c.User) // Include the related User
                                               .FirstOrDefaultAsync(m => m.ID == id);
            if (collaboratorRespository == null)
            {
                return NotFound();
            }
            return View(collaboratorRespository);
        }
            
        // POST: Admin/Collaborators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("UserID,StartDate,EndDate")] Collaborator collaboratorInput)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Trả về lỗi validation dưới dạng JSON
                return Json(new
                {
                    success = false,
                    errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                )
                });
            }
            else
            {
                try
                {

                    var collaborator = _collaboratorRepository.FindCollaborator(id);

                    if (collaborator == null)
                    {
                        return NotFound();
                    }

                    collaborator.StartDate = collaboratorInput.StartDate;
                    collaborator.EndDate = collaboratorInput.EndDate;

                    _collaboratorRepository.UpdateCollaborator(collaborator);
                    return Json(new { success = true ,
                        data = new
                        {
                            CollaboratorID = collaborator.ID
                        }
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaboratorExists(collaboratorInput.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            
            }
     

           

        }

        // GET: Admin/Collaborators/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborators
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(collaborator);
        }

        // POST: Admin/Collaborators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _collaboratorRepository.DeleteCollaborator(id);
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool CollaboratorExists(Guid? id)
        {
            return _context.Collaborators.Any(e => e.ID == id);
        }
    }
}
