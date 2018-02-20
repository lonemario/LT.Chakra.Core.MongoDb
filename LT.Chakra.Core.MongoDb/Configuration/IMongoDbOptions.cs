namespace LT.Chakra.Core.MongoDb.Configuration
{
    public interface IMongoDbOptions
    {
        /// <summary>
        /// Mongo host and port list, comma separated: 
        /// Url example multi host  my-ip-address1:my-port1,my-ip-address2:my-port2, ecc."
        /// Url example single Host my-ip-address1:my-port1
        /// </summary>
        string MongoDbHostsUrl { get; set; }
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

        /// <summary>
        /// Indicates if the user is and user of local adminDb
        /// </summary>
        bool UserOfAdminDb { get; set; }

        /// <summary>
        /// Flag for use DNS SeedList
        /// </summary>
        bool UseDNSSeedlist { get; set; }

        /// <summary>
        /// Mongo Db Options
        /// </summary>
        string DbOptions { get; set; }

        /// <summary>
        /// If connection string is set the other parameters are ingnored
        /// </summary>
        string ConnectionString { get; set; }

    }
}
