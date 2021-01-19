using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TesteEditoraLivros.Config;

namespace TesteEditoraLivros.Controllers
{
    public abstract class LoginBaseController : Controller
    {
        //Esse métoodo ele criar uma lista de Claim, usei apenas os dois básicos para conseguir utilizar as autorizações de acesso
        //que possui no controller, ele cria um cookie no browser contendo algumas informações e a aplicação consegue identiticar
        //se o usuário esta autenticado, tempo de expiração do cookie, entre outros
        protected async void SignIn(string Name,string Role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Name),
                new Claim(ClaimTypes.Role, Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await base.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), AuthSettings.AuthProperties);
        }
        //Apenas faz o logout da aplicação
        protected async void SignOut()
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}