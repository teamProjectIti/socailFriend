using Infostructure.Specailization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.InterfaceGeneric
{
    public interface IGenericRepositery<T>:IBase where T : class
    {
        Task<T> GetByIDAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        //get one item obj with include
        Task<T> Get_Entity_With_Include(ISpecailization<T> spec);
        //get list obj with include
        Task<IReadOnlyList<T>> Get_LisT_With_Include(ISpecailization<T> spec);


    }

 }
