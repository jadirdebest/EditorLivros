using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Domain.Models
{
    public class Register
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }

    }
}
