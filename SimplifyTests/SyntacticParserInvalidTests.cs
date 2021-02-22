using NUnit.Framework;
using Simplifing;

namespace SimplifyTests
{
    public class SyntacticParserInvalidTests
    {
        public static void CheckIsInvalid(string formula, bool expectedIsValid = false)
        {
            var simplify = new Simplify(formula);
            var isValid = simplify.Check();
            Assert.AreEqual(expectedIsValid, isValid);
        }

        [Test]
        public void Test()
        {
            CheckIsInvalid("IFF OR a b AND a b");
        }

        [Test]
        public void OrA_B()
        {
            CheckIsInvalid("OR a b", false);
        }

        [Test]
        public void TrueAndFalse()
        {
            CheckIsInvalid("(AND TRUE FALSE)");
        }
    }
}
