# LT.Chakra.Core.MongoDb 
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/LT.Chakra.Core.MongoDb)

Provider for MongoDb in [Chakra.Core Framework](https://www.nuget.org/packages/Chakra.Core/)

## Prerequisites

### .NETStandard 2.0
```
Chakra.Core (>= 2.0.7)
LT.PluralizeEn (>= 1.0.1)
MongoDB.Driver (>= 2.5.0)
```

### Example 

Startup project class

```c#
class Program
{
    static void Main(string[] args)
    {
        SessionFactory.RegisterDefaultDataSession<MongoDbDataSession<DevMongoDbOptions>>();

        //INITIALIZE SESSION DOMAIN
        using (var DataSession = SessionFactory.OpenSession())
        {
            using (var Layer = new MainServiceLayer(DataSession))
            {
                //ADD FIRST 500 COLLECTIONS
                MongoTestClassRepository _repo = new MongoTestClassRepository(new MongoDbDataSession());
                do
                {
                    var tmpEntity = new Entities.TestClass
                    {
                        Property1 = RandomGen.GenericInt(10000),
                        Property2 = RandomGen.GenericString(250, 20),
                        Property3 = RandomGen.GenericDate(),
                        Property4 = null,
                        Property5 = RandomGen.GenericNullableDate(60),
                        Property6 = null
                    };

                    Layer.SaveTest(ref tmpEntity);

                    Task.Delay(5000).Wait();
                
                } while (Layer.Count()<500);

                // GET SINGLE ENTITY
                var resS = Layer.GetSingle("de3318bd-d5db-4f1e-8130-cfed77039e93");

                //FETCH ALL
                var resF = Layer.Fetch();

                //FETCH WITH FILTER
                var respFF = Layer.Fetch(c => c.Property1 == 1);

                //COUNT THE ENTITIES
                var resC = Layer.Count();

                //DELETE AN ENTITY
                Layer.Delete(resS);
            }

        }

        Console.ReadKey();
    }
}
```

Db Options class

```c#
    public class DevMongoDbOptions : MongoDbOptions
    {
        //Class for Mongo Db Connection, it's possible set the connection string or
        //the singles parameters
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

            //SERVER AND PORT
            MongoDbHostsUrl = "XXX.XXX.XXX.XXX:XXXX";
            //Mongo User Name (Optional)
            MongoDbUser = "test";
            //Mongo Password (Optional)
            MongoDbPassword = "test";
            //Mongo Db Name
            MongoDbName = "TestDb";
            //If user is in AdminDb (Optional)
            UserOfAdminDb = true;
            //UseDNSSeedlist = true;
        }
    }
```
