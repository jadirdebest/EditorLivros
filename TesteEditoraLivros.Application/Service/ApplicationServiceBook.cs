using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Application.DTO;
using TesteEditoraLivros.Application.Interface;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Application.Service
{
    //Uma Classe de serviço comun contendo um crud básico, além dos específicos que criei pra filtrar os livros
    public class ApplicationServiceBook : IApplicationServiceBook
    {
        private readonly IServiceBook _service;
        private readonly IMapper _mapper;
        public ApplicationServiceBook(IServiceBook service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public void Add(BookDTO obj)
        {
            _service.Add(_mapper.Map<Book>(obj));
        }

        public async Task<IEnumerable<BookDTO>> GetAll()
        {
            var list = await _service.GetAll();
            return _mapper.Map<IEnumerable<BookDTO>>(list);
        }

        public async Task<BookDTO> GetById(int id)
        {
            var book = await _service.GetById(id);
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<IEnumerable<BookDTO>> GetByNameOrAuthor(string keyWord)
        {
            var bookList = await _service.GetByNameOrAuthor(keyWord);
            return _mapper.Map<IEnumerable<BookDTO>>(bookList);
        }

        public async Task<IEnumerable<BookDTO>> GetByNameOrAuthorWithRangeDate(string keyWord, DateTime initialDate, DateTime finalDate)
        {
            var bookList = await _service.GetByNameOrAuthorWithRangeDate(keyWord,initialDate, finalDate);
            return _mapper.Map<IEnumerable<BookDTO>>(bookList);
        }

        public async Task<IEnumerable<BookDTO>> GetByRangeDate(DateTime initialDate, DateTime finalDate)
        {
            var bookList = await _service.GetByRangeDate(initialDate,finalDate);
            return _mapper.Map<IEnumerable<BookDTO>>(bookList);
        }

        public void Remove(BookDTO obj)
        {
            _service.Remove(_mapper.Map<Book>(obj));
        }

        public void Update(BookDTO obj)
        {
            _service.Update(_mapper.Map<Book>(obj));
        }
    }
}
