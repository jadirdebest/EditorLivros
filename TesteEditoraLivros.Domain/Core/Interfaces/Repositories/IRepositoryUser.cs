using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        Task<User> AddWithReturn(User obj);
        Task<User> GetByUserOrEmail(string wordIdentifier);
    }
}
