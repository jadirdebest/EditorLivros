using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteEditoraLivros.Models
{
    public class LoginModelView
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Campo Obrigatório")]
        //[Compare(nameof(Password),ErrorMessage = "As senhas devem ser iguais")]
        //public string ConfirmPassword { get; set; }
    }
}
