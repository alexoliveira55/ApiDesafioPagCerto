using api.Infrastructure.Context;
using api.Models.EntityModel;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api.Repository
{
    public abstract class Repository<TEntityBase> : IRepository<TEntityBase> where TEntityBase : EntityBase, new()
    {
        protected readonly DbContextApi Db;
        protected readonly DbSet<TEntityBase> DbSet;

        public Repository(DbContextApi db)
        {
            Db = db;
            DbSet = db.Set<TEntityBase>();
        }
        public virtual async Task<IEnumerable<TEntityBase>> Consult(Expression<Func<TEntityBase, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task Add(TEntityBase entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }
        public virtual async Task Update(TEntityBase entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }
        public virtual async Task<List<TEntityBase>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task Delete(TEntityBase entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }
        public virtual async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            //a ? garente que não irá tentar executar o dispose se o objeto estiver nulo
            Db?.Dispose();
        }
    }
}
