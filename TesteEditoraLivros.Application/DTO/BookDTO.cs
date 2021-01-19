using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Application.DTO
{
    public class BookDTO : BaseDTO
    {
        public BookDTO(int id,string name, string publishingCompany, 
            string author, string resume, string iSBNCode, DateTime publicationDate, string urlImage)
        {
            Id = id;
            Name = name;
            PublishingCompany = publishingCompany;
            Author = author;
            Resume = resume;
            ISBNCode = iSBNCode;
            PublicationDate = publicationDate;
            UrlImage = urlImage;
        }

        public BookDTO(string name, string publishingCompany, 
            string author, string resume, string iSBNCode, DateTime publicationDate, string urlImage)
        {
            Name = name;
            PublishingCompany = publishingCompany;
            Author = author;
            Resume = resume;
            ISBNCode = iSBNCode;
            PublicationDate = publicationDate;
            UrlImage = urlImage;
        }

        public BookDTO()
        {

        }
        public string Name { get; set; }
        public string PublishingCompany { get; set; }
        public string Author { get; set; }
        public string Resume { get; set; }
        public string ISBNCode { get; set; }
        public DateTime PublicationDate { get; set; }
        public string UrlImage { get; set; }
    }
}
