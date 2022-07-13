using System;
using System.Threading.Tasks;
using Utn.Hacienda.Backend.Common;
using static Utn.Hacienda.Backend.Common.Enum;
using Utn.Hacienda.Backend.Utilities;
using Utn.Hacienda.Backend.DataAccess.Repository;
namespace Utn.Hacienda.Backend.BusinessLogic
{
    public class ExternalFacturation
    {
        #region Region [Methods]
        public async Task<Common.Message> DoWork(Message message)
        {
            try
            {
                switch (message.Operation)
                {
                    case Operation.ValidateFile:
                        return await ValidateFile(message);

                    default:
                        var resultMessage = new Message();
                        resultMessage.Status = Status.Failed;
                        resultMessage.Result = "Operación no soportada";
                        resultMessage.MessageInfo = string.Empty;
                        return resultMessage;
                }
            }
            catch (Exception ex)
            {
                var resultMessage = new Message();
                resultMessage.Status = Status.Failed;
                resultMessage.Result = string.Format("{0}", ex.Message);
                resultMessage.MessageInfo = string.Empty;
                return resultMessage;
            }
        }
        public async virtual Task<Message> ValidateFile(Message message)
        {
            try
            {
                var resultMessage = new Common.Message();
                var model = message.DeSerializeObject<Common.IExternalFacturation>();

                // Validamos que el e-commerce este registrado en hacienda
                var modelUser = new Common.Mtr_User();
                modelUser.Identification = "";

                var userBusinessLogic = new BusinessLogic.Mtr_User();
                resultMessage.MessageInfo = modelUser.SerializeObject<Common.Mtr_User>();
                var userResponse = await userBusinessLogic.Get(resultMessage);

                if (userResponse.MessageInfo.DeSerializeObject<Common.Mtr_User>().Pk_Mtr_User > 0)
                {
                    // Creamos el objeto para guardar en nuestra base de datos lo que se factura
                    var modelInvoice = new Common.Mtr_Invoice();
                    modelInvoice.Creation_User = "api";
                    modelInvoice.Modification_User = "api";
                    modelInvoice.Reference_Number = model.Item.Reference_Number;
                    modelInvoice.Invoice_Url = model.Item.New_File;

                    // Mandamos a llamar el business para que guarde los datos
                    var invoiceBusinessLogic = new BusinessLogic.Mtr_Invoice();
                    resultMessage.MessageInfo = modelInvoice.SerializeObject<Common.Mtr_Invoice>();
                    await invoiceBusinessLogic.Save(resultMessage);


                    // Mandamos a buscar el parametro para el api de firma




                    // Creamos el objeto parametros para mandarlo a firma
                    var modelSignature = new Common.IExternalSignature();
                    modelSignature.Item.New_File = model.Item.New_File;
                    modelSignature.Item.Public_Key = "";


                    // Mandamos a llamar el api de firma
                    var signatureResponse = SendRequest.InvokePost("url", "method", "parameters");


                    // Crear el objeto de retorno
                    

                }












                using (var repository = new Gbl_Catalog_Repository(message.Connection))
                {
                    var returnObject = await repository.List(model);
                    resultMessage.Status = Status.Success;
                    resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                    resultMessage.MessageInfo = returnObject.SerializeObject();
                    return resultMessage;
                }




                using (var repository = new Gbl_Catalog_Repository(message.Connection))
                {
                    var returnObject = await repository.List(model);
                    resultMessage.Status = Status.Success;
                    resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                    resultMessage.MessageInfo = returnObject.SerializeObject();
                    return resultMessage;
                }
            }
            catch (Exception ex)
            {
                var resultMessage = new Common.Message();
                resultMessage.Status = Status.Failed;
                resultMessage.Result = string.Format("{0}", ex.Message);
                resultMessage.MessageInfo = string.Empty;
                return resultMessage;
            }
        }


        #endregion
        #region Region [Dispose]
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
        }
        ~ExternalFacturation()
        {
            this.Dispose(false);
        }
        #endregion
    }
}
