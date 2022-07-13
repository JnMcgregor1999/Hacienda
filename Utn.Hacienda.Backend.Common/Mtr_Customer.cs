using System;
namespace Utn.Hacienda.Backend.Common
{
    public class Mtr_Customer
    {
        public Mtr_Customer()
        {
            Pk_Mtr_Customer = 0;
            Creation_User = "";
            Creation_Date = Convert.ToDateTime("1900-01-01");
            Modification_User = "";
            Modification_Date = Convert.ToDateTime("1900-01-01");
            Company_Name = "";
            Identification_Type = 0;
            Identification = "";
            Social_Reazon = "";
            Commercial_Name = "";
            Province = 0;
            Canton = 0;
            District = 0;
            Email = "";
            Telephone = "";
            Address = "";
            Active = false;
        }
        public Int64 Pk_Mtr_Customer { get; set; }
        public String Creation_User { get; set; }
        public DateTime Creation_Date { get; set; }
        public String Modification_User { get; set; }
        public DateTime Modification_Date { get; set; }
        public String Company_Name { get; set; }
        public Int64 Identification_Type { get; set; }
        public String Identification { get; set; }
        public String Social_Reazon { get; set; }
        public String Commercial_Name { get; set; }
        public Int64 Province { get; set; }
        public Int64 Canton { get; set; }
        public Int64 District { get; set; }
        public String Email { get; set; }
        public String Telephone { get; set; }
        public String Address { get; set; }
        public Boolean Active { get; set; }
    }
}
