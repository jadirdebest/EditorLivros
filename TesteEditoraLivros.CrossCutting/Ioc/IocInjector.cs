using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TesteEditoraLivros.Application.Interface;
using TesteEditoraLivros.Application.Service;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;
using TesteEditoraLivros.Domain.Services;
using TesteEditoraLivros.Infrastructure.Data.Repositories;

namespace TesteEditoraLivros.CrossCutting.Ioc
{
    //Essa é a unica camada da infra que faz comuniação com a apresentação, pois é daqui que precisa partir as injeções que irão ser 
    //utilizados na aplicação 
    public class IocInjector
    {
        //utilizo sempre que possível esse padrão, assim posso centralizar todas as minhas dependencias, caso eventualmente queira
        //acrescentar mais serviços ou mudar o tempo de vida da minha instância
        public static void Register(IServiceCollection svcColletion)
        {
            //Application
            svcColletion.AddSingleton<IApplicationServiceBook, ApplicationServiceBook>();
            svcColletion.AddSingleton<IApplicationServiceMD5, ApplicationServiceMD5>();
            svcColletion.AddSingleton<IApplicationServiceAccount, ApplicationServiceAccount>();
            

            //Domain e Infra
            svcColletion.AddSingleton<IServiceBook, ServiceBook>();
            svcColletion.AddSingleton<IServiceRegister, ServiceRegister>();
            svcColletion.AddSingleton<IServiceUser, ServiceUser>();
            svcColletion.AddSingleton<IServiceRole, ServiceRole >();
            //Domain e Infra
            svcColletion.AddSingleton<IRepositoryUser, RepositoryUser>();
            svcColletion.AddSingleton<IRepositoryRegister, RepositoryRegister>();
            svcColletion.AddSingleton<IRepositoryBook, RepositoryBook>();
            svcColletion.AddSingleton<IRepositoryRole, RepositoryRole>();
        }
    }
}
