using System;
namespace Utn.Hacienda.Backend.Common
{
    public class IExternalSignature
    {
        public IExternalSignature()
        {
            Item = new ISignature();
        }
        public ISignature Item { get; set; }
    }
}
