using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Utn.Hacienda.Backend.Common;
using Microsoft.Data.SqlClient;

namespace Utn.Hacienda.Backend.DataAccess.Repository
{

    public class Gbl_Catalog_Repository : IRepository<Common.Gbl_Catalog>, IDisposable
    {

        #region Region [Variables]
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        private string ConnectionString { get; set; }
        #endregion

        #region Region [Constructor]
        public Gbl_Catalog_Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Region [Methods]
        public async Task<IEnumerable<Gbl_Catalog>> List(Gbl_Catalog model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Gbl_Catalog>
                    ("PA_GBL_CATALOG_GET",
                    param: new
                    {
                        P_PK_GBL_CATALOG = model.Pk_Gbl_Catalog,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_GBL_TYPE_CATALOG = model.Fk_Gbl_Type_Catalog,
                        P_FK_GBL_CATALOG = model.Fk_Gbl_Catalog,
                        P_NAME = model.Name,
                        P_DESCRIPTION = model.Description,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<IEnumerable<Gbl_Catalog>>(result.ToList());
            }
        }
        public async Task<ICollection<Gbl_Catalog>> ListCollection(Gbl_Catalog model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Gbl_Catalog>
                    ("PA_CON_GBL_CATALOG_GET",
                    param: new
                    {
                        P_PK_GBL_CATALOG = model.Pk_Gbl_Catalog,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_GBL_TYPE_CATALOG = model.Fk_Gbl_Type_Catalog,
                        P_FK_GBL_CATALOG = model.Fk_Gbl_Catalog,
                        P_NAME = model.Name,
                        P_DESCRIPTION = model.Description,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<ICollection<Common.Gbl_Catalog>>(result.ToList());
            }
        }
        public async Task<Common.Gbl_Catalog> Get(Common.Gbl_Catalog model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Utn.Hacienda.Backend.Common.Gbl_Catalog>
                    ("PA_CON_GBL_CATALOG_GET",
                    param: new
                    {
                        P_PK_GBL_CATALOG = model.Pk_Gbl_Catalog,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_GBL_TYPE_CATALOG = model.Fk_Gbl_Type_Catalog,
                        P_FK_GBL_CATALOG = model.Fk_Gbl_Catalog,
                        P_NAME = model.Name,
                        P_DESCRIPTION = model.Description,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return await Task.FromResult<Common.Gbl_Catalog>(result);
            }
        }
        public async Task Save(Common.Gbl_Catalog model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.QueryAsync<
                    Utn.Hacienda.Backend.Common.Gbl_Catalog>
                    ("PA_MAN_GBL_CATALOG_SAVE",
                    param: new
                    {
                        P_PK_GBL_CATALOG = model.Pk_Gbl_Catalog,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_GBL_TYPE_CATALOG = model.Fk_Gbl_Type_Catalog,
                        P_FK_GBL_CATALOG = model.Fk_Gbl_Catalog,
                        P_NAME = model.Name,
                        P_DESCRIPTION = model.Description,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Delete(Common.Gbl_Catalog model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                sql: "PA_GBL_CATALOG_DELETE",
                param: new
                {
                    P_MAN_PK_GBL_CATALOG = model.Pk_Gbl_Catalog
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
        ~Gbl_Catalog_Repository()
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
