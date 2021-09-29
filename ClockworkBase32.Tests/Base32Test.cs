using System.Text;
using Xunit;
using ClockworkBase32;

namespace ClockworkBase32.Tests
{
    public class Base32Test
    {
        [Theory]
        [MemberData(nameof(testCases))]
        public void TestEncode(string plain, string encoded)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plain);
            Assert.Equal(encoded, Base32.Encode(plainBytes));
        }

        [Theory]
        [MemberData(nameof(testCases))]
        public void TestDecode(string plain, string encoded)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plain);
            Assert.Equal(plainBytes, Base32.Decode(encoded));
        }

        public static object[][] testCases = new object[][]
        {
            new[] { "", "" },
            new[] { "f", "CR" },
            new[] { "foobar", "CSQPYRK1E8" },
            new[] { "Hello, world!", "91JPRV3F5GG7EVVJDHJ22" },
            new[] { "The quick brown fox jumps over the lazy dog.", "AHM6A83HENMP6TS0C9S6YXVE41K6YY10D9TPTW3K41QQCSBJ41T6GS90DHGQMY90CHQPEBG" },
        };
    }
}
