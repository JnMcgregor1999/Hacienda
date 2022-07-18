using System;
using System.Threading.Tasks;
using Utn.Hacienda.Backend.Common;
using static Utn.Hacienda.Backend.Common.Enum;
using Utn.Hacienda.Backend.Utilities;
using Utn.Hacienda.Backend.DataAccess.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                string searchKeyIsProdSignature = "IS_PROD_SIGNATURE";
                string searchKeyApiSignature = "API_SIGNATURE";
                bool isProdSignature = false;
                string apiSignature = "";

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
                }

                // Validamos que el e-commerce este registrado en hacienda
                var userRepository = new Mtr_User_Repository(message.Connection);
                var userResponse = userRepository.Get(new Common.Mtr_User { Identification = model.Item.Commercial_Identification }).Result;

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
                    await invoiceRepository.Save(modelInvoice);


                    // Creamos el objeto parametros para mandarlo a firma
                    var modelSignature = new Common.IExternalSignature();
                    modelSignature.Item.New_File = model.Item.New_File;
                    modelSignature.Item.Public_Key = userResponse.Public_Key;


                    if (isProdSignature)
                    {
                        // Mandamos a llamar el api de firma
                        var signatureResponse = SendRequest.InvokePost("url", "method", "parameters");
                        Common.IExternalSignature signature_Response_Deserialization = JsonConvert.DeserializeObject<Common.IExternalSignature>(signatureResponse);

                        // Crear el objeto de retorno
                        var signature_Response = new
                        {
                            item = new
                            {
                                New_File = signature_Response_Deserialization.Item.New_File,
                                code = 200,
                                message = "success"
                            }
                        };

                        resultMessage.Status = Status.Success;
                        resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                        resultMessage.MessageInfo = signature_Response.SerializeObject();
                        return resultMessage;
                    }
                    else
                    {
                        // Crear el objeto de retorno dummy
                        var signature_Response = new
                        {
                            item = new
                            {
                                New_File = "PEZhY3R1cmFFbGVjdHJvbmljYSB4bWxucz0iaHR0cHM6Ly9jZG4uY29tcHJvYmFudGVzZWxlY3Ryb25pY29zLmdvLmNyL3htbC1zY2hlbWFzL3Y0LjMvZmFjdHVyYUVsZWN0cm9uaWNhIiB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiB4bWxuczpkc2lnPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIiB4c2k6c2NoZW1hTG9jYXRpb249Imh0dHBzOi8vY2RuLmNvbXByb2JhbnRlc2VsZWN0cm9uaWNvcy5nby5jci94bWwtc2NoZW1hcy92NC4zL2ZhY3R1cmFFbGVjdHJvbmljYSBodHRwczovL3d3dy5oYWNpZW5kYS5nby5jci9BVFYvQ29tcHJvYmFudGVFbGVjdHJvbmljby9kb2NzL2VzcXVlbWFzLzIwMTYvdjQuMy9GYWN0dXJhRWxlY3Ryb25pY2FfVjQuMy54c2QiPg0KPENsYXZlPjUwNjA0MDYyMjAwMzEwMTA3NDE1NDAxODAxODA0MDEwMDAwMDQ5MDg2MTc3MDY3NjU0PC9DbGF2ZT4NCjxDb2RpZ29BY3RpdmlkYWQ+NTIzMzA0PC9Db2RpZ29BY3RpdmlkYWQ+DQo8TnVtZXJvQ29uc2VjdXRpdm8+MDE4MDE4MDQwMTAwMDAwNDkwODY8L051bWVyb0NvbnNlY3V0aXZvPg0KPEZlY2hhRW1pc2lvbj4yMDIyLTA2LTA0VDIyOjM4OjExLTA2OjAwPC9GZWNoYUVtaXNpb24+DQo8RW1pc29yPg0KPE5vbWJyZT5VbmlvbiBDb21lcmNpYWwgRGUgQ29zdGEgUmljYS4gVW5pY29tZXIgUy5BLjwvTm9tYnJlPg0KPElkZW50aWZpY2FjaW9uPg0KDQo8TnVtZXJvPjMxMDEwNzQxNTQ8L051bWVybz4NCjwvSWRlbnRpZmljYWNpb24+DQo8Tm9tYnJlQ29tZXJjaWFsPkcgTyBMIEwgTzwvTm9tYnJlQ29tZXJjaWFsPg0KPFViaWNhY2lvbj4NCjxQcm92aW5jaWE+MjwvUHJvdmluY2lhPg0KPENhbnRvbj4wMTwvQ2FudG9uPg0KPERpc3RyaXRvPjAyPC9EaXN0cml0bz4NCjxCYXJyaW8+MDE8L0JhcnJpbz4NCjxPdHJhc1NlbmFzPjgwMCBPZXN0ZSBEZSBSdHYgRW4gQ295b2wgQWxhanVlbGE8L090cmFzU2VuYXM+DQo8L1ViaWNhY2lvbj4NCjxUZWxlZm9ubz4NCg0KPE51bVRlbGVmb25vPjI0Mzc0ODQ4PC9OdW1UZWxlZm9ubz4NCjwvVGVsZWZvbm8+DQoNCjxDb3JyZW9FbGVjdHJvbmljbz5jcl9mZUB1bmljb21lci5jb208L0NvcnJlb0VsZWN0cm9uaWNvPg0KPC9FbWlzb3I+DQo8UmVjZXB0b3I+DQo8Tm9tYnJlPkFSSUFTIENBTVBPUyBEWUxBTiBBTkRSRVk8L05vbWJyZT4NCjxJZGVudGlmaWNhY2lvbj4NCjxUaXBvPjAxPC9UaXBvPg0KPE51bWVybz42MDQ0MTA5MTc8L051bWVybz4NCjwvSWRlbnRpZmljYWNpb24+DQo8Q29ycmVvRWxlY3Ryb25pY28+ZGlsYW5hcmlhczU3QGdtYWlsLmNvbTwvQ29ycmVvRWxlY3Ryb25pY28+DQo8L1JlY2VwdG9yPg0KDQoNCjxEZXRhbGxlU2VydmljaW8+DQo8TGluZWFEZXRhbGxlPg0KPE51bWVyb0xpbmVhPjE8L051bWVyb0xpbmVhPg0KPENvZGlnbz43MTE5MDk5MDAwMDAwPC9Db2RpZ28+DQo8Q29kaWdvQ29tZXJjaWFsPg0KPFRpcG8+MDE8L1RpcG8+DQo8Q29kaWdvPjEwMDAzPC9Db2RpZ28+DQo8L0NvZGlnb0NvbWVyY2lhbD4NCjxDYW50aWRhZD4xLjAwMDwvQ2FudGlkYWQ+DQo8VW5pZGFkTWVkaWRhPkk8L1VuaWRhZE1lZGlkYT4NCg0KPFByZWNpb1VuaXRhcmlvPjIzNTkuNzAwMDA8L1ByZWNpb1VuaXRhcmlvPg0KPE1vbnRvVG90YWw+MjM1OS43MDAwMDwvTW9udG9Ub3RhbD4NCjxTdWJUb3RhbD4yMzU5LjcwMDAwPC9TdWJUb3RhbD4NCjxJbXB1ZXN0b0lWQT4wLjAwMDAwPC9JbXB1ZXN0b0lWQT4NCjxNb250b1RvdGFsTGluZWE+MjM1OS43MDAwMDwvTW9udG9Ub3RhbExpbmVhPg0KPC9MaW5lYURldGFsbGU+DQo8L0RldGFsbGVTZXJ2aWNpbz4NCg0KPFJlc3VtZW5GYWN0dXJhPg0KPENvZGlnb1RpcG9Nb25lZGE+DQo8Q29kaWdvTW9uZWRhPkNSQzwvQ29kaWdvTW9uZWRhPg0KPFRpcG9DYW1iaW8+MS4wMDAwMDwvVGlwb0NhbWJpbz4NCjwvQ29kaWdvVGlwb01vbmVkYT4NCg0KPFRvdGFsQnJ1dG8+MC4wMDAwMDwvVG90YWxCcnV0bz4NCjxUb3RhbElWQT4wLjAwMDAwPC9Ub3RhbElWQT4NCjxUb3RhbE5ldG8+ODQwLjMwMDAwPC9Ub3RhbE5ldG8+DQo8L1Jlc3VtZW5GYWN0dXJhPg0KDQo8ZmlybWE+DQo8ZmlybWFWYWx1ZSBJZD0iU2lnbmF0dXJlVmFsdWUtYTYwNTY1NDYtMGFlMy00NmM1LWIyMWMtNjU4MjUzMTE5ZmJiIj5WMUhjQ21vNFIrQ0NhRUtxYlJCNHVicEZrWkRjVENNYUFCT0dZSGxsRGZVd1MrSkcrY05uVll6cUxTcEhmdnJTK3J5L3cyQkptLy9HQjA2NGJmM05SQkh6WGlQbTRMVElaNnVxeVlFeFczN3RGWjd1TDZTUy9Od0NHTUhWZktheXYwWlhoUm1FbjdWcHJZb0FDc3V5cFhrWXgvdlEyVUJPb1lMckZrQlhybDRrVkRNenB6UnBkVlErZzNJKzhabE1xZ1YwcTRPYytnd2JiNExVQkg1MlNIK3Z5WGJHaUkzdkZWOE14V1l1RDh1bFJ2dWtZZU9yVFUzM3doaUpXQWNuT2MyMkkrUlVNOGRoWDJNbHZWT1dXT2tpU255YWQwaHRZa1k5blB3bzVXNkExQUZ3K0MrSE45TXNsODVYUElpT2NMb1VSUkxBcmVqa3ArZjdreGR2WHc9PTwvZmlybWFWYWx1ZT4NCjxYNTA5RGF0YT4NCjxYNTA5Q2VydGlmaWNhdGU+TUlJRk9qQ0NBeUtnQXdJQkFnSUdBWFF0QWZyUk1BMEdDU3FHU0liM0RRRUJDd1VBTUZveEN6QUpCZ05WQkFZVEFrTlNNUjh3SFFZRFZRUUtEQlpOU1U1SlUxUkZVa2xQSUVSRklFaEJRMGxGVGtSQk1Rd3dDZ1lEVlFRTERBTkVSMVF4SERBYUJnTlZCQU1NRTBOQklGQkZVbE5QVGtFZ1NsVlNTVVJKUTBFd0hoY05NakF3T0RJMk1qTXdNakU0V2hjTk1qSXdPREkyTWpNd01qRTRXakNCbERFWk1CY0dBMVVFQlJNUVExQktMVE10TVRBeExUQTNOREUxTkRFTE1Ba0dBMVVFQmhNQ1ExSXhHVEFYQmdOVkJBb01FRkJGVWxOUFRrRWdTbFZTU1VSSlEwRXhEREFLQmdOVkJBc01BME5RU2pGQk1EOEdBMVVFQXd3NFZVNUpUMDRnUTA5TlJWSkRTVUZNSUVSRklFTlBVMVJCSUZKSlEwRXNJRlZPU1VOUFRVVlNJRk5QUTBsRlJFRkVJRUZPVDA1SlRVRXdnZ0VpTUEwR0NTcUdTSWIzRFFFQkFRVUFBNElCRHdBd2dnRUtBb0lCQVFDSmZEckFvM0lva2lOSmxvNCt2Zi9oLzkrMmlOcjNza21BSEpzdkJENng1SmtUVzZyUUFOQkJDcjlCcHZpN1kvRGFxMFZuWVZINit0S01aZ0IvOElFT09vbFNDT1ZCaTFpeDhhS09HbkNXb1ZmY2tVd1RZZWV0SGVGaUxnL01VdUgyNGF6RndKQkx1TW15OG5EQ2JBNGgyaEk1bUpyQXZqa1NvVG0rVXRLQ1FCbEZValJnQ0loVHNVbUpXd2ZTT0NHQ2RDZ3dLQTZ6VGFZT1VuM0NzZW1HUW02Nm11RHRGbUVrL0svbTh3QXNpT1FBL1NQcTJhNW83UnhHQ2ovOHNFYnpXU2RualZMZmZCZ25pRGVWWjNHVFFXK1NzL0hjZWI4NGc0NUhoR2FqbmFBQWRJL1BHNndFaENDemgrbnptcEhUeDBNSnc5YWd5UWVycW15S09qNG5BZ01CQUFHamdjb3dnY2N3SHdZRFZSMGpCQmd3Rm9BVU8yMVg5ZFo4Nlo0UUFpQkI0NzBhek0relRwVXdIUVlEVlIwT0JCWUVGRTdpVGJuRWxuWEMvaXB5T1NYRElnUmhPVDdkTUFzR0ExVWREd1FFQXdJR3dEQVRCZ05WSFNVRUREQUtCZ2dyQmdFRkJRY0RCREJqQmdnckJnRUZCUWNCQVFSWE1GVXdVd1lJS3dZQkJRVUhNQUtHUjJoMGRIQnpPaTh2Y0d0cExtTnZiWEJ5YjJKaGJuUmxjMlZzWldOMGNtOXVhV052Y3k1bmJ5NWpjaTl3Y205a0wybHVkR1Z5YldWa2FXRjBaUzF3YWkxd1pXMHVZM0owTUEwR0NTcUdTSWIzRFFFQkN3VUFBNElDQVFDSXkvUGFXd3dLRGx6TUdsblRvK2tDdExxb0h1Rms3T0FJYlQ5Szc4aERiR0ZDbHU3bWFLV3lnNk5DalVuR0NaRkZtdHJDN0tyc0VPUUpneEtiUmVScEJTWENUblM1RHptTFh0WHgxcVAxTlFHL0M0eEQ4N090UW1MWlVBaStEdzUzMUx5MmFhWGZDc21KNHpWNkhkQUpyMmE0RGloajBuT3ZrLzJKd3UvblVtNzRKNGNucVdSNU1JWFprU0Zac2hFdStZd3RiWDdBSmhmVWVzcTY3NHRzS1VzbXVvL2lzd0pnUmpqSDE3U3o3dmhZeVVDSkg1Ym4waWprUlRGbGIyanFtQmxUdUR6Q3FLek83N0QrS3NhQWpoaEg5aUZhNnJ6azlBUnRYZ0dZZ3h3dnhmMitqV1MvU3lKQUxmVjJQNnR0RWFhZHFGaVE3UTBiUEoyYUNaR3AvRHRDblJHWi9idzJ1SFR0UWNCd245RmcrWUZXRVVuc3NlMUdBYU1QRHdhclBlZ0FlaFppcFdBMFJNck0rOW5uU0lpbkdyZEMzWml2NVkrdkxOQUF6a1FaRk9nZTNHTkttRUR6MkF1V21xL1FtTnBVb2NjWmorTlRwMVVZVkVRQTI3SWlmSEdHZFppNjFRSnRYSmtNdEdmRkd2RHV2bTZJSEVvYmVndEcwSDBId0tpTFlpRFNLZzlqcXRaSm91TUJibEtGWU1QS2RWTGRXU2JoSmphMlFJNTdTV3VEb1A0UFRnd0FZVGV3dldkc1NWcFB6WVV4dm0yT21mclh0RkYzbmMyZXV3TE9STDgwOVhXalNzY3R0S3pjVk9sb2gxbE1uQkdGTUVZUUxuczZMNlUwUXFuYWVuZUZMS05Cd1BsTzN2UVhUWENGL0w1VmRmWDd6UUxBZWc9PTwvWDUwOUNlcnRpZmljYXRlPg0KPC9YNTA5RGF0YT4NCjwvZmlybWE+DQo8L0ZhY3R1cmFFbGVjdHJvbmljYT4=",
                                code = 200,
                                message = "success"
                            }
                        };
                        resultMessage.Status = Status.Success;
                        resultMessage.Result = "Proceso efectuado satisfactoriamente...";
                        resultMessage.MessageInfo = signature_Response.SerializeObject();
                        return resultMessage;
                    }
                }
                else
                {
                    resultMessage.Status = Status.Failed;
                    resultMessage.Result = "No se encontro al cliente";
                    resultMessage.MessageInfo = "";
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
