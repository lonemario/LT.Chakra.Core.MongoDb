using LT.Chakra.Core.MongoDb.Configuration;

namespace LT.TEST.Chakra.Core.MongoDbRepository.Config
{
    public class DevMongoDbOptions : MongoDbOptions
    {
        public DevMongoDbOptions()
        {
            //mongodb://router1.example.com:27017,router2.example2.com:27017,router3.example3.com:27017/
            //mongodb://example1.com,example2.com,example3.com/?replicaSet=test&w=2&wtimeoutMS=2000
            //mongodb://localhost,localhost:27018,localhost:27019/?replicaSet=test
            //mongodb://db1.example.net,db2.example.com/?replicaSet=test
            //mongodb://sysop:moon@localhost/records
            //mongodb://db1.example.net:27017,db2.example.net:2500/?replicaSet=test&connectTimeoutMS=300000
            //mongodb+srv://server.example.com/
            //mongodb://db1.example.net:27017,db2.example.net:2500/?replicaSet=test

            //ConnectionString = "mongysop:moon@localhost/records";

            MongoDbHostsUrl = "192.168.44.132:27017";
            MongoDbUser = "test";
            MongoDbPassword = "test";
            MongoDbName = "TestDb";
            UserOfAdminDb = true;
            //UseDNSSeedlist = true;
        }
    }
}
