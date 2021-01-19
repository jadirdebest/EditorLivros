using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Application.DTO
{
    public class UserDTO : BaseDTO
    {
        public UserDTO(string email,string password)
        {
            Email = email;
            Password = password;

        }
        public UserDTO()
        {

        }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
