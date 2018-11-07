using Autofac;
using System.Linq;
using System.Reflection;

namespace ATNB.Web.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("ATNB.Service"))
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}