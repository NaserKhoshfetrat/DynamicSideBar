using DAL.Services;
using DataModel.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class UnitOfWork : IDisposable
    {
        private IMemoryCache _memoryCache;
        public UnitOfWork(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        private TestDBContext _db = new TestDBContext();

        #region TblCategory
        private CategoryRepository _categoryRepository;
        public CategoryRepository TblCategory
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_db, _memoryCache);
                }
                return _categoryRepository;
            }
        }
        #endregion

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}