using Microsoft.Extensions.DependencyInjection;
using System;
using WebMotors.Application.Services;
using WebMotors.Data.Context;
using WebMotors.Data.Implementation;
using WebMotors.Data.Implementation.Base;
using WebMotors.Data.UnitOfWork;
using WebMotors.Domain.Interfaces;
using WebMotors.Domain.Interfaces.Services;

namespace WebMotors.DI
{
    /// <summary>
    /// Classe Responsável por realizar Injeções de Dependências do .NET Core
    /// </summary>
    public static class Injetor
    {

        private static IServiceCollection _services;
        
        /* Classe Injetora deverá ser estática
        public Injetor(IServiceCollection services)
        {
            this._services = services;
        }*/

        public static void InjetarDependecias(IServiceCollection services)
        {
            _services = services;
            InjetarRepositorios();
            InjetarServicos();
        }

        /// <summary>
        /// Injetar Classes de Repositórios
        /// </summary>
        private static  void InjetarRepositorios()
        {
            _services.AddScoped<Contexto>();
            _services.AddScoped<IUnitOfWork, UnitOfWork>();
            //_services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>)); //Não injeta classe abstrata
            _services.AddScoped<IAnuncioRepository, AnuncioRepository>();
        }

        /// <summary>
        /// Injetar Classes de Serviços
        /// </summary>
        private static void InjetarServicos()
        {
            //_services.AddScoped(typeof(IServiceBase<>), typeof(IServiceBase<>)); //Não injeta classe abstrata
            _services.AddScoped<IAnuncioService, AnuncioService>();
        }
    }
}
