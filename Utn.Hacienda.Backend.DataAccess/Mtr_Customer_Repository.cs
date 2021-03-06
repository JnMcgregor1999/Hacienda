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

    public class Mtr_Customer_Repository : IRepository<Common.Mtr_Customer>, IDisposable
    {

        #region Region [Variables]
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        private string ConnectionString { get; set; }
        #endregion

        #region Region [Constructor]
        public Mtr_Customer_Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Region [Methods]
        public async Task<IEnumerable<Mtr_Customer>> List(Mtr_Customer model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Mtr_Customer>
                    ("PA_CON_MTR_CUSTOMER_GET",
                    param: new
                    {
                        P_PK_MTR_CUSTOMER = model.Pk_Mtr_Customer,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_COMPANY_NAME = model.Company_Name,
                        P_IDENTIFICATION_TYPE = model.Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_SOCIAL_REAZON = model.Social_Reazon,
                        P_COMMERCIAL_NAME = model.Commercial_Name,
                        P_PROVINCE = model.Province,
                        P_CANTON = model.Canton,
                        P_DISTRICT = model.District,
                        P_EMAIL = model.Email,
                        P_TELEPHONE = model.Telephone,
                        P_ADDRESS = model.Address,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<IEnumerable<Mtr_Customer>>(result.ToList());
            }
        }
        public async Task<ICollection<Mtr_Customer>> ListCollection(Mtr_Customer model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Mtr_Customer>
                    ("PA_CON_MTR_CUSTOMER_GET",
                    param: new
                    {
                        P_PK_MTR_CUSTOMER = model.Pk_Mtr_Customer,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_COMPANY_NAME = model.Company_Name,
                        P_IDENTIFICATION_TYPE = model.Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_SOCIAL_REAZON = model.Social_Reazon,
                        P_COMMERCIAL_NAME = model.Commercial_Name,
                        P_PROVINCE = model.Province,
                        P_CANTON = model.Canton,
                        P_DISTRICT = model.District,
                        P_EMAIL = model.Email,
                        P_TELEPHONE = model.Telephone,
                        P_ADDRESS = model.Address,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<ICollection<Common.Mtr_Customer>>(result.ToList());
            }
        }
        public async Task<Common.Mtr_Customer> Get(Common.Mtr_Customer model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Utn.Hacienda.Backend.Common.Mtr_Customer>
                    ("PA_CON_MTR_CUSTOMER_GET",
                    param: new
                    {
                        P_PK_MTR_CUSTOMER = model.Pk_Mtr_Customer,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_COMPANY_NAME = model.Company_Name,
                        P_IDENTIFICATION_TYPE = model.Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_SOCIAL_REAZON = model.Social_Reazon,
                        P_COMMERCIAL_NAME = model.Commercial_Name,
                        P_PROVINCE = model.Province,
                        P_CANTON = model.Canton,
                        P_DISTRICT = model.District,
                        P_EMAIL = model.Email,
                        P_TELEPHONE = model.Telephone,
                        P_ADDRESS = model.Address,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return await Task.FromResult<Common.Mtr_Customer>(result);
            }
        }
        public async Task Save(Common.Mtr_Customer model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.QueryAsync<
                    Utn.Hacienda.Backend.Common.Mtr_Customer>
                    ("PA_MAN_MTR_CUSTOMER_SAVE",
                    param: new
                    {
                        P_PK_MTR_CUSTOMER = model.Pk_Mtr_Customer,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_COMPANY_NAME = model.Company_Name,
                        P_IDENTIFICATION_TYPE = model.Identification_Type,
                        P_IDENTIFICATION = model.Identification,
                        P_SOCIAL_REAZON = model.Social_Reazon,
                        P_COMMERCIAL_NAME = model.Commercial_Name,
                        P_PROVINCE = model.Province,
                        P_CANTON = model.Canton,
                        P_DISTRICT = model.District,
                        P_EMAIL = model.Email,
                        P_TELEPHONE = model.Telephone,
                        P_ADDRESS = model.Address,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Delete(Common.Mtr_Customer model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                sql: "PA_MTR_CUSTOMER_DELETE",
                param: new
                {
                    P_MAN_PK_MTR_CUSTOMER = model.Pk_Mtr_Customer
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
        ~Mtr_Customer_Repository()
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
