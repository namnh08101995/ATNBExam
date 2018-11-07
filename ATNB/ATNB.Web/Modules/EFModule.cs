using ATNB.Model;
using ATNB.Repository;
using ATNB.Repository.Abstractions;
using Autofac;
using System.Data.Entity;

namespace ATNB.Web.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(ATNBContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
        }
    }
}