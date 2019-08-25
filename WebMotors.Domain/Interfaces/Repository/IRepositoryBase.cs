using System;
using System.Collections.Generic;
using WebMotors.Domain.Entities.Base;

namespace WebMotors.Domain.Interfaces
{
    /// <summary>
    /// Interface Base de Repositório
    /// </summary>
    public interface IRepositoryBase<T>: IDisposable where T : EntidadeBase
    {
        T Adicionar(T entidade);
        T Atualizar(T entidade);
        bool Excluir(int id);
        T LocalizarPorId(int id);
        List<T> RetornarTodos();
        bool Existe(int id);
    }
}
