namespace CoinChangeMachine.Tests
{
    using System.IO;
    using System.Linq;
    using Xunit;

    public class ProgramTests
    {
        [Fact]
        public void MakeChange_WithImpossibleSum_ReturnsEmptyArray()
        {
            var actual = Program.MakeChange(new[] {(5, 2), (3, 2)}, 4);

            Assert.Empty(actual);
        }

        [Fact]
        public void MakeChange_WithPossibleSum_ReturnsMaximalValueCoinsFirst()
        {
            var actual = Program.MakeChange(new[] { (5, 4), (2, 6) }, 10);

            Assert.Equal(new[] {(5, 2)}, actual);
        }

        [Fact]
        public void ReadCoins_WithZeroN_ReturnsEmptyEnumerable()
        {
            using (var reader = new StringReader("0"))
            {
                var coins = Program.ReadCoins(reader)
                                   .ToArray();

                Assert.Empty(coins);
            }
        }

        [Fact]
        public void ReadCoins_WithPositiveN_ReturnsNElements()
        {
            using (var reader = new StringReader("3\n1 100\n2 200\n3 300"))
            {
                var coins = Program.ReadCoins(reader)
                                   .ToArray();

                Assert.Equal(1, coins[0].value);
                Assert.Equal(100, coins[0].count);

                Assert.Equal(2, coins[1].value);
                Assert.Equal(200, coins[1].count);

                Assert.Equal(3, coins[2].value);
                Assert.Equal(300, coins[2].count);
            }
        }

        [Fact]
        public void WriteCoins_WithEmptyCollection_PrintsZero()
        {
            using (var writer = new StringWriter())
            {
                writer.NewLine = "\n";
                Program.WriteCoins(new (int value, int count)[0], writer);

                Assert.Equal("0\n", writer.ToString());
            }
        }

        [Fact]
        public void WriteCoins_WithNonEmptyCollection_PrintsPairs()
        {
            using (var writer = new StringWriter())
            {
                writer.NewLine = "\n";
                Program.WriteCoins(new[] {(1, 100), (2, 200), (3, 300)}, writer);

                Assert.Equal("1 100\n2 200\n3 300\n", writer.ToString());
            }
        }
    }
}
