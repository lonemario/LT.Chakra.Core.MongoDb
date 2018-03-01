using LT.Chakra.Core.MongoDb.Configuration;
using LT.Chakra.Core.MongoDb.Utilities;
using LT.PluralizeEn;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories;
using ZenProgramming.Chakra.Core.Data.Repositories.Helpers;
using ZenProgramming.Chakra.Core.Entities;

namespace LT.Chakra.Core.MongoDb.Data.Repositories
{
    public abstract class MongoDbRepositoryBase<TEntity,TOptions> : IRepository<TEntity>, IMongoDbRepository
    where TEntity : class, IModernEntity, new()
    where TOptions : class, IMongoDbOptions, new()
    {
        private bool _IsDisposed;
        protected MongoDbDataSession<TOptions> DataSession { get; }
        private string _CollectionName;
        protected IList<TEntity> Collection {
            get
            {
                return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).FindSync(_ => true).ToList();
            }
        }
        protected IMongoDbOptions Options {
            get {
                return DataSession.Options;
            }
        }

        /// <summary>
        /// Costructor of Mongo Repository Base
        /// </summary>
        /// <param name="dataSession">Data Session</param>
        /// <param name="CollectionName">if null use as collectionName the pluralized class name</param>
        protected MongoDbRepositoryBase(IDataSession dataSession, string CollectionName = null)
        {
            //Validazione argomenti
            if (dataSession == null) throw new ArgumentNullException(nameof(dataSession));
            //dataSession.
            //var options = new TOptions();
            //Type type = System.Type.GetType();
            //dataSession.

            //Tento il cast della sessione generica a RemoteApiDataSession
            if (!(dataSession is MongoDbDataSession<TOptions> currentSession))
                throw new InvalidCastException(string.Format("Specified session of type '{0}' cannot be converted to type '{1}'.",
                    dataSession.GetType().FullName, typeof(MongoDbDataSession<TOptions>).FullName));

            //Imposto la proprietà della sessione
            DataSession = currentSession;

            //DataSession = dataSession;
            //Imposto il nome della Collection
            if (CollectionName == null)
            {
                var pluralize = new PluralizeUtility();
                CollectionName = pluralize.Pluralize(typeof(TEntity));
            }
            _CollectionName = CollectionName;
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).FindSync(expression).FirstOrDefault();
        }

        public virtual IList<TEntity> Fetch(Expression<Func<TEntity, bool>> filterExpression = null, int? startRowIndex = null,
            int? maximumRows = null, Expression<Func<TEntity, object>> sortExpression = null, bool isDescending = false)
        {
            //if (isDescending)
            //{
            //    return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
            //                                                            .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Descending(sortExpression))
            //                                                            .Limit(maximumRows).ToList();
            //}
            //else
            //{
            //    return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
            //                                                            .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Ascending(sortExpression))
            //                                                            .Limit(maximumRows).ToList();
            //}
            int istartRowIndex, imaximumRows;
            if (startRowIndex == null)
            {
                istartRowIndex = 0;
            }
            else
            {
                istartRowIndex = (int)startRowIndex;
            }
            if (maximumRows == null)
            {
                imaximumRows = 0;
            }
            else
            {
                imaximumRows = (int)maximumRows;
            }
            if (imaximumRows > 0)
            {
                if (filterExpression != null)
                {
                    if (sortExpression != null)
                    {
                        if (isDescending)
                        {
                            //return _ctx.Set<TEntity>().Where(filterExpression)
                            //        .OrderByDescending(sortExpression)
                            //        .Skip(istartRowIndex)
                            //        .Take(imaximumRows).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
                                                                        .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Descending(sortExpression))
                                                                        .Limit(maximumRows).ToList();
                        }
                        else
                        {
                            //return _ctx.Set<TEntity>().Where(filterExpression)
                            //        .OrderBy(sortExpression)
                            //        .Skip(istartRowIndex)
                            //        .Take(imaximumRows).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
                                                                        .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Ascending(sortExpression))
                                                                        .Limit(maximumRows).ToList();
                        }
                    }
                    else
                    {
                        //non considerare DESCENDING
                        //return _ctx.Set<TEntity>().Where(filterExpression)
                        //                                    .Take(imaximumRows).ToList();
                        return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
                                            .Skip(startRowIndex).Limit(maximumRows).ToList();
                    }
                }
                else
                {
                    if (sortExpression != null)
                    {
                        if (isDescending)
                        {
                            //return _ctx.Set<TEntity>()
                            //        .OrderByDescending(sortExpression)
                            //        .Skip(istartRowIndex)
                            //        .Take(imaximumRows).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(_ => true)
                                            .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Descending(sortExpression))
                                            .Limit(maximumRows).ToList();
                        }
                        else
                        {
                            //return _ctx.Set<TEntity>()
                            //        .OrderBy(sortExpression)
                            //        .Skip(istartRowIndex)
                            //        .Take(imaximumRows).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(_ => true)
                                            .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Ascending(sortExpression))
                                            .Limit(maximumRows).ToList();
                        }
                    }
                    else
                    {
                        //non considerare DESCENDING
                        //return _ctx.Set<TEntity>().Take(imaximumRows).ToList();
                        return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(_ => true)
                                            .Skip(startRowIndex).Limit(maximumRows).ToList();
                    }
                }
            }
            else
            {
                //NON CONSIDERARE TAKE
                if (filterExpression != null)
                {
                    if (sortExpression != null)
                    {
                        if (isDescending)
                        {
                            //return _ctx.Set<TEntity>().Where(filterExpression)
                            //        .OrderByDescending(sortExpression)
                            //        .Skip(istartRowIndex).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
                                            .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Descending(sortExpression)).ToList();
                        }
                        else
                        {
                            //return _ctx.Set<TEntity>().Where(filterExpression)
                            //        .OrderBy(sortExpression)
                            //        .Skip(istartRowIndex).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
                                            .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Ascending(sortExpression)).ToList();
                        }
                    }
                    else
                    {
                        //non considerare DESCENDING
                        //return _ctx.Set<TEntity>().Where(filterExpression).ToList();
                        return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression)
                                            .Skip(startRowIndex).ToList();
                    }
                }
                else
                {
                    if (sortExpression != null)
                    {
                        if (isDescending)
                        {
                            //return _ctx.Set<TEntity>()
                            //        .OrderByDescending(sortExpression)
                            //        .Skip(istartRowIndex).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(_ => true)
                                    .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Descending(sortExpression)).ToList();
                        }
                        else
                        {
                            //return _ctx.Set<TEntity>()
                            //        .OrderBy(sortExpression)
                            //        .Skip(istartRowIndex).ToList();
                            return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(_ => true)
                                    .Skip(startRowIndex).Sort(Builders<TEntity>.Sort.Ascending(sortExpression)).ToList();
                        }
                    }
                    else
                    {
                        //non considerare DESCENDING
                        //return _ctx.Set<TEntity>().ToList();
                        return DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(_ => true)
                                    .Skip(startRowIndex).ToList();
                    }
                }
            }
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            if (filterExpression == null)
            {
                return unchecked((int)DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(_ => true).Count());
            }
            else
            {
                return unchecked((int)DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).Find(filterExpression).Count());
            }

        }

        public virtual void Save(TEntity entity)
        {
            //VALIDAZIONE ARGOMENTI
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Se Id è vuoto ne genero uno random
            if (String.IsNullOrWhiteSpace(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
                //INSERIMENTO
                DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).InsertOne(entity);
            }
            else
            {
                //SE SI RICHIEDE IL SALVATAGGIO DI UN'ENTITà IL CUI ID NON è PRESENTE NEL DB NON FA NULLA
                //var result = DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).ReplaceOne(e => e.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
                DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).ReplaceOne(e => e.Id == entity.Id, entity);
            }
        }

        public bool IsValid(TEntity entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Utilizzo il metodo di validazione
            var validations = Validate(entity);

            //E' valido se non ho validazioni errate
            return validations.Count == 0;
        }

        public IList<ValidationResult> Validate(TEntity entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Utilizzo l'helper per eseguire l'operazione
            return RepositoryHelper.Validate(entity, DataSession);
        }

        public virtual void Delete(TEntity entity)
        {
            //VALIDAZIONE ARGOMENTI
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            ObjectFilterDefinition<TEntity> filter = new ObjectFilterDefinition<TEntity>(entity);

            DataSession.MongoDatabase.GetCollection<TEntity>(_CollectionName).DeleteOne(filter);
        }

        ~MongoDbRepositoryBase()
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Explicit dispose</param>
        protected virtual void Dispose(bool isDisposing)
        {
            //Se l'oggetto è già rilasciato, esco
            if (_IsDisposed)
                return;

            //Se è richiesto il rilascio esplicito
            if (isDisposing)
            {
                //Rilascio della logica non finalizzabile
                //=> Qui non è necessario
            }

            //Marco il dispose e invoco il GC
            _IsDisposed = true;
        }
    }
}
