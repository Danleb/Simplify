using NUnit.Framework;
using Simplifing;
using System;
using System.Threading;

namespace SimplifyTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void EmptyArguments()
        {
            Thread t = new Thread(() =>
            {
                using ConsoleOutputReader output = new ConsoleOutputReader();
                Program.Main(Array.Empty<string>());
                Assert.AreEqual("Empty formula input.", output.ReadLine());
            });

        }

        [Test]
        public void True()
        {
            using ConsoleOutputReader output = new ConsoleOutputReader();
            Program.Main(new[] { "TRUE" });
            Assert.AreEqual("Valid", output.ReadLine());
        }

        [Test]
        public void False()
        {
            using ConsoleOutputReader output = new ConsoleOutputReader();
            Program.Main(new[] { "FALSE" });
            Assert.AreEqual("Invalid", output.ReadLine());
        }

        [Test]
        public void OrTrueFalse()
        {
            using ConsoleOutputReader output = new ConsoleOutputReader();
            Program.Main(new[] { "OR", "TRUE", "FALSE" });
        }

        [Test]
        public void OrA_B()
        {
            using ConsoleOutputReader output = new ConsoleOutputReader();
            Program.Main(new[] { "OR", "a", "b" });
        }
    }
}
