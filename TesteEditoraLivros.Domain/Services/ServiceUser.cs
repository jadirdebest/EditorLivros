using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Domain.Services
{
    public class ServiceUser : ServiceBase<User> , IServiceUser
    {
        private readonly IRepositoryUser _repository;
        public ServiceUser(IRepositoryUser repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<User> AddWithReturn(User obj)
        {
            return _repository.AddWithReturn(obj);
        }

        public Task<User> GetByUserOrEmail(string wordIdentifier)
        {
            return _repository.GetByUserOrEmail(wordIdentifier);
        }
    }
}
