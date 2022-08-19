using Autofac;
using Infostructure.InterfaceGeneric;
using Repositery.GenericRostery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AutoFacConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);


            #region Generic
            builder.RegisterGeneric(typeof(GenericRepostery<>)).As(typeof(IGenericRepositery<>));
            #endregion

        }
    }
}
