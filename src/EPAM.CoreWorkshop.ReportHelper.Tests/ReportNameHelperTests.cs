using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;

namespace EPAM.Core.ReportHelper.Tests
{
    [TestClass]
    public class ReportNameHelperTests
    {
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            //System.Threading.Thread.Sleep(30 * 1000);
        }

        [TestMethod]
        [DynamicData("NormalizeFileNameTestData", DynamicDataSourceType.Property)]
        public void NormalizeFileNameTest(string name, char repl, string expected)
        {
            var result = ReportNameHelper.NormalizeFileName(name, repl);

            result.Should().BeEquivalentTo(expected);
        }

        static object[][] NormalizeFileNameTestData
        {
            get
            {
                object[][] data = new object[][]
                {
            new object[]{"Hello: my world", '_', "Hello_ my world"},
            new object[]{"Hello my %world", '_', "Hello my %world"},
            new object[]{"Hello my /world", '_', "Hello my _world"}
                };

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    data[0][2] = "Hello: my world";
                    data[1][2] = "Hello my %world";
                    data[2][2] = "Hello my _world";
                }
                return data;
            }
        }
    }
}