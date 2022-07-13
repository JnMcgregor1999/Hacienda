using System;
namespace Utn.Hacienda.Backend.Common
{
    public class ISignature
    {
        public ISignature()
        {
            New_File = "";
            Public_Key = "";
        }
        public String New_File { get; set; }
        public String Public_Key { get; set; }
    }
}
