namespace _01.CubicArtillery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CubicArtillery
    {
        private const string End = "Bunker";

        private static readonly Queue<Bunker> Bunkers = new Queue<Bunker>();

        public static void Main()
        {
            var bunkerCapacity = int.Parse(Console.ReadLine());
            var inputLine = Console.ReadLine().Split();
            while (inputLine[0] != End)
            {
                foreach (var token in inputLine)
                {
                    if (char.IsLetter(token[0]))
                    {
                        AddBunker(token, bunkerCapacity);
                    }
                    else
                    {
                        AddWeaponToBunker(int.Parse(token));
                    }
                }

                inputLine = Console.ReadLine().Split();
            }
        }

        private static void AddBunker(string bunkerName, int bunkerCapacity)
        {
            Bunkers.Enqueue(new Bunker(bunkerName, bunkerCapacity));
        }

        private static void AddWeaponToBunker(int weapon)
        {
            while (true)
            {
                var bunker = Bunkers.Peek();

                if (!bunker.CanWeaponFitIn(weapon))
                {
                    if (!IsLastBunker())
                    {
                        // It's not the last bunker and it's overflowed - lets remove it, and try the next one
                        Console.WriteLine(bunker);
                        Bunkers.Dequeue();
                    }
                    else
                    {
                        if (bunker.CanWeaponFitInWhenBunkerCleaned(weapon))
                        {
                            // Weapon can fit in, after we clean up a bit the buker.
                            bunker.AddBigerWeapon(weapon);
                        }

                        break;
                    }
                }
                else
                {
                    // Weapon is small enough to fit in - lets add it.
                    bunker.AddWeapon(weapon);
                    break;
                }
            }
        }

        private static bool IsLastBunker()
        {
            return Bunkers.Count == 1;
        }

        internal class Bunker
        {
            private readonly int initCapacity;
            private readonly string name;
            private readonly Queue<int> storage;
            private int capacity;

            public Bunker(string name, int capacity)
            {
                this.name = name;
                this.capacity = capacity;
                this.initCapacity = capacity;
                this.storage = new Queue<int>();
            }

            public bool CanWeaponFitIn(int weapon)
            {
                return this.capacity >= weapon;
            }

            public bool CanWeaponFitInWhenBunkerCleaned(int weapon)
            {
                return this.initCapacity >= weapon;
            }

            public void AddWeapon(int weapon)
            {
                this.storage.Enqueue(weapon);
                this.capacity -= weapon;
            }

            public void AddBigerWeapon(int weapon)
            {
                while (this.capacity < weapon)
                {
                    this.capacity += this.storage.Dequeue();
                }

                this.AddWeapon(weapon);
            }

            public override string ToString()
            {
                var result = $"{this.name} -> ";
                if (!this.storage.Any())
                {
                    result += "Empty";
                }
                else
                {
                    result += string.Join(", ", this.storage);
                }

                return result;
            }
        }
    }
}
