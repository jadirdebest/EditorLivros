using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Domain.Core.Interfaces.Services
{
    public interface IServiceBook : IServiceBase<Book>
    {
        Task<IEnumerable<Book>> GetByNameOrAuthor(string keyWord);
        Task<IEnumerable<Book>> GetByRangeDate(DateTime initialDate, DateTime finalDate);
        Task<IEnumerable<Book>> GetByNameOrAuthorWithRangeDate(string keyWord, DateTime initialDate, DateTime finalDate);
    }
}
