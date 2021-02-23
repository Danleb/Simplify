using System;
using System.IO;
using System.Linq;

namespace Simplifing
{
    public class Program
    {
        private static readonly string[] Comments =
        {
            "#",
            "//",
            ";"
        };

        public static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "-w")
            {
                ExecuteEndlessInput();
                return;
            }
            else if (args[0] == "-f")
            {
                var path = args.Skip(1).Aggregate(string.Concat);
                ExecuteFromFile(path);
            }
            else
            {
                ExecuteSingleFormula(args);
            }
        }

        private static void ExecuteFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Such file doesn't exist: " + path);
                return;
            }

            var reader = new StreamReader(path);
            var formulaNumber = 1;
            while (reader.Peek() > 0)
            {
                var line = reader.ReadLine();
                if (Comments.Any(v => line.StartsWith(v)))
                {
                    Console.WriteLine(line);
                    continue;
                }

                Console.WriteLine($"Formula #{formulaNumber++}");
                ProcessFormula(line);
            }
            reader.Close();
        }

        private static void ExecuteSingleFormula(string[] args)
        {
            var formula = args.Aggregate((v1, v2) => v1 + " " + v2);
            ProcessFormula(formula);
        }

        private static void ExecuteEndlessInput()
        {
            int formulaCounter = 1;
            while (true)
            {
                Console.Write($"{formulaCounter++}. ");
                var line = Console.ReadLine();
                if (line == "exit")
                {
                    break;
                }
                ProcessFormula(line);
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
