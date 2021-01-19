using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Application.DTO
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool Active { get; set; }
    }
}
