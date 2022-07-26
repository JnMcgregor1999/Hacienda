using System;
namespace Utn.Hacienda.Backend.Common
{
    public class Par_Parameter
    {
        public Par_Parameter()
        {
            Pk_Par_Parameter = 0;
            Creation_User = "";
            Creation_Date = Convert.ToDateTime("1900-01-01");
            Modification_User = "";
            Modification_Date = Convert.ToDateTime("1900-01-01");
            Search_Key = "";
            Parameter_Value = "";
            Active = false;
        }
        public Int64 Pk_Par_Parameter { get; set; }
        public String Creation_User { get; set; }
        public DateTime Creation_Date { get; set; }
        public String Modification_User { get; set; }
        public DateTime Modification_Date { get; set; }
        public String Search_Key { get; set; }
        public String Parameter_Value { get; set; }
        public Boolean Active { get; set; }
    }
}
