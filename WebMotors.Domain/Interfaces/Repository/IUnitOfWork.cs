using System;

namespace WebMotors.Domain.Interfaces
{
    /// <summary>
    /// Interfacae de UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
