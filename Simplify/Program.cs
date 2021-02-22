using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simplifing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Empty formula input.");
                return;
            }

            var formulas = new List<string>();
            if (args[0] == "-f")
            {
                var path = args.Skip(1).Aggregate(string.Concat);
                if (!File.Exists(path))
                {
                    Console.WriteLine("Such file doesn't exists: " + path);
                    return;
                }

                var reader = new StreamReader(path);
                while (reader.Peek() > 0)
                {
                    formulas.Add(reader.ReadLine());
                }
                reader.Close();
            }
            else
            {
                var formula = args.Aggregate((v1, v2) => v1 + " " + v2);
                formulas.Add(formula);
            }

            foreach (var formula in formulas)
            {
                if (formulas.Count > 1)
                {
                    Console.WriteLine("Formula: " + formula);
                }

                var simplify = new Simplify(formula);
                simplify.Check();

                if (simplify.IsValid.Value)
                {
                    Console.WriteLine($"Valid");
                }
                else
                {
                    Console.WriteLine($"Invalid" + Environment.NewLine);
                    foreach (var contraargument in simplify.Contraarguments)
                    {
                        Console.WriteLine($"Contraargument:");
                        Console.WriteLine(contraargument);
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }
        }
    }
}
