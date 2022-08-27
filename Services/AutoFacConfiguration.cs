using Autofac;
using Infostructure.ILike;
using Infostructure.InterfaceGeneric;
using Infostructure.IUser;
using Infostructure.IUser.IServicesToken;
using Repositery.GenericRostery;
using Repositery.likesRepositery;
using Repositery.Userrepositery;
using Repositery.Userrepositery.IServicesToken;
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

            #region UniteOfWork
            builder.RegisterGeneric(typeof(UniteOfWork<>)).As(typeof(IUniteOfWork<>));
            #endregion
            #region UserRepositery
            builder.RegisterType<UserRepositry>().As<IUserRepositery>();
            #endregion
            #region TokenServices
            builder.RegisterType<TokenServices>().As<ITokenServices>();
            #endregion 
            #region TokenServices
            builder.RegisterType<likeRepositery>().As<IlikeRepositery>();
            #endregion
            





        }
    }
}
