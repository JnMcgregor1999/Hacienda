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
    public class Gbl_Type_Catalog_Controller : Controller
    {
        private IConfiguration configuration;
        public Gbl_Type_Catalog_Controller(IConfiguration iConfiguration)
        {
            configuration = iConfiguration;
        }
        #region Region [Methods]
        /// <summary>
        /// Nombre: ListarGbl_Type_Catalog
        /// Descripcion: Metodo utilizado para ontener una lista de modelos GBL_TYPE_CATALOG y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> List([FromBody] Common.Gbl_Type_Catalog model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Gbl_Type_Catalog");
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
                    var list = result.DeSerializeObject<IEnumerable<Common.Gbl_Type_Catalog>>();
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
        /// Nombre: ObtenerGbl_Type_Catalog
        /// Descripcion: Metodo utilizado para ontener una lista de modelos GBL_TYPE_CATALOG y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> Get([FromBody] Common.Gbl_Type_Catalog model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Gbl_Type_Catalog");
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
                    var resultModel = result.DeSerializeObject<Common.Gbl_Type_Catalog>();
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
        /// Nombre: GuardarGbl_Type_Catalog
        /// Descripcion: Metodo utilizado para ontener una lista de modelos GBL_TYPE_CATALOG y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> Save([FromBody] Common.Gbl_Type_Catalog model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Gbl_Type_Catalog");
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
                    var resultModel = result.DeSerializeObject<Common.Gbl_Type_Catalog>();
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
