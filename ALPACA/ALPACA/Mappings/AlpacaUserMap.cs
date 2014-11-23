using ALPACA.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;

namespace ALPACA.Mappings
{
    public class AlpacaUserMap : ClassMapping<AlpacaUser>
    {
        public AlpacaUserMap()
        {
            Property(x => x.Contacts, map =>
            {
                map.Type<NHibernateDelimitedList>();
            });
        }
    }
}
