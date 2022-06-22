using System;
namespace Utn.Hacienda.Backend.Common
{
    public class Gbl_Type_Catalog
    {
        public Gbl_Type_Catalog()
        {
            Pk_Gbl_Type_Catalog = 0;
            Creation_User = "";
            Creation_Date = Convert.ToDateTime("1900-01-01");
            Modification_User = "";
            Modification_Date = Convert.ToDateTime("1900-01-01");
            Name = "";
            Description = "";
            Active = false;
        }
        public Int64 Pk_Gbl_Type_Catalog { get; set; }
        public String Creation_User { get; set; }
        public DateTime Creation_Date { get; set; }
        public String Modification_User { get; set; }
        public DateTime Modification_Date { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Boolean Active { get; set; }
    }
}
