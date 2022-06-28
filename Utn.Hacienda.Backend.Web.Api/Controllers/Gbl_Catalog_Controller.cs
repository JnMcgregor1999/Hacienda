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
    public class Gbl_CatalogController : Controller
    {
        private IConfiguration configuration;
        public Gbl_CatalogController(IConfiguration iConfiguration)
        {
            configuration = iConfiguration;
        }
        #region Region [Methods]
        /// <summary>
        /// Nombre: ListarGbl_Catalog
        /// Descripcion: Metodo utilizado para ontener una lista de modelos GBL_CATALOG y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Route("List")]
        [HttpPost]
        public async Task<IActionResult> List([FromBody] Common.Gbl_Catalog model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Gbl_Catalog");
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
                    var list = result.DeSerializeObject<IEnumerable<Common.Gbl_Catalog>>();

                    return Ok(list);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Nombre: ObtenerGbl_Catalog
        /// Descripcion: Metodo utilizado para ontener una lista de modelos GBL_CATALOG y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Route("Get")]
        [HttpPost]

        public async Task<IActionResult> Get([FromBody] Common.Gbl_Catalog model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Gbl_Catalog");
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
                    var resultModel = result.DeSerializeObject<Common.Gbl_Catalog>();

                    return Ok(resultModel);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Nombre: GuardarGbl_Catalog
        /// Descripcion: Metodo utilizado para ontener una lista de modelos GBL_CATALOG y retornar un objeto datatable
        /// Fecha de creacion: 6/21/2022
        /// Autor:
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Route("Save")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Common.Gbl_Catalog model)
        {
            try
            {
                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Gbl_Catalog");
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
                    var resultModel = result.DeSerializeObject<Common.Gbl_Catalog>();

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
