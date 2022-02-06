using System;

namespace Foundation
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
