using ALPACA.Domain.Entities;
using ALPACA.Mappings;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace ALPACA
{
    public static class AlpacaDatabaseFactory
    {
        public static ISessionFactory CreateSessionFactory()
        {
            var automapConfiguration = new AutoMappingConfiguration();
            return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("ALPACA")))
                            .Mappings(m => m.AutoMappings
                            .Add(AutoMap.AssemblyOf<AutoMappingConfiguration>(automapConfiguration)
                                .Conventions.Setup(c => c.Add<CustomForeignKeyConvention>())
                                .UseOverridesFromAssemblyOf<AutoMappingConfiguration>))
                    .BuildSessionFactory();
        }
    }
}
