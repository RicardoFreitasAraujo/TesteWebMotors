using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Domain.Entities.Base;
using WebMotors.Domain.Helpers;

namespace WebMotors.Domain.Interfaces.Services
{
    public interface IServiceBase<T> where T: EntidadeBase
    {
        RetornoBackEnd Adicionar(T entidade);
        RetornoBackEnd Atualizar(T entidade);
        RetornoBackEnd Excluir(int id);
        RetornoBackEnd LocalizarPorId(int id);
        RetornoBackEnd RetornarTodos();
        bool Valido(T entidade);
    }
}
