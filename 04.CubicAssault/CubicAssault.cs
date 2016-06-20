namespace _04.CubicAssault
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class CubicAssault
    {
        private const string End = "Count em all";
        private const int ValueBreakpoint = 1000000;
        private const string BestType = "Black";

        private static readonly SortedDictionary<string, SortedDictionary<string, long>> Statistics =
            new SortedDictionary<string, SortedDictionary<string, long>>();

        internal enum SoldierType
        {
            Green = 0,
            Red = 1,
            Black = 2
        }

        public static void Main()
        {
            var input = Console.ReadLine();
            while (input != End)
            {
                var tokens = Regex.Split(input, @"\s+\-\>\s+");

                var regionName = tokens[0];
                var soldierType = tokens[1];
                var soldiersCount = long.Parse(tokens[2]);

                if (!Statistics.ContainsKey(regionName))
                {
                    Statistics[regionName] = new SortedDictionary<string, long>();
                    for (int i = 0; i < Enum.GetNames(typeof(SoldierType)).Length; i++)
                    {
                        Statistics[regionName].Add(((SoldierType)i).ToString(), 0);
                    }
                }

                RecalculateValues(regionName, soldierType, soldiersCount);

                input = Console.ReadLine();
            }

            SortAndPrint();
        }

        private static void SortAndPrint()
        {
            var regions = Statistics
                .OrderByDescending(i => i.Value[BestType])
                .ThenBy(item => item.Key.Length);

            foreach (var region in regions)
            {
                Console.WriteLine(region.Key);

                var stats = region.Value
                    .OrderByDescending(t => t.Value);

                foreach (var stat in stats)
                {
                    Console.WriteLine($"-> {stat.Key} : {stat.Value}");
                }
            }
        }

        private static void RecalculateValues(string regionName, string soldierType, long soldiersCount)
        {
            while (true)
            {
                var totalCount = Statistics[regionName][soldierType] + soldiersCount;

                if (soldierType == SoldierType.Black.ToString() ||
                    totalCount < ValueBreakpoint)
                {
                    // All good - increase the value
                    Statistics[regionName][soldierType] = totalCount;
                    break;
                }

                var newValue = totalCount % ValueBreakpoint;
                soldiersCount = (totalCount - newValue) / ValueBreakpoint;

                var currentTypeIndex = (int)Enum.Parse(typeof(SoldierType), soldierType);
                Statistics[regionName][((SoldierType)currentTypeIndex).ToString()] = newValue;
                soldierType = ((SoldierType)currentTypeIndex + 1).ToString();
            }
        }
    }
}