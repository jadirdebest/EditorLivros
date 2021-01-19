using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Application.DTO
{
    public class AccountDTO : RegisterDTO
    {
        public AccountDTO(int roleId,string email, string password,string avatarUrl,string name,string nickName)
        {RoleId = roleId;Email = email;Password = password;AvatarUrl = avatarUrl;Name = name;NickName = nickName;}
        public AccountDTO(int id,int roleId,int userId, string email, string password, string avatarUrl, string name, string nickName)
        {Id = id; RoleId = roleId; UserId = userId; Email = email; Password = password; AvatarUrl = avatarUrl; Name = name; NickName = nickName; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
