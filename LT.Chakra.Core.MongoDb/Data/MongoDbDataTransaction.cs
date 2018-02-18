using LT.Chakra.Core.MongoDb.Configuration;
using System;
using ZenProgramming.Chakra.Core.Data;
namespace LT.Chakra.Core.MongoDb.Data
{
    public class MongoDbDataTransaction<TOptions> : IDataTransaction
        where TOptions : class, IMongoDbOptions, new()
    {
        private bool _IsDisposed;
        private readonly MongoDbDataSession<TOptions> _DataSession;
        public bool IsActive { get; private set; }
        public bool WasRolledBack { get; private set; }
        public bool WasCommitted { get; private set; }
        public bool IsTransactionOwner { get; protected set; }

        public MongoDbDataTransaction(MongoDbDataSession<TOptions> dataSession)
        {
            //Validazione argomenti
            if (dataSession == null) throw new ArgumentNullException(nameof(dataSession));

            //Imposto lo stato iniziale
            IsActive = true;
            IsTransactionOwner = true;
            WasCommitted = false;
            WasRolledBack = false;

            //Imposto la data session
            _DataSession = dataSession;

            //Se già esiste una transanzione sull'holder, esco
            if (_DataSession.Transaction != null)
                return;

            //Imposto l'istanza corrente
            _DataSession.SetActiveTransaction(this);
        }

        public void Commit()
        {
            //se l'istanza è la proprietaria della transazione
            if (IsTransactionOwner)
            {
                //imposto i flag per commit
                IsActive = false;
                WasCommitted = true;
                WasRolledBack = false;

                //Rimuovo il riferimento alla transazione
                _DataSession.SetActiveTransaction(this);
            }
        }

        public void Rollback()
        {
            //se l'istanza è la proprietaria della transazione
            if (IsTransactionOwner)
            {
                //imposto i flag per commit
                IsActive = false;
                WasCommitted = false;
                WasRolledBack = true;

                //Rimuovo il riferimento alla transazione
                _DataSession.SetActiveTransaction(this);
            }
        }

        ~MongoDbDataTransaction()
        {
            //Richiamo i dispose implicito
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
            //Se l'oggetto è già rilasciato, esco
            if (_IsDisposed)
                return;

            //Se è richiesto il rilascio esplicito
            if (isDisposing)
            {
                //Se l'istanza è proprietaria e non ho chiuso, eccezione
                if (IsTransactionOwner && IsActive)
                    throw new InvalidOperationException("Transaction was opened but never committed o rolled back");

                //Marco il dispose e invoco il GC
                _IsDisposed = true;
            }
        }

    }
}
