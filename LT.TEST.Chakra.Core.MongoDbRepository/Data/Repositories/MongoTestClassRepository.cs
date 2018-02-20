using LT.Chakra.Core.MongoDb.Data.Repositories;
using LT.TEST.Chakra.Core.MongoDb.Data.Repositories;
using LT.TEST.Chakra.Core.MongoDb.Entities;
using LT.TEST.Chakra.Core.MongoDbRepository.Config;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories.Attributes;

namespace LT.TEST.Chakra.Core.MongoDbRepository.Data.Repositories
{
    [Repository]
    public class MongoTestClassRepository : MongoDbRepositoryBase<TestClass, DevMongoDbOptions>, ITestClassRepository
    {
        public MongoTestClassRepository(IDataSession dataSession)
            : base(dataSession) { }
    }
}
