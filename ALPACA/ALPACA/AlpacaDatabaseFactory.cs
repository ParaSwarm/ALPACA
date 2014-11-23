using ALPACA.Mappings;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.AspNet.Identity;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System.Collections.Generic;
using System.Linq;

namespace ALPACA
{
    public static class AlpacaDatabaseFactory
    {
        private static ISessionFactory _sessionFactory;
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    _sessionFactory = CreateSessionFactory();

                return _sessionFactory;
            }
        }

        public static ISessionFactory CreateSessionFactory()
        {
            var automapConfiguration = new AutoMappingConfiguration();
            return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("ALPACA")))
                            .Mappings(m => m.AutoMappings
                                    .Add(AutoMap.AssemblyOf<AutoMappingConfiguration>(automapConfiguration)
                                    .Conventions.Setup(c => c.Add<CustomForeignKeyConvention>())))
                            .ExposeConfiguration(config =>
                            {
                                config.AddDeserializedMapping(NhibernateAspNetMapper.MapStuff(), null);
                                //var schemaExport = new SchemaExport(config);
                                //schemaExport.Create(true, true);
                            })
                    .BuildSessionFactory();
        }
    }


    public static class NhibernateAspNetMapper
    {
        public static HbmMapping MapStuff()
        {
            var baseEntityToIgnore = new[] { 
                typeof(NHibernate.AspNet.Identity.DomainModel.EntityWithTypedId<int>), 
                typeof(NHibernate.AspNet.Identity.DomainModel.EntityWithTypedId<string>), 
            };

            var allEntities = new List<System.Type> { 
                typeof(IdentityUser), 
                typeof(IdentityRole), 
                typeof(IdentityUserLogin), 
                typeof(IdentityUserClaim),
                typeof(AlpacaUser)
            };

            var mapper = new ConventionModelMapper();
            DefineBaseClass(mapper, baseEntityToIgnore.ToArray());
            mapper.IsComponent((type, declared) => typeof(NHibernate.AspNet.Identity.DomainModel.ValueObject).IsAssignableFrom(type));

            mapper.AddMapping<IdentityUserMap>();
            mapper.AddMapping<IdentityRoleMap>();
            mapper.AddMapping<IdentityUserClaimMap>();
            mapper.AddMapping<IdentityUserClaimMap>();
            mapper.AddMapping<AlpacaUserMap>();
            
            return mapper.CompileMappingFor(allEntities);
        }

        private static void DefineBaseClass(ConventionModelMapper mapper, System.Type[] baseEntityToIgnore)
        {
            if (baseEntityToIgnore == null) return;
            mapper.IsEntity((type, declared) =>
                baseEntityToIgnore.Any(x => x.IsAssignableFrom(type)) &&
                !baseEntityToIgnore.Any(x => x == type) &&
                !type.IsInterface);
            mapper.IsRootEntity((type, declared) => baseEntityToIgnore.Any(x => x == type.BaseType));
        }
    }

}
