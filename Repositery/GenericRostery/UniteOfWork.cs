using Data.DBContext;
using Infostructure.InterfaceGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositery.GenericRostery
{
    public class UniteOfWork<T> : IUniteOfWork<T> where T : class
    {
        private readonly DataContext context;

        private IGenericRepositery<T> _entity;
        public UniteOfWork(DataContext context)
        {
            this.context = context;
        }
        public IGenericRepositery<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepostery<T>(context));
            }
        }

        public bool save()
        {
            context.SaveChanges();
                return true;
        }
    }
}
