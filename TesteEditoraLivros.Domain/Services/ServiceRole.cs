using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Domain.Services
{
    public class ServiceRole : ServiceBase<Role>, IServiceRole
    {
        private readonly IRepositoryRole _repository;
        public ServiceRole(IRepositoryRole repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
