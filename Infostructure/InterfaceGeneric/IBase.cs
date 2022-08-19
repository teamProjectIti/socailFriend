using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.InterfaceGeneric
{
    public interface IBase
    {
        Task Add<T>(T entity) where T : class;
        void update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;


    }
}
