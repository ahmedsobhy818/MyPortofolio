using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Base_Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> table = null;
        public GenericRepository(DbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }
        public void Delete(object id)
        {
            T existing = GetById(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return table;
        }

        public T GetById(object id)
        {
            return table.Find(id); //dbset.find search by the pk
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            //table.Update(entity);  works

            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            
        }
    }
}
