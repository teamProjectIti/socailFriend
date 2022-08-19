using Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            // connectio sql
            services.AddDbContext<DataContext>(x =>
                  x.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection")));

                  return services;
        }
    }
}
