using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Core
{
    public enum Operation
    {
        None = 0,
        FillBucket1,
        FillBucket2,
        DumpBucket1,
        DumpBucket2,
        TransferBucket1ToBucket2,
        TransferBucket2ToBucket1
    }

    /// <summary>
    /// Helper class for the solver
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Helper method to convert the provided enumeration to a useful string for display
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static string ToDetails(this Operation operation)
        {
            switch (operation)
            {
                case Operation.FillBucket1:
                    return "Fill bucket 1";
                case Operation.FillBucket2:
                    return "Fill bucket 2";
                case Operation.DumpBucket1:
                    return "Dump bucket 1";
                case Operation.DumpBucket2:
                    return "Dump bucket 2";
                case Operation.TransferBucket1ToBucket2:
                    return "Transfer bucket 1 to bucket 2";
                case Operation.TransferBucket2ToBucket1:
                    return "Transfer bucket 2 to bucket 1";
            }

            return string.Empty;
        }
    }
}
