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
            var formulas = new List<string>();
            if (args.Length == 0 || args[0] == "-w")
            {
                int formulaCounter = 1;
                while (true)
                {
                    Console.Write($"{formulaCounter++}. ");
                    var formula = Console.ReadLine();
                    ProcessFormula(formula);
                }
            }
            else if (args[0] == "-f")
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

            for (int formulaIndex = 0; formulaIndex < formulas.Count; formulaIndex++)
            {
                string formula = formulas[formulaIndex];
                if (formulas.Count > 1)
                {
                    Console.WriteLine($"{formulaIndex + 1}. Formula: {formula}");
                }

                ProcessFormula(formula);

                if (formulas.Count > 1)
                {
                    Console.WriteLine(Environment.NewLine);
                }
            }
        }

        public static void ProcessFormula(string formula)
        {
            var simplify = new Simplify(formula);

            try
            {
                simplify.Check();
            }
            catch (SyntaxException e)
            {
                Console.WriteLine($"Syntax error: {e.Message}");
                return;
            }
            catch (LexicalException e)
            {
                Console.WriteLine($"Lexical error: {e.Message}");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

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
                    Console.WriteLine(string.Empty);
                }
            }
        }
    }
}
