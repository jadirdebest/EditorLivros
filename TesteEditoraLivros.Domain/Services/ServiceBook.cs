using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Domain.Services
{
    public class ServiceBook : ServiceBase<Book>, IServiceBook
    {
        private readonly IRepositoryBook _repository;
        public ServiceBook(IRepositoryBook repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Book>> GetByNameOrAuthor(string keyWord)
        {
            return _repository.GetByNameOrAuthor(keyWord);
        }

        public Task<IEnumerable<Book>> GetByNameOrAuthorWithRangeDate(string keyWord, DateTime initialDate, DateTime finalDate)
        {
            if (initialDate.CompareTo(finalDate) > 0) throw new Exception("A data Inicial não deve ser maior que a Final");
            return _repository.GetByNameOrAuthorWithRangeDate(keyWord,initialDate,finalDate);
        }

        public Task<IEnumerable<Book>> GetByRangeDate(DateTime initialDate, DateTime finalDate)
        {
            if (initialDate.CompareTo(finalDate) > 0) throw new Exception("A data Inicial não deve ser maior que a Final");
            return _repository.GetByRangeDate(initialDate, finalDate);
        }
    }
}
