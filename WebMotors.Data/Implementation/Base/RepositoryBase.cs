using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMotors.Data.Context;
using WebMotors.Domain.Entities.Base;
using WebMotors.Domain.Interfaces;

namespace WebMotors.Data.Implementation.Base
{
    public abstract class RepositoryBase<TEntidade> : IRepositoryBase<TEntidade> where TEntidade : EntidadeBase
    {
        private readonly Contexto _contexto;
        private readonly DbSet<TEntidade> _dbset;

        public RepositoryBase(Contexto contexto)
        {
            this._contexto = contexto;
            this._dbset = contexto.Set<TEntidade>();
        }

        public TEntidade Adicionar(TEntidade entidade)
        {
            this._dbset.Add(entidade);
            return entidade;
        }

        public TEntidade Atualizar(TEntidade entidade)
        {
            this._dbset.Update(entidade);
            return entidade;
        }

        public void Dispose()
        {
            this._contexto.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool Excluir(int id)
        {
            TEntidade obj = this.LocalizarPorId(id);
            if (obj != null)
            {
                this._dbset.Remove(obj);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Existe(int id)
        {
            return this._dbset.Any(et => et.Id == id);
        }

        public TEntidade LocalizarPorId(int id)
        {
            return this._dbset.Where(et => et.Id ==  id).FirstOrDefault();
        }

        public List<TEntidade> RetornarTodos()
        {
            return this._dbset.ToList();
        }
    }
}
