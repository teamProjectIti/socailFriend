using Infostructure.Specailization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositery.SpecailizationRepostery
{
        public class BaseSpecailization<T> : ISpecailization<T> where T : class
        {
            public BaseSpecailization()
            {
            }
            public Expression<Func<T, bool>> Criteria { get; }

            // property type list 
            // default empty not null begin
            public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

            public BaseSpecailization(Expression<Func<T, bool>> criteria)
            {
                Criteria = criteria;
            }

            protected void AddInclude(Expression<Func<T, Object>> IncludeExpression)
            {
                Includes.Add(IncludeExpression);
            }

        }
}
