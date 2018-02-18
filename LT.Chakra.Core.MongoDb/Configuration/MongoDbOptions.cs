namespace LT.Chakra.Core.MongoDb.Configuration
{
    public class MongoDbOptions : IMongoDbOptions
    {
        /// <summary>
        /// Mongo Db Connection String Url ex.  mongodb://my-ip-address:my-port"
        /// </summary>
        public virtual string MongoDbUrl { get; set; }
        /// <summary>
        /// Mongo Db Database Name
        /// </summary>
        public virtual string MongoDbName { get; set; }
        /// <summary>
        /// Mongo Db User Name
        /// </summary>
        public virtual string MongoDbUser { get; set; }
        /// <summary>
        /// Mongo Db Password
        /// </summary>
        public virtual string MongoDbPassword { get; set; }

    }
}
