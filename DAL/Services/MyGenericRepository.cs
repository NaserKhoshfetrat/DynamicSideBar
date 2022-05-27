using DAL.Context;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class MyGenericRepository<TEntity> : IMyGenericRepository<TEntity> where TEntity : class
    {
        protected TestDBContext _context;
        protected DbSet<TEntity> _dbSet;

        public MyGenericRepository(TestDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditionWhere = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includesRelatioship = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (conditionWhere != null)
            {
                query = query.Where(conditionWhere);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includesRelatioship != null)
            {
                foreach (var include in includesRelatioship)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            Delete(entity);
            
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}