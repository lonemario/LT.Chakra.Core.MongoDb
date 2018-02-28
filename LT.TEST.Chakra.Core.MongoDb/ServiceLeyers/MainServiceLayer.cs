using LT.TEST.Chakra.Core.MongoDb.Data.Repositories;
using LT.TEST.Chakra.Core.MongoDb.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.ServicesLayers;

namespace LT.TEST.Chakra.Core.MongoDb.ServiceLeyers
{
    public class MainServiceLayer : DataServiceLayerBase
    {
        #region Private fields
        private readonly ITestClassRepository _TestRepository;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession">Active data session</param>
        public MainServiceLayer(IDataSession dataSession)
            : base(dataSession)
        {
            //Inizializzo i repository
            _TestRepository = dataSession.ResolveRepository<ITestClassRepository>();

        }


        public TestClass GetSingle(string id)
        {
            return _TestRepository.GetSingle(t => t.Id == id);
        }


        public IList<TestClass> Fetch(Expression<Func<TestClass, bool>> filterExpression = null, int? startRowIndex = null,
            int? maximumRows = null, Expression<Func<TestClass, object>> sortExpression = null, bool isDescending = false)
        {
            return _TestRepository.Fetch(filterExpression, startRowIndex, maximumRows, sortExpression, isDescending);
        }


        public void Delete(TestClass entityToDelete)
        {
            _TestRepository.Delete(entityToDelete);
        }


        public int Count(Expression<Func<TestClass, bool>> filterExpression = null)
        {
            return _TestRepository.Count(filterExpression);
        }


        public TestClass GetClass(int? property1)
        {
            return _TestRepository.GetTestClassByProperty1(property1);
        }
        /// <summary>
        /// Save Audience
        /// </summary>
        /// <param name="audience">Audience</param>
        /// <returns>Returns validations</returns>
        public IList<ValidationResult> SaveTest(ref TestClass entity, bool validateEntity = true)
        {
            //Valido Argomenti
            //if (entity == null) throw new ArgumentNullException(nameof(entity));

            //if (validateEntity)
            //{
            //    //Valido l'entità, se ha id nullo ne metto uno fittizio che poi tolgo prima di fare Save
            //    bool idFittizio = false;
            //    if (String.IsNullOrWhiteSpace(entity.Id))
            //    {
            //        entity.Id = "[FAKEID]" + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            //        idFittizio = true;
            //    }
            //    var resValid = _AudienceRepository.Validate(entity);
            //    if (resValid.Count > 0)
            //        return resValid;
            //    if (idFittizio)
            //        entity.Id = null;

            //    //CONTROLLO ESISTENZA COMPANY
            //    if (!String.IsNullOrWhiteSpace(entity.CompanyId))
            //    {
            //        var resCompany = GetCompanyById(entity.CompanyId);
            //        if (resCompany == null)
            //            throw new InvalidOperationException($"[VISIBLE]The Company with id: {entity.CompanyId} not exist");
            //    }
            //}

            //Utilizzo il metodo base
            return SaveEntity(entity, _TestRepository);
        }
    }
}
