using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Domain.Core.Interfaces.Services
{
    public interface IServiceUser : IServiceBase<User>
    {
        Task<User> AddWithReturn(User obj);
        Task<User> GetByUserOrEmail(string wordIdentifier);
    }
}
