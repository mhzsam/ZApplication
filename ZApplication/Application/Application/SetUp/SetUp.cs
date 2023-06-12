using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SetUp
{
    public static class SetUp
    {
        public static void AddApplicationDBContext(this IServiceCollection services ,string connectionString)
        {
            services.AddDbContext<ApplicationDBContext> (options =>
            {
                options.UseSqlServer(connectionString);
            });
        
        }
    }
}
