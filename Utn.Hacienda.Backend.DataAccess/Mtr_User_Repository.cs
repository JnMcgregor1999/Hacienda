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

    public class Mtr_User_Repository : IRepository<Common.Mtr_User>, IDisposable
    {

        #region Region [Variables]
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        private string ConnectionString { get; set; }
        #endregion

        #region Region [Constructor]
        public Mtr_User_Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Region [Methods]
        public async Task<IEnumerable<Mtr_User>> List(Mtr_User model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Mtr_User>
                    ("PA_CON_MTR_USER_GET",
                    param: new
                    {
                        P_PK_MTR_USER = model.Pk_Mtr_User,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_CATALOG_IDENTIFICATION_TYPE = model.Fk_Catalog_Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_FULL_NAME = model.Full_Name,
                        P_EMAIL = model.Email,
                        P_PASSWORD = model.Password,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<IEnumerable<Mtr_User>>(result.ToList());
            }
        }
        public async Task<ICollection<Mtr_User>> ListCollection(Mtr_User model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Mtr_User>
                    ("PA_CON_MTR_USER_GET",
                    param: new
                    {
                        P_PK_MTR_USER = model.Pk_Mtr_User,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_CATALOG_IDENTIFICATION_TYPE = model.Fk_Catalog_Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_FULL_NAME = model.Full_Name,
                        P_EMAIL = model.Email,
                        P_PASSWORD = model.Password,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<ICollection<Common.Mtr_User>>(result.ToList());
            }
        }
        public async Task<Common.Mtr_User> Get(Common.Mtr_User model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Utn.Hacienda.Backend.Common.Mtr_User>
                    ("PA_CON_MTR_USER_GET",
                    param: new
                    {
                        P_PK_MTR_USER = model.Pk_Mtr_User,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_CATALOG_IDENTIFICATION_TYPE = model.Fk_Catalog_Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_FULL_NAME = model.Full_Name,
                        P_EMAIL = model.Email,
                        P_PASSWORD = model.Password,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return await Task.FromResult<Common.Mtr_User>(result);
            }
        }
        public async Task Save(Common.Mtr_User model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.QueryAsync<
                    Utn.Hacienda.Backend.Common.Mtr_User>
                    ("PA_MAN_MTR_USER_SAVE",
                    param: new
                    {
                        P_PK_MTR_USER = model.Pk_Mtr_User,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_CATALOG_IDENTIFICATION_TYPE = model.Fk_Catalog_Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_FULL_NAME = model.Full_Name,
                        P_EMAIL = model.Email,
                        P_PASSWORD = model.Password,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Delete(Common.Mtr_User model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                sql: "PA_MTR_USER_DELETE",
                param: new
                {
                    P_MAN_PK_MTR_USER = model.Pk_Mtr_User
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
        ~Mtr_User_Repository()
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
