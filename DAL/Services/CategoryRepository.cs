using DAL.Context;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DAL.Services
{
    public class CategoryRepository : MyGenericRepository<TblCategory>
    {
        private IMemoryCache _cache;
        private readonly string _tag = "CategoryRepository";
        public CategoryRepository(TestDBContext context, IMemoryCache cache) : base(context)
        {
            _cache = cache;
        }

        public List<TblCategory> GetAll()
        {
            var cache = _cache.Get<List<TblCategory>>(_tag);
            if (cache != null)
            {
                return cache;
            }
            else
            {
                //fetch from db
                var data = _context.TblCategories
                     .FromSqlRaw("EXECUTE dbo.spoc_readAllCategory")
                     .ToList();
                //cache key in memory-cache
                var cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(_tag, data, cacheOption);

                return data;
            }
        }

        public void RemoveCache()
        {
            _cache.Remove(_tag);
        }
    }
}