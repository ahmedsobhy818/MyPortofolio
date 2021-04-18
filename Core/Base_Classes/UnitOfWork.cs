using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Base_Classes
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DbContext _context;
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
