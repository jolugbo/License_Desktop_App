using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKLicense.Utilities
{
    public static class ActivityStatus
    {
        public static Guid NotCompleted { get { return Guid.Parse("3FF5C4B6-7307-497A-BCCC-29895EB545B4"); } }
        public static Guid ReturnedRequest { get { return Guid.Parse("E506F0B4-AE5E-45E4-B8CC-7079896B18AB"); } }
       
        public static Guid Completed { get { return Guid.Parse("83EE1EF5-9490-4CED-8E13-87BD08C09CB7"); } }
        public static Guid Transmitted { get { return Guid.Parse("BF665DF4-FBCF-442D-B85E-9741BFD68F11"); } }
        public static Guid ReadyForCalculation { get { return Guid.Parse("43304EEA-9BB8-4FE5-BC2B-7592BCF14611"); } }
        


    }
}
