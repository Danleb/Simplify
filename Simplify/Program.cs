using System;

namespace Simplifing
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = args[0];
            var simplify = new Simplify(input);
            simplify.Check();
            Console.WriteLine($"Is valid = {simplify.IsValid}");
            foreach (var contrarqument in simplify.Contrarquments)
            {
                Console.WriteLine($"Contrarqument: {contrarqument}");
                Console.WriteLine(Environment.NewLine);
            }

            Console.ReadKey();
        }
    }
}
