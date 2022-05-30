using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SAMAirline.DataProvider.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class, new()
    {
        private readonly AirlineDB _context;
        public BaseRepository(AirlineDB context)
        {
            _context = context;
        }

        protected DbSet<T> DbSet
        {
            get
            {
                return _context.Set<T>();
            }
        }

        protected AirlineDB Context
        {
            get
            {
                return _context;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public PagedList<T> GetPagedList(IEnumerable<T> source, int pageNumber, int pageSize, bool isAdmin)
        {
            var count = source.Count();
            var pagesCount = 0;
            string message = null;

            if (count != 0)
                if (count < pageSize)
                    pagesCount = 1;
                else if (count % pageSize == 0)
                    pagesCount = count / pageSize;
                else
                    pagesCount = count / pageSize + 1;
            else
                message = "There are no items";
            if (isAdmin)
                return new PagedList<T>
                {
                    RowsCount = count,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    PagesCount = pagesCount,
                    Data = source.Skip(pageNumber * pageSize).Take(pageSize).ToList(),
                    Message = message
                };
            else
                return new PagedList<T>
                {
                    RowsCount = count,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    PagesCount = pagesCount,
                    Data = source.Take(pageNumber * pageSize).ToList(),
                    Message = message
                };
        }

        public bool IsExists(int id)
        {
            var entity = DbSet.Find(id);

            if (entity == null)
            {
                throw new ApplicationException($"Can't find item with id = {id}");
            }

            return true;
        }
    }
}
