using Autofac;

namespace Praxent.Data
{
    public static class Bootstrapper
    {
        public static ContainerBuilder AddBasicDataComponents(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(InMemoryRepository<>)).As(typeof(IRepository<>));
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            return builder;
        }
    }
}