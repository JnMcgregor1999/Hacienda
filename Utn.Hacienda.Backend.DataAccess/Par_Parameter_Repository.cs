using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Utn.Hacienda.Backend.Common;
using Microsoft.Data.SqlClient;

namespace Utn.Hacienda.Backend.DataAccess.Repository
{

    public class Par_Parameter_Repository : IRepository<Common.Par_Parameter>, IDisposable
    {

        #region Region [Variables]
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        private string ConnectionString { get; set; }
        #endregion

        #region Region [Constructor]
        public Par_Parameter_Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Region [Methods]
        public async Task<IEnumerable<Par_Parameter>> List(Par_Parameter model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Par_Parameter>
                    ("PA_CON_PAR_PARAMETER_GET",
                    param: new
                    {
                        P_PK_PAR_PARAMETER = model.Pk_Par_Parameter,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_SEARCH_KEY = model.Search_Key,
                        P_PARAMETER_VALUE = model.Parameter_Value,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<IEnumerable<Par_Parameter>>(result.ToList());
            }
        }
        public async Task<ICollection<Par_Parameter>> ListCollection(Par_Parameter model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Par_Parameter>
                    ("PA_CON_PAR_PARAMETER_GET",
                    param: new
                    {
                        P_PK_PAR_PARAMETER = model.Pk_Par_Parameter,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_SEARCH_KEY = model.Search_Key,
                        P_PARAMETER_VALUE = model.Parameter_Value,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<ICollection<Common.Par_Parameter>>(result.ToList());
            }
        }
        public async Task<Common.Par_Parameter> Get(Common.Par_Parameter model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Utn.Hacienda.Backend.Common.Par_Parameter>
                    ("PA_CON_PAR_PARAMETER_GET",
                    param: new
                    {
                        P_PK_PAR_PARAMETER = model.Pk_Par_Parameter,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_SEARCH_KEY = model.Search_Key,
                        P_PARAMETER_VALUE = model.Parameter_Value,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return await Task.FromResult<Common.Par_Parameter>(result);
            }
        }
        public async Task Save(Common.Par_Parameter model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.QueryAsync<
                    Utn.Hacienda.Backend.Common.Par_Parameter>
                    ("PA_MAN_PAR_PARAMETER_SAVE",
                    param: new
                    {
                        P_PK_PAR_PARAMETER = model.Pk_Par_Parameter,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_SEARCH_KEY = model.Search_Key,
                        P_PARAMETER_VALUE = model.Parameter_Value,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Delete(Common.Par_Parameter model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                sql: "PA_PAR_PARAMETER_DELETE",
                param: new
                {
                    P_MAN_PK_PAR_PARAMETER = model.Pk_Par_Parameter
                },
                commandType: CommandType.StoredProcedure);
            }
        }
        #endregion
        #region Region [Dispose]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Par_Parameter_Repository()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
        #endregion
    }
}
