using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteEditoraLivros.Application.DTO;

namespace TesteEditoraLivros.Models
{
    public class BooksModelView
    {
        public BooksModelView(IEnumerable<BookDTO> bookList)
        {
            BookList = bookList;
        }

        public BooksModelView()
        {

        }

        public IEnumerable<BookDTO> BookList { get; set; }

        public string KeyWord { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public string OrderName { get; set; } = "A"; //Coloquei um valor padrão inicial
    }
}
