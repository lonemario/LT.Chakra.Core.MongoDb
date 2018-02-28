using LT.TEST.Chakra.Core.MongoDb.Entities;
using ZenProgramming.Chakra.Core.Data.Repositories;

namespace LT.TEST.Chakra.Core.MongoDb.Data.Repositories
{
    public interface ITestClassRepository : IRepository<TestClass>
    {
        /// <summary>
        /// Get single user by user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Returns user or null</returns>
        TestClass GetTestClassByProperty1(int? property1);
    }
}
