using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Domain.Models
{
    public class User : Base
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

    }
}
