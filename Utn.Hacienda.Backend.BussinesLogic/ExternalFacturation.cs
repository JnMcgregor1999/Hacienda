using System;
using System.Threading.Tasks;
using Utn.Hacienda.Backend.Common;
using static Utn.Hacienda.Backend.Common.Enum;
using Utn.Hacienda.Backend.Utilities;
using Utn.Hacienda.Backend.DataAccess.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
                var newXmlBase64 = "";

                string searchKeyIsProdSignature = "IS_PROD_SIGNATURE";
                string searchKeyApiSignature = "API_SIGNATURE";
                string searchKeyValidateWithXds = "VALIDATE_WITH_XDS";
                string searchKeyUrlXds = "URL_XDS";
                string searchKeyMethodApiSignature = "METHOD_API_SIGNATURE";

                bool isProdSignature = false;
                string apiSignature = "";
                bool validateWithXDS = false;
                string urlXds = "";
                string methodApiSignature = "";

                // Mandamos a buscar el parametros
                var parameterRepository = new Par_Parameter_Repository(message.Connection);
                var parameterResponse = parameterRepository.List(new Common.Par_Parameter()).Result;

                foreach (var parameter in parameterResponse)
                {
                    if (parameter.Search_Key == searchKeyIsProdSignature)
                    {
                        isProdSignature = Convert.ToBoolean(parameter.Parameter_Value);
                    }
                    else if (parameter.Search_Key == searchKeyApiSignature)
                    {
                        apiSignature = parameter.Parameter_Value;
                    }
                    else if (parameter.Search_Key == searchKeyValidateWithXds)
                    {
                        validateWithXDS = Convert.ToBoolean(parameter.Parameter_Value);
                    }
                    else if (parameter.Search_Key == searchKeyUrlXds)
                    {
                        urlXds = parameter.Parameter_Value;
                    }
                    else if (parameter.Search_Key == searchKeyMethodApiSignature)
                    {
                        methodApiSignature = parameter.Parameter_Value;
                    }
                }

                // Validar contra xds
                if (validateWithXDS)
                {
                    throw new Exception("Error validacion xds");
                }

                // Validamos que el e-commerce este registrado en hacienda
                var userRepository = new Mtr_User_Repository(message.Connection);
                var userResponse = userRepository.Get(new Common.Mtr_User { Identification = model.Item.Commercial_Identification }).Result;


                if (isProdSignature)
                {
                    var modelSignature = new Common.IExternalSignature();
                    modelSignature.Item.Public_Key = userResponse.Public_Key;
                    // Mandamos a llamar el api de firma
                    var signatureResponse = SendRequest.InvokePost(apiSignature, methodApiSignature, modelSignature);
                    Common.IExternalSignature signature_Response_Deserialization = JsonConvert.DeserializeObject<Common.IExternalSignature>(signatureResponse);

                    if (!signature_Response_Deserialization.Item.isValid)
                    {
                        throw new Exception("Error validacion firma");
                    }

                }

                if (userResponse.Pk_Mtr_User > 0)
                {
                    var customerRepository = new Mtr_Customer_Repository(message.Connection);
                    var customerResponse = customerRepository.Get(new Common.Mtr_Customer { Identification = model.Item.Client_Identification }).Result;

                    // Creamos el objeto para guardar en nuestra base de datos lo que se factura
                    var modelInvoice = new Common.Mtr_Invoice();
                    modelInvoice.Creation_User = "api";
                    modelInvoice.Modification_User = "api";
                    modelInvoice.Reference_Number = model.Item.Reference_Number;
                    modelInvoice.Invoice_Url = model.Item.File_Path;
                    modelInvoice.Fk_Mtr_Customer = customerResponse.Pk_Mtr_Customer;
                    modelInvoice.Fk_Mtr_User = userResponse.Pk_Mtr_User;
                    modelInvoice.Active = true;


                    // Mandamos a llamar el business para que guarde los datos
                    var invoiceRepository = new Mtr_Invoice_Repository(message.Connection);
                    // await invoiceRepository.Save(modelInvoice);


                    newXmlBase64 = await generateXML(model.Item.New_File);
                }
                else
                {
                    throw new Exception("No se encontro al cliente");
                }

                // Crear el objeto de retorno
                var signature_Response = new
                {
                    item = new
                    {
                        New_File = newXmlBase64,
                        code = 200,
                        message = "success"
                    }
                };

                resultMessage.Status = Status.Success;
                resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                resultMessage.MessageInfo = signature_Response.SerializeObject();
                return resultMessage;
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


        public async virtual Task<string> generateXML(string xmlBase64)
        {
            var xmlString = Encoding.UTF8.GetString(Convert.FromBase64String(xmlBase64));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            string xmlClave = xmlDoc.GetElementsByTagName("Clave")[0].InnerXml;
            string xmlCodigoActividad = xmlDoc.GetElementsByTagName("CodigoActividad")[0].InnerXml;
            string xmlNumeroConsecutivo = xmlDoc.GetElementsByTagName("NumeroConsecutivo")[0].InnerXml;
            string xmlFechaEmision = xmlDoc.GetElementsByTagName("FechaEmision")[0].InnerXml;
            XmlNodeList xmlEmisor = xmlDoc.GetElementsByTagName("Emisor");


            //Decalre a new XMLDocument object
            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement element1 = doc.CreateElement(string.Empty, "FacturaElectronica", string.Empty);
            doc.AppendChild(element1);

            XmlElement elementClave = doc.CreateElement(string.Empty, "Clave", string.Empty);
            XmlText textClave = doc.CreateTextNode(xmlClave);
            elementClave.AppendChild(textClave);
            element1.AppendChild(elementClave);

            XmlElement elementCodigoActividad = doc.CreateElement(string.Empty, "CodigoActividad", string.Empty);
            XmlText textCodigoActividad = doc.CreateTextNode(xmlCodigoActividad);
            elementCodigoActividad.AppendChild(textCodigoActividad);
            element1.AppendChild(elementCodigoActividad);

            XmlElement elementNumeroConsecutivo = doc.CreateElement(string.Empty, "NumeroConsecutivo", string.Empty);
            XmlText textNumeroConsecutivo = doc.CreateTextNode(xmlNumeroConsecutivo);
            elementNumeroConsecutivo.AppendChild(textNumeroConsecutivo);
            element1.AppendChild(elementNumeroConsecutivo);


            XmlElement elementFechaEmision = doc.CreateElement(string.Empty, "FechaEmision", string.Empty);
            XmlText textFechaEmision = doc.CreateTextNode(xmlFechaEmision);
            elementFechaEmision.AppendChild(textFechaEmision);
            element1.AppendChild(elementFechaEmision);

            XmlElement elementEmisorNombre = doc.CreateElement(string.Empty, "Nombre", string.Empty);
            XmlText textEmisorNombre = doc.CreateTextNode(xmlEmisor[0].ChildNodes[0].InnerXml);
            elementEmisorNombre.AppendChild(textEmisorNombre);

            XmlElement elementEmisorIdentificacion = doc.CreateElement(string.Empty, "Identificacion", string.Empty);
            XmlText textEmisorIdentificacion = doc.CreateTextNode(xmlEmisor[0].ChildNodes[1].InnerXml);
            elementEmisorIdentificacion.AppendChild(textEmisorIdentificacion);

            XmlElement elementEmisorUbicacion = doc.CreateElement(string.Empty, "Ubicacion", string.Empty);
            XmlText textEmisorUbicacion = doc.CreateTextNode(xmlEmisor[0].ChildNodes[2].InnerXml);
            elementEmisorUbicacion.AppendChild(textEmisorUbicacion);

            XmlElement elementEmisorTelefono = doc.CreateElement(string.Empty, "Telefono", string.Empty);
            XmlText textEmisorTelefono = doc.CreateTextNode(xmlEmisor[0].ChildNodes[3].InnerXml);
            elementEmisorTelefono.AppendChild(textEmisorTelefono);

            XmlElement elementEmisorCorreoElectronico = doc.CreateElement(string.Empty, "CorreoElectronico", string.Empty);
            XmlText textEmisorCorreoElectronico = doc.CreateTextNode(xmlEmisor[0].ChildNodes[4].InnerXml);
            elementEmisorCorreoElectronico.AppendChild(textEmisorCorreoElectronico);



            XmlElement elementEmisor = doc.CreateElement(string.Empty, "Emisor", string.Empty);

            elementEmisor.AppendChild(elementEmisorNombre);
            elementEmisor.AppendChild(elementEmisorIdentificacion);
            elementEmisor.AppendChild(elementEmisorUbicacion);
            elementEmisor.AppendChild(elementEmisorTelefono);
            elementEmisor.AppendChild(elementEmisorCorreoElectronico);

            element1.AppendChild(elementEmisor);


            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(doc.OuterXml);
            var encode = System.Convert.ToBase64String(plainTextBytes);

            return encode;
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
