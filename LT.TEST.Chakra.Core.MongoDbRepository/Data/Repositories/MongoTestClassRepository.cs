using LT.Chakra.Core.MongoDb.Data.Repositories;
using LT.TEST.Chakra.Core.MongoDb.Data.Repositories;
using LT.TEST.Chakra.Core.MongoDb.Entities;
using LT.TEST.Chakra.Core.MongoDbRepository.Config;
using System;
using System.Linq;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories.Attributes;

namespace LT.TEST.Chakra.Core.MongoDbRepository.Data.Repositories
{
    [Repository]
    public class MongoTestClassRepository : MongoDbRepositoryBase<TestClass, DevMongoDbOptions>, ITestClassRepository
    {
        public MongoTestClassRepository(IDataSession dataSession)
            : base(dataSession) { }

        public TestClass GetTestClassByProperty1(int? property1)
        {
            //Validazione argomenti
            if (property1==null) throw new ArgumentNullException(nameof(property1));

            //Soluzione 1 usando lo scenario
            //return Scenario.Users.SingleOrDefault(
            //    u => u.UserName.ToLower() == userName.ToLower());

            //Soluzione 2 usando l'alias
            //return Collection.SingleOrDefault(
            //    u => u.Property1 == property1);

            return Collection.FirstOrDefault(
                    u => u.Property1 == property1);
        }
    }
}
