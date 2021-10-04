using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var noOfDuplicates = numDuplicates((new string[] { "glove", "glove", "glove" }).ToList(),
                (new int[] { 1, 2, 1 }).ToList(), (new int[] { 1, 1, 1 }).ToList());
            var result = betterCompression("a30c9b2c1");
            var result1 = betterCompression("abc");
        }

        public static int numDuplicates(List<string> name, List<int> price, List<int> weight)
        {
            var results = name.Zip(price, (x, y) => new Item { Name = x, Price = y })
                .
                Zip(weight, (x, y) => new Item { Name = x.Name, Price = x.Price, Weight = y });
            var test = results.ToList();
            //var distict = results.Distinct().Distinct().Count();
            var dis = results.GroupBy(x => x.ToString()).Count();
            var dist = results.Distinct().ToList();
            var hash = results.ToHashSet();
            return results.Count() - hash.Count;
        }

        //a30c9b2c1
        public static string betterCompression(string s)
        {
            var lettersWithIndex = s.Select((c, i) => new { Letter = c, Index = i }).
                Where(item => Char.IsLetter(item.Letter))
                .ToList();
            var data = new Dictionary<char, int>();
            for (int i = 0; i < lettersWithIndex.Count; i++)
            {
                var firstChar = lettersWithIndex[i].Letter;
                var frequencyToRead = i < lettersWithIndex.Count - 1 ?
                    (lettersWithIndex[i + 1].Index) - (lettersWithIndex[i].Index)
                    : (s.Length) - (lettersWithIndex[i].Index);
                var firstNumber = frequencyToRead > 1 ?
                    s.Substring((lettersWithIndex[i].Index) + 1, frequencyToRead - 1)
                    : string.Empty;
                int.TryParse(firstNumber, out int digit);
                if (data.ContainsKey(firstChar))
                {
                    data[firstChar] = data[firstChar] + digit;
                }
                else
                {
                    data.Add(firstChar, digit);
                }
            }

            var result = new StringBuilder();
            foreach (var entry in data)
            {
                result.Append(entry.Key).Append(entry.Value==0?string.Empty:entry.Value.ToString());
            }
            return result.ToString();
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }


        public override bool Equals(object obj)
        {
            var item = (Item)obj;
            return this.Name == item.Name && this.Price == item.Price && this.Weight == item.Weight;
        }

        public override string ToString()
        {
            return this.Name + this.Price + this.Weight;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
