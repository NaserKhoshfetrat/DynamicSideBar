using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IMyGenericRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditionWhere = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includesRelatioship = null);
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Save();
        void Update(TEntity entity);
    }
}