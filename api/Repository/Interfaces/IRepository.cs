using api.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api.Repository.Interfaces
{
    public interface IRepository<TEntityBase> : IDisposable where TEntityBase : EntityBase
    {
        Task Add(TEntityBase entidade);
        Task<List<TEntityBase>> GetAll();
        Task Update(TEntityBase entity);
        Task Delete(TEntityBase entity);
        Task<IEnumerable<TEntityBase>> Consult(Expression<Func<TEntityBase, bool>> predicate);
        Task<int> SaveChanges();
    }
}
