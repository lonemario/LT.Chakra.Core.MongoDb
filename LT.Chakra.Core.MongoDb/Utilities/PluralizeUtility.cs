using LT.PluralizeEn;
using System;

namespace LT.Chakra.Core.MongoDb.Utilities
{
    public class PluralizeUtility
    {
        Pluralizer pluralize;

        public PluralizeUtility()
        {
            pluralize = new Pluralizer();
        }

        public string Pluralize(Type entityType)
        {            
            return pluralize.Pluralize(entityType.Name);
        }

        public string Pluralize(string collectionName)
        {
            return pluralize.Pluralize(collectionName);
        }
    }
}
