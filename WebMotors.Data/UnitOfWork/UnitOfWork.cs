using WebMotors.Data.Context;
using WebMotors.Domain.Interfaces;

namespace WebMotors.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto _context;
        public UnitOfWork(Contexto contexto)
        {
            this._context = contexto;
        }

        public bool Commit()
        {
            return this._context.SaveChanges() > 0;
        }
    }
}
