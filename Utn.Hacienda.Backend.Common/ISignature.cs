using System;
namespace Utn.Hacienda.Backend.Common
{
    public class ISignature
    {
        public ISignature()
        {
            isValid = false;
            code = 0;
            message = "";
            Public_Key = "";
        }
        public String New_File { get; set; }
        public String Public_Key { get; set; }
        public bool isValid { get; set; }
        public int code { get; set; }
        public String message { get; set; }
    }
}
