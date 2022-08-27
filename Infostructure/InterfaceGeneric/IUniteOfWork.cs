using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.InterfaceGeneric
{
    public interface IUniteOfWork<T> where T : class
    {
        IGenericRepositery<T> Entity { get; }

        bool save();

    }
}
