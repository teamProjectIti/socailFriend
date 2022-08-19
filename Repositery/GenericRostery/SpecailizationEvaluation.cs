using Infostructure.Specailization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositery.GenericRostery
{
    public class SpecailizationEvaluation<TEntity> where TEntity:class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecailization<TEntity> spec)
        {
            //IQueryable<TEntity> query == context.set<T>(parmater)
            IQueryable<TEntity> query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            //funcation aggregate make loop and add all item include has model relationship 
            query = spec.Includes.Aggregate(query, (a, b) => a.Include(b));
            return query;
        }
    }
}
