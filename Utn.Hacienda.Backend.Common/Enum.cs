using System;

namespace Utn.Hacienda.Backend.Common
{
    public class Enum
    {
        public enum Operation
        {
            Save,
            List,
            Get,
            Delete,
            SaveGet,
            ValidateFile
        }

        public enum Status
        {
            Success,
            Failed
        }

    }

}