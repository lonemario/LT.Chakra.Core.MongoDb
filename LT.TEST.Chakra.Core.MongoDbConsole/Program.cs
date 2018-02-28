using LT.Chakra.Core.MongoDb.Data;
using LT.TEST.Chakra.Core.MongoDb.ServiceLeyers;
using LT.RandomGen;
using ZenProgramming.Chakra.Core.Data;
using LT.TEST.Chakra.Core.MongoDbRepository.Config;

namespace LT.TEST.Chakra.Core.MongoDbConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mongo new DevMongoDbOptions { }
            
            SessionFactory.RegisterDefaultDataSession<MongoDbDataSession<DevMongoDbOptions>>();

            //Inizializzo la session e il dominio
            using (var DataSession = SessionFactory.OpenSession())
            {

                //var a = DataSession.ResolveRepository<ITestClassRepository>();

                using (var Layer = new MainServiceLayer(DataSession))
                {
                    //Console.WriteLine("Hello World!");
                    //MongoTestClassRepository _repo = new MongoTestClassRepository(new MongoDbDataSession());
                    //do
                    //{
                    //    var tmpEntity = new Entities.TestClass
                    //    {
                    //        Property1 = RandomGen.GenericInt(10000),
                    //        Property2 = RandomGen.GenericString(250, 20),
                    //        Property3 = RandomGen.GenericDate(),
                    //        Property4 = null,
                    //        Property5 = RandomGen.GenericNullableDate(60),
                    //        Property6 = null
                    //    };

                    //    Layer.SaveTest(ref tmpEntity);

                    //    Task.Delay(5000).Wait();

                    //} while (true);
                    //var resString = RandomGenerator.GenericOnlyAlfaNumericString(43, 0, false);
                    //// RICERCA SINGOLA ENTITà    

                    ////Fetch
                    var respF = Layer.Fetch();

                    //var respFF = Layer.Fetch(c => c.Property1 == 1);

                    //var resS = Layer.GetSingle("de3318bd-d5db-4f1e-8130-cfed77039e93");
                    //Layer.Delete(resS);

                    //var resd = Layer.Count();
                    var TestCollection = Layer.GetClass(1); 

                }

            }


            //Console.ReadKey();
        }
    }
}
