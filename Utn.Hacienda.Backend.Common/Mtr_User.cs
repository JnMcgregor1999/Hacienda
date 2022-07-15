using System;
namespace Utn.Hacienda.Backend.Common
{
    public class Mtr_User
    {
        public Mtr_User()
        {
            Pk_Mtr_User = 0;
            Creation_User = "";
            Creation_Date = Convert.ToDateTime("1900-01-01");
            Modification_User = "";
            Modification_Date = Convert.ToDateTime("1900-01-01");
            Fk_Catalog_Identification_Type = 0;
            Identification = "";
            Full_Name = "";
            Email = "";
            Password = "";
            Active = false;
            Public_Key = "";
        }
        public Int64 Pk_Mtr_User { get; set; }
        public String Creation_User { get; set; }
        public DateTime Creation_Date { get; set; }
        public String Modification_User { get; set; }
        public DateTime Modification_Date { get; set; }
        public Int64 Fk_Catalog_Identification_Type { get; set; }
        public String Identification { get; set; }
        public String Full_Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Boolean Active { get; set; }
        public String Public_Key { get; set; }
    }
}
