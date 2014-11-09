using FluentNHibernate.Automapping;
using System;

namespace ALPACA.Mappings
{
    public class AutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "ALPACA.Domain.Entities";
        }

        public override bool IsComponent(Type type)
        {
            return type.Namespace.EndsWith("Entities.Components");
        }
    }
}
