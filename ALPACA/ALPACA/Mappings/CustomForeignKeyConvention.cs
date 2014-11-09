using FluentNHibernate;
using FluentNHibernate.Conventions;
using System;

namespace ALPACA.Mappings
{
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
            {
                return type.Name + "Id";
            }

            return property.Name + "Id";
        }
    }
}
