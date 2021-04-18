using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Core.Base_Classes;
using Core.Interfaces;

namespace Infrastructure.UnitOfWork
{
    public class TestUOW : UnitOfWork <TestUOW>
    {
        private readonly DataContext _context;
        public TestUOW(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
