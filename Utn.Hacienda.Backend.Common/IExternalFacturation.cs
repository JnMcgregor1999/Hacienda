using System;
namespace Utn.Hacienda.Backend.Common
{
    public class IExternalFacturation
    {
        public IExternalFacturation()
        {
            Item = new IFacturation();

        }
        public IFacturation Item { get; set; }
    }
}
