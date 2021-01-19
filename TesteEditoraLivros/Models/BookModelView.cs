using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteEditoraLivros.Models
{
    public class BookModelView
    {
        public BookModelView(int id,string name, string publishingCompany, string author, string resume, string iSBNCode, DateTime publicationDate, string urlImage)
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
        public BookModelView()
        {

        }

        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Name { get; set; }

        [DisplayName("Editora")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string PublishingCompany { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Author { get; set; }

        [DisplayName("Sinopse")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Resume { get; set; }

        [DisplayName("ISBN")]
        public string ISBNCode { get; set; }

        [DisplayName("DataDePublicação")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime PublicationDate { get; set; }

        [DisplayName("ImagemUrl")]
        public string UrlImage { get; set; }


    }
}
