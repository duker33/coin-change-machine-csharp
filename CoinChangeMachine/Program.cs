namespace CoinChangeMachine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public class Program
    {
        static void Main()
        {
            var coins = ReadCoins(Console.In);
            var sum = int.Parse(Console.ReadLine());
            var change = MakeChange(coins, sum);
            WriteCoins(change, Console.Out);
        }

        public static IReadOnlyCollection<(int value, int count)> MakeChange(IEnumerable<(int value, int count)> coins, int sum)
        {
            try
            {
                var sortedCoins = coins.OrderByDescending(x => x.value);

                return BuildChange(sortedCoins, sum).ToArray();
            }
            catch (InvalidOperationException)
            {
                return new (int value, int count)[0];
            }
        }

        private static IEnumerable<(int value, int count)> BuildChange(IOrderedEnumerable<(int value, int count)> sortedCoins, int sum)
        {
            foreach (var coin in sortedCoins)
            {
                var count = Math.Min(sum / coin.value, coin.count);

                if (count == 0)
                    continue;

                sum -= coin.value * count;

                yield return (coin.value, count);

                if (sum == 0)
                    yield break;
            }

            if (sum > 0)
                throw new InvalidOperationException();
        }

        public static (int value, int count)[] ReadCoins(TextReader reader)
        {
            var n = int.Parse(reader.ReadLine());
            var result = new (int value, int count)[n];
            for (int i = 0; i < n; i++)
            {
                var coin = reader.ReadLine()
                                 .Split(' ');

                Debug.Assert(coin.Length == 2);

                result[i].value = int.Parse(coin[0]);
                result[i].count = int.Parse(coin[1]);
            }

            return result;
        }

        public static void WriteCoins(IReadOnlyCollection<(int value, int count)> coins, TextWriter writer)
        {
            if (coins.Count > 0)
            {
                foreach (var coin in coins)
                    writer.WriteLine("{0} {1}", coin.value, coin.count);
            }
            else
                writer.WriteLine("0");
        }
    }
}
