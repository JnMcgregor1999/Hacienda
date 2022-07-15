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
    public class ExternalFacturationController : Controller
    {
        private IConfiguration configuration;
        public ExternalFacturationController(IConfiguration iConfiguration)
        {
            configuration = iConfiguration;
        }
        #region Region [Methods]
        [Route("ValidateFile")]
        [HttpPost]
        public async Task<IActionResult> ValidateFile([FromBody] Common.IExternalFacturation model)
        {
            try
            {
                var message = new Message();
                if (model == null ||
                   model.Item == null ||
                   model.Item.Client_Name == "" ||
                   model.Item.File_Path == "" ||
                   model.Item.New_File == "" ||
                   model.Item.Reference_Number == ""
                   )
                {
                    message.Status = Status.Failed;
                    return BadRequest("Invalid Object");
                }

                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:ExternalFacturation");
                message.Connection = configuration.GetValue<string>("ConnectionStrings:HACIENDA");
                message.Operation = Operation.ValidateFile;
                message.MessageInfo = model.SerializeObject();
                using (var businessLgic = new DoWorkService())
                {
                    var result = await businessLgic.DoWork(message);
                    if (result.Status == Status.Failed)
                    {
                        return BadRequest(result.Result);
                    }
                    // var list = result.DeSerializeObject<IEnumerable<Common.IExternalFacturation>>();

                    return Ok(result);
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
