<<<<<<< HEAD
﻿using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories;
using POS_SYSTEM_MVC.Repositories.Base;
=======
﻿using POS_SYSTEM_MVC.Repositories;
>>>>>>> da50706294f0edc6b02741a19c15f78c8e9fc500
using POS_SYSTEM_MVC.Repositories.UnitRepo;

namespace POS_SYSTEM_MVC.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<SubCategory> SubCategories { get; }
        IBaseRepository<Brand> Brands { get; }

        IUnitRepository Units { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
