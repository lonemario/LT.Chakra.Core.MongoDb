namespace LT.Chakra.Core.MongoDb.Configuration
{
    public interface IMongoDbOptions
    {
        /// <summary>
        /// Mongo Db Connection String Url ex.  mongodb://my-ip-address:my-port"
        /// </summary>
        string MongoDbUrl { get; set; }
        /// <summary>
        /// Mongo Db Database Name
        /// </summary>
        string MongoDbName { get; set; }
        /// <summary>
        /// Mongo Db User Name
        /// </summary>
        string MongoDbUser { get; set; }
        /// <summary>
        /// Mongo Db Password
        /// </summary>
        string MongoDbPassword { get; set; }
    }
}
