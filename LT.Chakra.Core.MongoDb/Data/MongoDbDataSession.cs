using LT.Chakra.Core.MongoDb.Configuration;
using LT.Chakra.Core.MongoDb.Data.Repositories;
using MongoDB.Driver;
using System;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories;
using ZenProgramming.Chakra.Core.Data.Repositories.Helpers;

namespace LT.Chakra.Core.MongoDb.Data
{
    public class MongoDbDataSession<TOptions> : IDataSession
        where TOptions : class, IMongoDbOptions, new()
    {
        private bool _IsDisposed;
        public MongoClient Client { get; }
        public IMongoDatabase MongoDatabase { get; }

        public IMongoDbOptions Options { get;  }

        public MongoDbDataSession()
        {
            //var con = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            var options = new TOptions();
            Options = options;
            //Inizializzazione del client MongoDb
            var dbUrl = options.MongoDbUrl; // @"mongodb://192.168.44.132:27017";
            var dbName = options.MongoDbName; // "TestDb";

            Client = new MongoClient(dbUrl);
            MongoDatabase = Client.GetDatabase(dbName);
        }

        //public MongoDbDataSession(string DbUrl, string DbName)
        //{
        //    //var con = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);


        //    //Inizializzazione del client MongoDb
        //    var dbUrl = @"mongodb://192.168.44.132:27017";
        //    var dbName = "TestDb";

        //    Client = new MongoClient(dbUrl);
        //    MongoDatabase = Client.GetDatabase(dbName);
        //}

        public TRepositoryInterface ResolveRepository<TRepositoryInterface>() where TRepositoryInterface : IRepository
        {
            //Utilizzo il metodo presente nell'helper per generare il Repository
            return RepositoryHelper.Resolve<TRepositoryInterface, IMongoDbRepository>(this);
        }

        public IDataTransaction Transaction { get; private set; }

        public IDataTransaction BeginTransaction()
        {
            return new MongoDbDataTransaction<TOptions>(this);
        }

        public TOutput As<TOutput>() where TOutput : class
        {
            //Se il tipo di destinazione non è lo stesso dell'istanza corrente emetto l'eccezione
            if (GetType() != typeof(TOutput))
                throw new InvalidCastException(string.Format("Unable to convert data session type '{0}' to" +
                    " requested type '{1}'.", GetType().FullName, typeof(TOutput).FullName));

            //Eseguo la conversione e ritorno
            return this as TOutput;
        }

        public void SetActiveTransaction(IDataTransaction dataTransaction)
        {
            //Validazione Argomenti
            if (dataTransaction == null) throw new ArgumentNullException(nameof(dataTransaction));

            //Imposto la transazione
            Transaction = dataTransaction;
        }

        ~MongoDbDataSession()
        {
            //richiamo i dispose implicito
            Dispose(false);
        }

        public void Dispose()
        {
            //Eseguo una dispose esplicita
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            //se l'oggetto è già rilasciato, esco
            if (_IsDisposed)
                return;

            // se è richiesto il rilascio espilcito
            if (isDisposing)
            {
                //rilascio eventualmente il client
            }

            //Marco il dispose e invoco il GC
            _IsDisposed = true;
        }

    }
}
