using System;
namespace Utn.Hacienda.Backend.Common
{
    public class Mtr_Invoice
    {
        public Mtr_Invoice()
        {
            Pk_Mtr_Invoice = 0;
            Creation_User = "";
            Creation_Date = Convert.ToDateTime("1900-01-01");
            Modification_User = "";
            Modification_Date = Convert.ToDateTime("1900-01-01");
            Fk_Mtr_Customer = 0;
            Fk_Mtr_User = 0;
            Reference_Number = "";
            Invoice_Url = "";
            Active = false;
        }
        public Int64 Pk_Mtr_Invoice { get; set; }
        public String Creation_User { get; set; }
        public DateTime Creation_Date { get; set; }
        public String Modification_User { get; set; }
        public DateTime Modification_Date { get; set; }
        public Int64 Fk_Mtr_Customer { get; set; }
        public Int64 Fk_Mtr_User { get; set; }
        public String Reference_Number { get; set; }
        public String Invoice_Url { get; set; }
        public Boolean Active { get; set; }
    }
}
