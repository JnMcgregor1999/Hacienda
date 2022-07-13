using System;
namespace Utn.Hacienda.Backend.Common
{
    public class IFacturation
    {
        public IFacturation()
        {
            Reference_Number = "";
            Client_Name = "";
            File_Path = "";
            New_File = "";
        }
        public String Reference_Number { get; set; }
        public String Client_Name { get; set; }
        public String File_Path { get; set; }
        public String New_File { get; set; }
    }
}
