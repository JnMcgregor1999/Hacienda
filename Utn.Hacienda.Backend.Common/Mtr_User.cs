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
            User_Name = "";
            Password = "";
        }
        public Int64 Pk_Mtr_User { get; set; }
        public String Creation_User { get; set; }
        public DateTime Creation_Date { get; set; }
        public String Modification_User { get; set; }
        public DateTime Modification_Date { get; set; }
        public String User_Name { get; set; }
        public String Password { get; set; }
    }
}
