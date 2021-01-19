using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Domain.Models
{
    public class Book : Base
    {
        public string Name { get; set; }
        public string PublishingCompany { get; set; }
        public string Author { get; set; }
        public string Resume { get; set; }
        public string ISBNCode { get; set; }
        public DateTime PublicationDate  { get; set; }
        public string UrlImage { get; set; }
    }
}
