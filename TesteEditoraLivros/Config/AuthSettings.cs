using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteEditoraLivros.Config
{
    //Essa classe tem o proposito de carregar os parametros de autenticação da lib de Autenticação do Dot Net
    public class AuthSettings
    {
        public static AuthenticationProperties AuthProperties { get => GetAuthProperties(); }

        private static AuthenticationProperties GetAuthProperties()
        {
            var authPropiets = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),// Mantem o cookie durante 15 minutos
                IsPersistent = true, // Deixo salvo no browser local para evitar logins no tempo de vida do cookie
            };

            return authPropiets;
        }
    }
}
