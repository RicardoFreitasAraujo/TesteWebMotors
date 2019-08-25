using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Application.Services.Base;
using WebMotors.Domain.Entities;
using WebMotors.Domain.Interfaces;
using WebMotors.Domain.Interfaces.Services;

namespace WebMotors.Application.Services
{
    public class AnuncioService: ServiceBase<Anuncio>, IAnuncioService
    {
        public AnuncioService(IAnuncioRepository repo, IUnitOfWork uow): base(repo, uow)
        {
        }
    }
}
