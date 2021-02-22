using NUnit.Framework;
using Simplifing;
using System;

namespace SimplifyTests
{
    public class ProgramTests
    {
        [Test]
        public void EmptyArguments()
        {
            Program.Main(Array.Empty<string>());
        }

        [Test]
        public void True()
        {
            Program.Main(new[] { "TRUE" });
        }

        [Test]
        public void False()
        {
            Program.Main(new[] { "FALSE" });
        }

        [Test]
        public void OrTrueFalse()
        {
            Program.Main(new[] { "OR", "TRUE", "FALSE" });
        }

        [Test]
        public void OrA_B()
        {
            Program.Main(new[] { "OR", "a", "b" });
        }
    }
}
