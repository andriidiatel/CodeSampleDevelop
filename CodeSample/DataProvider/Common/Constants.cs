using System;
using System.Collections.Generic;
using System.Text;

namespace DataProvider.Common
{
    /// <summary>
    /// All global constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Data entityes
        /// </summary>
        public static class Entities
        {
            public static class Plane
            {
                public const string Name = "PlaneEntity";

                public static class Columns
                {
                    public const string Manufacturer = "Manufacturer";
                    public const string Registration = "Registration";
                    public const string Type = "Type";
                    public const string ModeSCode = "ModeSCode";
                }
            }

            public static class FlightInfo
            {
                public const string Name = "FlightInfoEntity";

                public static class Columns
                {

                }
            }
        }
    }
}
