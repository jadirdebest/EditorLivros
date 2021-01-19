using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Application.DTO;

namespace TesteEditoraLivros.Application.Interface
{
    public interface IApplicationServiceBook
    {
        void Add(BookDTO obj);
        Task<BookDTO> GetById(int id);
        Task<IEnumerable<BookDTO>> GetAll();
        Task<IEnumerable<BookDTO>> GetByNameOrAuthor(string keyWord);
        Task<IEnumerable<BookDTO>> GetByRangeDate(DateTime initialDate, DateTime finalDate);
        Task<IEnumerable<BookDTO>> GetByNameOrAuthorWithRangeDate(string keyWord,DateTime initialDate, DateTime finalDate);
        void Update(BookDTO obj);
        void Remove(BookDTO obj);
    }
}
