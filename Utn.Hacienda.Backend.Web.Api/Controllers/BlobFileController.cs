using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Utn.Hacienda.Backend.Web.Api.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Utn.Hacienda.Backend.Utilities;
using Utn.Hacienda.Backend.Common;
using static Utn.Hacienda.Backend.Common.Enum;
using Utn.Hacienda.Backend.BusinessLogic;

namespace Utn.Hacienda.Backend.WepApi.Controllers
{
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlobFileController : Controller
    {
        private IConfiguration configuration;
        private static readonly Microsoft.AspNetCore.Http.Features.FormOptions _defaultFormOptions = new Microsoft.AspNetCore.Http.Features.FormOptions();
        public BlobFileController(IConfiguration iConfiguration)
        {
            configuration = iConfiguration;
        }
        #region Region [Methods]

        [Route("UploadFile")]
        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                {
                    return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
                }
                var formAccumulator = new KeyValueAccumulator();
                var nameFile = String.Empty;
                string targetFilePathBackUp = string.Empty;
                string targetFilePath = string.Empty;
                var boundary = MultipartRequestHelper.GetBoundary(
                   Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse(Request.ContentType),
                   _defaultFormOptions.MultipartBoundaryLengthLimit);
                var reader = new MultipartReader(boundary, HttpContext.Request.Body);
                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition;
                    var hasContentDispositionHeader = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            targetFilePathBackUp = string.Format("{0}{1}", configuration.GetValue<string>("Files:RutaArchivos"), contentDisposition.FileName.Value);

                            using (var targetStream = System.IO.File.Create(targetFilePathBackUp))
                            {
                                await section.Body.CopyToAsync(targetStream);
                            }
                        }
                        else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                        {
                            // Content-Disposition: form-data; name="key"
                            //
                            // value

                            // Do not limit the key name length here because the 
                            // multipart headers length limit is already in effect.
                            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                            var encoding = GetEncoding(section);
                            using (var streamReader = new StreamReader(
                                section.Body,
                                encoding,
                                detectEncodingFromByteOrderMarks: true,
                                bufferSize: 1024,
                                leaveOpen: true))
                            {
                                // The value length limit is enforced by MultipartBodyLengthLimit
                                var value = await streamReader.ReadToEndAsync();
                                if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                                {
                                    value = String.Empty;
                                }
                                formAccumulator.Append(key.Value, value);

                                if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                                {
                                    throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                                }
                            }
                        }
                    }

                    // Drains any remaining section body that has not been consumed and
                    // reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }

                var pk_Mtr_User = GetDocuments(formAccumulator.GetResults());
                Common.Mtr_User userInfo = new Common.Mtr_User();
                using (StreamReader read = new StreamReader(targetFilePathBackUp))
                {
                    var json = read.ReadToEnd();
                    userInfo = json.DeSerializeObject<Common.Mtr_User>();
                    userInfo.Pk_Mtr_User = pk_Mtr_User;
                    userInfo.Is_Update_Public_Key = true;
                }

                var message = new Message();
                message.BusinessLogic = configuration.GetValue<string>("AppSettings:BusinessLogic:Mtr_User");
                message.Connection = configuration.GetValue<string>("ConnectionStrings:HACIENDA");
                message.Operation = Operation.Save;
                message.MessageInfo = userInfo.SerializeObject();
                using (var businessLgic = new DoWorkService())
                {
                    var result = await businessLgic.DoWork(message);
                    if (result.Status == Status.Failed)
                    {
                        return BadRequest(result.Result);
                    }

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            Microsoft.Net.Http.Headers.MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = Microsoft.Net.Http.Headers.MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF8.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
        private int GetDocuments(Dictionary<string, StringValues> fields)
        {
            var contador = (fields["pk_Mtr_User"].Count);
            var pk_Mtr_User = 0;

            for (var x = 0; x < contador; x++)
            {
                foreach (var dictionary in fields)
                {
                    if (dictionary.Key == "pk_Mtr_User")
                    {
                        pk_Mtr_User = int.Parse(dictionary.Value[x]);
                    }
                }
            }
            return pk_Mtr_User;
        }

        #endregion Region [Methods]
    }
}
