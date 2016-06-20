namespace _02.CubicsRube
{
    using System;
    using System.Linq;

    public static class CubicsRube
    {
        private const string End = "Analyze";
        private static int cubeSize;

        public static void Main()
        {
            cubeSize = int.Parse(Console.ReadLine());

            var sum = 0L;
            var untouchedCells = Math.Pow(cubeSize, 3);

            var input = Console.ReadLine().Split();
            while (input[0] != End)
            {
                var tokens = input.Select(long.Parse).ToArray();

                var pointX = tokens[0];
                var pointY = tokens[1];
                var pointZ = tokens[2];
                var value = tokens[3];

                if (AllPoitsAreInsideTheCube(pointX, pointY, pointZ))
                {
                    sum += value;

                    if (value != 0)
                    {
                        untouchedCells--;
                    }
                }

                input = Console.ReadLine().Split();
            }

            Console.WriteLine(sum);
            Console.WriteLine(untouchedCells);
        }

        public static bool AllPoitsAreInsideTheCube(params long[] points)
        {
            foreach (var point in points)
            {
                if (!(point >= 0 && point <= cubeSize))
                {
                    return false;
                }
            }

            return true;
        }
    }
}