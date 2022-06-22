using System;
using System.Threading.Tasks;
using Utn.Hacienda.Backend.Common;
using static Utn.Hacienda.Backend.Common.Enum;
using Utn.Hacienda.Backend.Utilities;
using Utn.Hacienda.Backend.DataAccess.Repository;
namespace Utn.Hacienda.Backend.BusinessLogic
{
    public class Mtr_User : IBusinessLogic
    {
        #region Region [Methods]
        /// <summary>
        /// Nombre: DoWork
        /// Descripcion: Metodo encargado de orquestar las solicitudes de operaciones para el objeto "MTR_USER".
        /// Fecha de creación: 6/21/2022.
        /// Autor: JnMcGregor.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<Common.Message> DoWork(Message message)
        {
            try
            {
                switch (message.Operation)
                {
                    case Operation.List:
                        return await List(message);
                    case Operation.Get:
                        return await Get(message);
                    case Operation.Save:
                        return await Save(message);
                    case Operation.Delete:
                        return await Delete(message);
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
        public async virtual Task<Message> List(Message message)
        {
            try
            {
                var resultMessage = new Common.Message();
                var model = message.DeSerializeObject<Common.Mtr_User>();
                using (var repository = new Mtr_User_Repository(message.Connection))
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
        public async virtual Task<Message> Get(Message message)
        {
            try
            {
                var resultMessage = new Message();
                var model = message.DeSerializeObject<Common.Mtr_User>();
                using (var repository = new Mtr_User_Repository(message.Connection))
                {
                    var returnObject = await repository.Get(model);
                    resultMessage.Status = Status.Success;
                    resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                    resultMessage.MessageInfo = returnObject.SerializeObject();
                    return resultMessage;
                }
            }
            catch (Exception ex)
            {
                var resultMessage = new Message();
                resultMessage.Status = Status.Failed;
                resultMessage.Result = string.Format("{ 0}", ex.Message);
                resultMessage.MessageInfo = string.Empty;
                return resultMessage;
            }
        }
        public async virtual Task<Message> Save(Message message)
        {
            try
            {
                var resultMessage = new Message();
                var model = message.DeSerializeObject<Common.Mtr_User>();
                using (var repository = new Mtr_User_Repository(message.Connection))
                {
                    await repository.Save(model);
                    resultMessage.Status = Status.Success;
                    resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                    resultMessage.MessageInfo = String.Empty;
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
        public async virtual Task<Common.Message> Delete(Message message)
        {
            try
            {
                var resultMessage = new Message();
                var model = message.DeSerializeObject<Common.Mtr_User>();
                using (var repository = new Mtr_User_Repository(message.Connection))
                {
                    await repository.Delete(model);
                    resultMessage.Status = Status.Success;
                    resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                    resultMessage.MessageInfo = String.Empty;
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
        ~Mtr_User()
        {
            this.Dispose(false);
        }
        #endregion
    }
}
