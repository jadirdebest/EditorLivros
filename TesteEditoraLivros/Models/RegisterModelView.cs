using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using TesteEditoraLivros.Application.DTO;

namespace TesteEditoraLivros.Models
{
    public class RegisterModelView
    {
        public RegisterModelView() { }
        public RegisterModelView(IEnumerable<RoleDTO> roleList){ RoleList = roleList; }
        public RegisterModelView(IEnumerable<RoleDTO> roleList,IEnumerable<RegisterDTO> registerList) 
        { RoleList = roleList; RegisterList = registerList; }

        public RegisterModelView(int id,int userId,string name,string nickName,string avatarUrl,int roleProfile, IEnumerable<RoleDTO> roleList)
        { Id = id;UserId = userId; Name = name;NickName = nickName;AvatarUrl = avatarUrl;RoleProfile = roleProfile; RoleList = roleList; }

        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Login")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("E-Mail")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [DisplayName("Avatar")]
        public string AvatarUrl { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Confirmar Senha")]
        [Compare(nameof(Password),ErrorMessage = "As senhas devem ser iguais")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Perfil de Acesso")]
        public int RoleProfile { get; set; }

       
        public IEnumerable<RoleDTO> RoleList { get; set; }
        public IEnumerable<RegisterDTO> RegisterList { get; set; }
    }
}
