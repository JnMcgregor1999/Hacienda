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

    public class Mtr_Invoice_Repository : IRepository<Common.Mtr_Invoice>, IDisposable
    {

        #region Region [Variables]
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        private string ConnectionString { get; set; }
        #endregion

        #region Region [Constructor]
        public Mtr_Invoice_Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Region [Methods]
        public async Task<IEnumerable<Mtr_Invoice>> List(Mtr_Invoice model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Mtr_Invoice>
                    ("PA_CON_MTR_INVOICE_GET",
                    param: new
                    {
                        P_PK_MTR_INVOICE = model.Pk_Mtr_Invoice,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_MTR_CUSTOMER = model.Fk_Mtr_Customer,
                        P_FK_MTR_USER = model.Fk_Mtr_User,
                        P_REFERENCE_NUMBER = model.Reference_Number,
                        P_INVOICE_URL = model.Invoice_Url,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<IEnumerable<Mtr_Invoice>>(result.ToList());
            }
        }
        public async Task<ICollection<Mtr_Invoice>> ListCollection(Mtr_Invoice model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Common.Mtr_Invoice>
                    ("PA_CON_MTR_INVOICE_GET",
                    param: new
                    {
                        P_PK_MTR_INVOICE = model.Pk_Mtr_Invoice,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_MTR_CUSTOMER = model.Fk_Mtr_Customer,
                        P_FK_MTR_USER = model.Fk_Mtr_User,
                        P_REFERENCE_NUMBER = model.Reference_Number,
                        P_INVOICE_URL = model.Invoice_Url,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult<ICollection<Common.Mtr_Invoice>>(result.ToList());
            }
        }
        public async Task<Common.Mtr_Invoice> Get(Common.Mtr_Invoice model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<
                    Utn.Hacienda.Backend.Common.Mtr_Invoice>
                    ("PA_CON_MTR_INVOICE_GET",
                    param: new
                    {
                        P_PK_MTR_INVOICE = model.Pk_Mtr_Invoice,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_MTR_CUSTOMER = model.Fk_Mtr_Customer,
                        P_FK_MTR_USER = model.Fk_Mtr_User,
                        P_REFERENCE_NUMBER = model.Reference_Number,
                        P_INVOICE_URL = model.Invoice_Url,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return await Task.FromResult<Common.Mtr_Invoice>(result);
            }
        }
        public async Task Save(Common.Mtr_Invoice model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.QueryAsync<
                    Utn.Hacienda.Backend.Common.Mtr_Invoice>
                    ("PA_MAN_MTR_INVOICE_SAVE",
                    param: new
                    {
                        P_PK_MTR_INVOICE = model.Pk_Mtr_Invoice,
                        P_CREATION_USER = model.Creation_User,
                        P_CREATION_DATE = model.Creation_Date,
                        P_MODIFICATION_USER = model.Modification_User,
                        P_MODIFICATION_DATE = model.Modification_Date,
                        P_FK_MTR_CUSTOMER = model.Fk_Mtr_Customer,
                        P_FK_MTR_USER = model.Fk_Mtr_User,
                        P_REFERENCE_NUMBER = model.Reference_Number,
                        P_INVOICE_URL = model.Invoice_Url,
                        P_ACTIVE = model.Active,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Delete(Common.Mtr_Invoice model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                sql: "PA_MTR_INVOICE_DELETE",
                param: new
                {
                    P_MAN_PK_MTR_INVOICE = model.Pk_Mtr_Invoice
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
        ~Mtr_Invoice_Repository()
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
