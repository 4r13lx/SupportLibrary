using System;
using System.ComponentModel;

namespace SupportLibraryTest.Entities
{
    public enum TestEnumSimple
    {
        Unknown = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 3
    }

    public enum TestEnumDescription
    {
        [Description("Unknown")]
        Unknown = 0,

        [Description("One")]
        Value1 = 1,

        [Description("Two")]
        Value2 = 2,

        [Description("Three")]
        Value3 = 3
    }
}
