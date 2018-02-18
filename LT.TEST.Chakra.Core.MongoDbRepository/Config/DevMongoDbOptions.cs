using LT.Chakra.Core.MongoDb.Configuration;

namespace LT.TEST.Chakra.Core.MongoDbRepository.Config
{
    public class DevMongoDbOptions : IMongoDbOptions
    {
        /// <summary>
        /// Mongo Db Connection String Url ex.  mongodb://my-ip-address:my-port"
        /// </summary>
        public string MongoDbUrl { get { return "mongodb://192.168.44.132:27017"; } set { } }
        /// <summary>
        /// Mongo Db Database Name
        /// </summary>
        public string MongoDbName { get { return "TestDb"; } set { } }
        /// <summary>
        /// Mongo Db User Name
        /// </summary>
        public string MongoDbUser { get; set; }
        /// <summary>
        /// Mongo Db Password
        /// </summary>
        public string MongoDbPassword { get; set; }
    }
}
