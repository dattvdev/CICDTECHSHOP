using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Core.Repositories
{
    public interface ICollaboratorRepository
    {
        IList<Collaborator> GetAllCollaborators();
        void AddCollaborator(Collaborator collaborator);
        void UpdateCollaborator(Collaborator collaborator);
        void DeleteCollaborator(Guid id);
        Collaborator FindCollaborator(Guid? id);
        Collaborator GetCollaboratorByUserId(string userId);
        public Task<object> GetDataCollaborators(DataTableParameters parameters);
        Task<int> Count();
        Task<Collaborator> GetCollaboratorByUserIdAsync(string userId);
    }
}
