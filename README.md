# LT.Chakra.Core.MongoDb 
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/LT.Chakra.Core.MongoDb)

Provider for MongoDb in Chakra.Core framework

## Prerequisites

### .NETStandard 2.0
```
Chakra.Core (>= 2.0.7)
LT.PluralizeEn (>= 1.0.1)
MongoDB.Driver (>= 2.5.0)
```


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
