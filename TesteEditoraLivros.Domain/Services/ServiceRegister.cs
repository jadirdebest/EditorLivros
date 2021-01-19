using System;
using System.Collections.Generic;
using System.Text;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Domain.Services
{
    public class ServiceRegister : ServiceBase<Register> , IServiceRegister
    {
        private readonly IRepositoryRegister _repository;
        public ServiceRegister(IRepositoryRegister repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
