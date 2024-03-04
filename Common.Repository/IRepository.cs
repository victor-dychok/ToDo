using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        TEntity[] GetList(
            int? offset = null,
            int? limit = null, 
            Expression<Func<TEntity, bool>>? predicate = null, 
            Expression<Func<TEntity, object>>? orderBy = null,
            bool? destinct = null);
        TEntity? SingleOrDefault(Expression<Func<TEntity, bool>>? predicate = null);
        TEntity? Add(TEntity item);
        TEntity? Update(TEntity item);
        bool Delete(TEntity item);
    }
}
