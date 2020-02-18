using System;
using Xunit;

namespace ShopCart.Tests.Utils
{
    public static class ID
    {
        public static long Counter = 0;

        public static string Generate()
        {
            return (DateTime.Now.ToBinary() - new DateTime(2019, 1, 1).ToBinary() + Counter++)
                .ToString("X");
        }
    }

    public class IDTest
    {
        [Fact]
        public void TestIDGeneration()
        {
            var id = ID.Generate();
            var id2 = ID.Generate();
            var id3 = ID.Generate();
            Assert.NotEmpty(id);
            Assert.NotEmpty(id2);
            Assert.NotEmpty(id3);
            Assert.NotEqual(id, id2);
            Assert.NotEqual(id2, id3);
            Assert.NotEqual(id3, id);
        }
    }
}