using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Application.DTO;

namespace TesteEditoraLivros.Application.Interface
{
    public interface IApplicationServiceAccount
    {
        void CreateAccount(AccountDTO account);
        void UpdateAccount(AccountDTO account);
        void DeleteAccount(AccountDTO account);
        Task<bool> LogonIsValid(UserDTO user);
        Task<string> GetRoleProfile(string nickName);
        Task<IEnumerable<RoleDTO>> GetAllRoles();
        Task<IEnumerable<RegisterDTO>> GetAllAccounts();
        Task<RegisterDTO> GetAccountById(int id);
        Task<string> GetRoleNameById(int id);
    }
}
