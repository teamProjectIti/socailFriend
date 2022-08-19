using Data.DBContext;
using Infostructure.InterfaceGeneric;
using Infostructure.Specailization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositery.GenericRostery
{
    public class GenericRepostery<T> : IGenericRepositery<T> where T : class
    {
        private readonly DataContext context;

        //constructor init
        public GenericRepostery(DataContext context)
        {
            this.context = context;
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }



        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> Get_Entity_With_Include(ISpecailization<T> spec)
        {
            return await ApplaySpecaction(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> Get_LisT_With_Include(ISpecailization<T> spec)
        {
            return await ApplaySpecaction(spec).ToListAsync();
        }

        private IQueryable<T> ApplaySpecaction(ISpecailization<T> specailization)
        {
            return SpecailizationEvaluation<T>.GetQuery(context.Set<T>().AsQueryable(), specailization);
        }


    }
}
