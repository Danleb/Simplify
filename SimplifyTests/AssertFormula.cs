using NUnit.Framework;
using Simplifing;

namespace SimplifyTests
{
    public static class AssertFormula
    {
        public static void IsValid(string formula)
        {
            var simplify = new Simplify(formula);
            var isValid = simplify.Check();
            Assert.AreEqual(true, isValid);
        }

        public static void IsInvalid(string formula)
        {
            var simplify = new Simplify(formula);
            var isValid = simplify.Check();
            Assert.AreEqual(false, isValid);
        }

        public static void ThrowsSyntaxException(string formula)
        {
            var simplify = new Simplify(formula);
            Assert.Throws<SyntaxException>(() => simplify.Check());
        }

        public static void ThrowsLexicalException(string formula)
        {
            var parser = new LexicalParser(formula);
            Assert.Throws<LexicalException>(() => parser.Parse());
        }
    }
}
