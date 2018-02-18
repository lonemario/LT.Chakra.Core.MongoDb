using System;
using ZenProgramming.Chakra.Core.Entities;

namespace LT.TEST.Chakra.Core.MongoDb.Entities
{
    public class TestClass : ModernEntityBase
    {
        public int Property1 { get; set; }

        public string Property2 { get; set; }

        public DateTime Property3 { get; set; }

        public byte[] Property4 { get; set; }

        public DateTime? Property5 { get; set; }

        public byte?[] Property6 { get; set; }
    }
}
