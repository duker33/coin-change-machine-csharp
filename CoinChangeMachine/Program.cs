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
            var sortedCoins = coins.OrderByDescending(x => x.value);
            var result = new List<(int value, int count)>();

            using (var enumerator = sortedCoins.GetEnumerator())
            {
                while (enumerator.MoveNext() && sum > 0)
                {
                    var value = enumerator.Current.value;
                    var count = Math.Max(sum / value, enumerator.Current.count);
                    sum -= value * count;

                    if (sum < 0)
                        return new (int value, int count)[0];

                    result.Add((value, count));
                }
            }

            return result;
        }

        public static IEnumerable<(int value, int count)> ReadCoins(TextReader reader)
        {
            var n = int.Parse(reader.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var coin = reader.ReadLine()
                                 .Split(' ');

                Debug.Assert(coin.Length == 2);
                var value = int.Parse(coin[0]);
                var count = int.Parse(coin[1]);

                yield return (value, count);
            }
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
