using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Data.Context;
using WebMotors.Data.Implementation.Base;
using WebMotors.Domain.Entities;
using WebMotors.Domain.Interfaces;

namespace WebMotors.Data.Implementation
{
    public class AnuncioRepository: RepositoryBase<Anuncio>, IAnuncioRepository
    {
        public AnuncioRepository(Contexto contexto): base(contexto)
        {
            
        }
    }
}
