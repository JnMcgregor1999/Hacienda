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
    public class Mtr_UserController : Controller
    {
        private IConfiguration configuration;
        public Mtr_UserController(IConfiguration iConfiguration)
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
        [Route("List")]
        [HttpPost]
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

                    return Ok(list);
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
        [Route("Get")]
        [HttpPost]
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

                    return Ok(resultModel);
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
        [Route("Save")]
        [HttpPost]
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

                    return Ok(resultModel);
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
