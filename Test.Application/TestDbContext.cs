using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Test.Application
{
    public static class TestDbContext
    {
        public static IApplicationDbContext CreateContext()
        {
            

            return new ApplicationDbContext(options);
        }
    }
}
