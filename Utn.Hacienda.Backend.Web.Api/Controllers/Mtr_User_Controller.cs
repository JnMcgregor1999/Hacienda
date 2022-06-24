using System;
using Microsoft.AspNetCore.Mvc;
using Utn.Hacienda.Backend.BusinessLogic;
using Utn.Hacienda.Backend.Utilities;
using Utn.Hacienda.Backend.Common;
using static Utn.Hacienda.Backend.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Utn.Hacienda.Backend.WepApi.Controllers
{
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Mtr_User_Controller : Controller
    {
        private IConfiguration configuration;
        public Mtr_User_Controller(IConfiguration iConfiguration)
        {
            configuration = iConfiguration;
        }
        #region Region [Methods]
        /// <summary>
        /// Nombre: ListarMtr_User
        /// Descripcion: Metodo utilizado para ontener una lista de modelos MTR_USER y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> List([FromBody] Common.Mtr_User model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Mtr_User");
                message.Connection = configuration.GetValue<string>("ConnectionStrings:HACIENDA");
                message.Operation = Operation.List;
                message.MessageInfo = model.SerializeObject();
                using (var businessLgic = new DoWorkService())
                {
                    var result = await businessLgic.DoWork(message);
                    if (result.Status == Status.Failed)
                    {
                        return BadRequest(result.Result);
                    }
                    var list = result.DeSerializeObject<IEnumerable<Common.Mtr_User>>();
                    var dataSuccess = new
                    {
                        Data = list,
                        MessageResult = Backend.Common.Enum.Status.Success,
                        Message = string.Empty,
                        RegisterType = string.Empty
                    };
                    return Ok(dataSuccess);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Nombre: ObtenerMtr_User
        /// Descripcion: Metodo utilizado para ontener una lista de modelos MTR_USER y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> Get([FromBody] Common.Mtr_User model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Mtr_User");
                message.Connection = configuration.GetValue<string>("ConnectionStrings:HACIENDA");
                message.Operation = Operation.Get;
                message.MessageInfo = model.SerializeObject();
                using (var businessLgic = new DoWorkService())
                {
                    var result = await businessLgic.DoWork(message);
                    if (result.Status == Status.Failed)
                    {
                        return BadRequest(result.Result);
                    }
                    var resultModel = result.DeSerializeObject<Common.Mtr_User>();
                    var dataSuccess = new
                    {
                        Data = resultModel,
                        MessageResult = Backend.Common.Enum.Status.Success,
                        Message = string.Empty,
                        RegisterType = string.Empty
                    };
                    return Ok(dataSuccess);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Nombre: GuardarMtr_User
        /// Descripcion: Metodo utilizado para ontener una lista de modelos MTR_USER y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> Save([FromBody] Common.Mtr_User model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Mtr_User");
                message.Connection = configuration.GetValue<string>("ConnectionStrings:HACIENDA");
                message.Operation = Operation.Save;
                message.MessageInfo = model.SerializeObject();
                using (var businessLgic = new DoWorkService())
                {
                    var result = await businessLgic.DoWork(message);
                    if (result.Status == Status.Failed)
                    {
                        return BadRequest(result.Result);
                    }
                    var resultModel = result.DeSerializeObject<Common.Mtr_User>();
                    var dataSuccess = new
                    {
                        Data = resultModel,
                        MessageResult = Backend.Common.Enum.Status.Success,
                        Message = string.Empty,
                        RegisterType = string.Empty
                    };
                    return Ok(dataSuccess);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion Region [Methods]
    }
}
