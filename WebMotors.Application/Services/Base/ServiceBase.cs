using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Domain.Entities.Base;
using WebMotors.Domain.Helpers;
using WebMotors.Domain.Interfaces;
using WebMotors.Domain.Interfaces.Services;

namespace WebMotors.Application.Services.Base
{
    public abstract class ServiceBase<TEntidade> : IServiceBase<TEntidade> where TEntidade : EntidadeBase
    {

        private readonly IRepositoryBase<TEntidade> _repo;
        private readonly IUnitOfWork _uow;

        public ServiceBase(IRepositoryBase<TEntidade> repo, IUnitOfWork uow)
        {
            this._repo = repo;
            this._uow = uow;
        }


        public RetornoBackEnd Adicionar(TEntidade entidade)
        {
            bool valido = this.Valido(entidade);

            if (valido)
            {
                try
                {
                    var entidadeObj = this._repo.Adicionar(entidade);
                    this._uow.Commit();
                    return new RetornoBackEnd(entidadeObj);
                }
                catch (Exception ex)
                {
                    return new RetornoBackEnd(ex);
                }

            }
            else
            {
                return new RetornoBackEnd(false, entidade.ErrosValidacoes, entidade.ErrosValidacoes);
            }

        }

        public RetornoBackEnd Atualizar(TEntidade entidade)
        {
            bool valido = this.Valido(entidade);

            if (valido)
            {
                try
                {
                    var entidadeObj = this._repo.Atualizar(entidade);
                    this._uow.Commit();
                    return new RetornoBackEnd(entidadeObj);
                }
                catch (Exception ex)
                {
                    return new RetornoBackEnd(ex);
                }

            }
            else
            {
                return new RetornoBackEnd(false, entidade.ErrosValidacoes, entidade.ErrosValidacoes);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public RetornoBackEnd Excluir(int id)
        {
            try
            {
                bool retorno = this._repo.Excluir(id);
                this._uow.Commit();
                return new RetornoBackEnd(true);
            }
            catch (Exception ex)
            {
                return new RetornoBackEnd(ex);
            }
        }

        public RetornoBackEnd Existe(int id)
        {
            try
            {
                bool retorno = this._repo.Existe(id);
                return new RetornoBackEnd(retorno);
            }
            catch (Exception ex)
            {
                return new RetornoBackEnd(ex);
            }
        }

        public RetornoBackEnd LocalizarPorId(int id)
        {
            try
            {
                var dados = this._repo.LocalizarPorId(id);
                return new RetornoBackEnd(dados);
            }
            catch (Exception ex)
            {
                return new RetornoBackEnd(ex);
            }
        }

        public RetornoBackEnd RetornarTodos()
        {
            try
            {
                var dados = this._repo.RetornarTodos();
                return new RetornoBackEnd(dados);
            }
            catch (Exception ex)
            {
                return new RetornoBackEnd(ex);
            }
        }

        public bool Valido(TEntidade entidade)
        {
            return entidade.Validar();
        }

    }
}
