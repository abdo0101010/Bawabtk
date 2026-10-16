using huzcodes.Persistence.Implementations.EfRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructre.Data
{
    public class BawabtkRepository<TEntity> : HuzcodesRepository<TEntity> where TEntity : class
    {
        public BawabtkRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
