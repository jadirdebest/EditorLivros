using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TesteEditoraLivros.Application.DTO;
using TesteEditoraLivros.Application.Interface;
using TesteEditoraLivros.Models;

namespace TesteEditoraLivros.Controllers
{
    public class LoginController : LoginBaseController
    {
        private readonly IApplicationServiceAccount _serviceAccount;

        public LoginController(IApplicationServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }
       
        public IActionResult Logon(string ReturnUrl, bool denied = false)
        {
            //Eu faço uma verificação bem simples para saber se o usuário ja está autenticado e impedir que tente fazer logon 
            //desnecessário, aqui verifico também se houve alguma solitiação de usuário com mais previlégios e preencho uma viewbag pra
            //passar uma mensagem pra view e solicitar um login com mais acesso.
            if (User.Identity.IsAuthenticated && !denied) return RedirectToAction("Books", "Book");
            
            if (denied) ViewBag.Error = "Acesso Negado, é necessário um usuario com este previlégio";
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        public IActionResult DeniedLogon(string ReturnUrl)
        {
            return RedirectToAction("Logon", new { denied = true, ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logon(LoginModelView model,string ReturnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            ViewBag.ReturnUrl = ReturnUrl;

            //Esta condição criei apenas pra facilitar o login "admin" sem necessitar da base de dados
            if (model.Login.Equals("admin") && model.Password.Equals("admin"))
            {
                SignIn("admin", "Administrator");
                if(string.IsNullOrEmpty(ReturnUrl)) return RedirectToAction("Books", "Book");
                return Redirect(ReturnUrl);
            }
            //Fim - Pode ser comentado até aqui, não efeta o funcionamento

            //Esse método do serviço ele faz uma verificãção de usuário e senha no banco de dados
            //O usuário pode ser o e-mail ou nickName cadastrado, ambos irão ser autenticados normalmente
            var acessGranted = await _serviceAccount.LogonIsValid(new UserDTO(model.Login, model.Password));
            if (!acessGranted)
            {
                ModelState.AddModelError("Password", "Usuário ou senha incorretos");
                return View(model);
            }

            //Esse método é apenas pra capturar qual Profile(Role) que o usuário tem
            //Em condições normais de desenvolvimento, eu criaria uma classe contendo os dados de acesso e que irá conter um enum com o
            //tipo acessGranted ou algo pra identiticar que o o acesso foi permitido, caso nao use utilizasse framework como o Identity
            var roleProfile = await _serviceAccount.GetRoleProfile(model.Login);

            //Eu criei uma classe Abstrata herdado por essa classe que contém esse método, isso facilita bastante, quando é preciso utilizar mais de uma vez
            //o método, fora que deixa o código mais limpo
            SignIn(model.Login,roleProfile);
            
            if (string.IsNullOrEmpty(ReturnUrl)) return RedirectToAction("Books", "Book");
            return Redirect(ReturnUrl);
        }

        public IActionResult Logout()
        {
            SignOut();
            return RedirectToAction("Logon");
        }
    }
}
