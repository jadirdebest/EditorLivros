using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Application.DTO;
using TesteEditoraLivros.Application.Interface;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Application.Service
{
    /* Esse serviço foi criado para obter melhor controle sobre as entidades User e Register, pois estão ligadas diretamente,
     * como não utilizei uma framework específica para controle de acesso de usuários , eu optei por ter essa "camada" de Contas, pois 
     * assim qualquer Aplicação que eu possa utilizar na minha Apresentação vai estar pronto pra ser utilizado com as regras
     * e fluxos bem definidos, além que tenho um controle muito maior do que está sendo persistido.
     */

    public class ApplicationServiceAccount : IApplicationServiceAccount
    {
        private readonly IServiceUser _serviceUser;
        private readonly IServiceRole _serviceRole;
        private readonly IServiceRegister _serviceRegister;
        private readonly IApplicationServiceMD5 _md5Service;
        private readonly IMapper _mapper;
        public ApplicationServiceAccount
            (
                IServiceUser serviceUser,
                IServiceRole serviceRole,
                IServiceRegister serviceRegister,
                IApplicationServiceMD5 md5Service,
                IMapper mapper
            )
        {
            _serviceUser = serviceUser;
            _serviceRole = serviceRole;
            _serviceRegister = serviceRegister;
            _md5Service = md5Service;
            _mapper = mapper;
        }
        
        // Dessa maneira assim que o usuário cria uma Conta ele automaticamente ja 
        //cria um usuario na tabela responsavel pelo usuario e outra na tabela de dados do usuario
        //isso mantém a padronização e fluxo do sistema corretamente 
        public async void CreateAccount(AccountDTO account)
        {
            var userDto = new UserDTO()
            {
                RoleId = account.RoleId,
                Email = account.Email,
                Password = _md5Service.CreateMD5(account.Password)
            };
           
            var user = await _serviceUser.AddWithReturn(_mapper.Map<User>(userDto));
           
            var registerDto = new RegisterDTO()
            {
                AvatarUrl = account.AvatarUrl,
                Name = account.Name,
                NickName = account.NickName,
                UserId = user.Id
            };

            _serviceRegister.Add(_mapper.Map<Register>(registerDto));
        }

        public void DeleteAccount(AccountDTO account)
        {
            _serviceRegister.Remove(_mapper.Map<Register>(new RegisterDTO() { Id = account.Id }));
            _serviceUser.Remove(_mapper.Map<User>(new UserDTO() { Id = account.UserId }));
        }

        public async Task<RegisterDTO> GetAccountById(int id)
        {
            var register = await _serviceRegister.GetById(id);
            return _mapper.Map<RegisterDTO>(register);
        }

        public async Task<IEnumerable<RegisterDTO>> GetAllAccounts()
        {
            var listRegister = await _serviceRegister.GetAll();
            return _mapper.Map<IEnumerable<RegisterDTO>>(listRegister);
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            var roleList = await _serviceRole.GetAll();
            return _mapper.Map<IEnumerable<RoleDTO>>(roleList);
        }

        public async Task<string> GetRoleNameById(int id)
        {
            var roleResponse = await _serviceRole.GetById(id);
            return roleResponse.Name;
        }

        //Poderia trazer qualquer informação sobre O perfil de acesso, porém optei por deixar apenas o nome
        public async Task<string> GetRoleProfile(string nickName)
        {
            var userResponse = await _serviceUser.GetByUserOrEmail(nickName);
            var roleResponse = await _serviceRole.GetById(userResponse.RoleId);
            return roleResponse.Name;
        }

        
        //A validação podia ser feito diretamente no banco,porém apliquei dessa maneira
        //pois caso precise mudar o sistema de criptograria aqui será melhor essa mudança
        public async Task<bool> LogonIsValid(UserDTO user)
        {
            var userResponse = await _serviceUser.GetByUserOrEmail(user.Email);
            if (userResponse == null) return false;
            return _md5Service.MD5IsMatch(user.Password, userResponse.Password);
        }

        public void UpdateAccount(AccountDTO account)
        {
            var registerDto = new RegisterDTO()
            {
                Id = account.Id,
                AvatarUrl = account.AvatarUrl,
                Name = account.Name,
                NickName = account.NickName,
            };

            var userDto = new UserDTO()
            {
                Id = account.UserId,
                RoleId = account.RoleId,
            };

            _serviceRegister.Update(_mapper.Map<Register>(registerDto));
            _serviceUser.Update(_mapper.Map<User>(userDto));


        }
    }
}
