using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Domain.Models
{
    //Criei essa class como um "padrão" pra eu seguir com os atributos mais usados nas outras classes, com isso ganho
    //tempo e mantenho uma referência
    public abstract class Base
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool Active { get; set; }
    }
}
