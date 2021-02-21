using NUnit.Framework;
using Simplifing;

namespace SimplifyTests
{
    public class SyntacticParserTests
    {
        public void CheckIsValid(string formula, bool expectedIsValid = true)
        {
            var simplify = new Simplify(formula);
            var isValid = simplify.Check();
            Assert.AreEqual(expectedIsValid, isValid);
        }

        [Test]
        public void True()
        {
            CheckIsValid("TRUE");
        }

        [Test]
        public void False()
        {
            CheckIsValid("FALSE", false);
        }

        [Test]
        public void Brackets()
        {
            CheckIsValid("(TRUE)");
        }

        [Test]
        public void TrueOrTrue()
        {
            CheckIsValid("(OR TRUE TRUE)");
        }

        [Test]
        public void IdempotenticRule1()
        {
            CheckIsValid("(IFF p (AND p p))");
        }

        [Test]
        public void IdempotenticRule2()
        {
            CheckIsValid("(IFF p (OR p p))");
        }

        [Test]
        public void DoubleNegationRule()
        {
            CheckIsValid("(IFF (NOT(NOT p)) p)");
        }

        [Test]
        public void DeMorganRule1()
        {
            CheckIsValid("(IFF (NOT(OR p q)) (AND (NOT p) (NOT q)))");
        }

        [Test]
        public void DeMorganRule2()
        {
            CheckIsValid("(IFF (NOT (AND p q)) (OR (NOT p) (NOT q)))");
        }

        [Test]
        public void CommutabilityRule1()
        {
            CheckIsValid("(IFF (AND p q) (AND q p))");
        }

        [Test]
        public void CommutabilityRule2()
        {
            CheckIsValid("(IFF (OR p q) (OR q p))");
        }

        [Test]
        public void AssociativityRule1()
        {
            CheckIsValid("(IFF (AND p (AND q r)) (AND (AND p q) r))");
        }

        [Test]
        public void AssociativityRule2()
        {
            CheckIsValid("(IFF (OR p (OR q r)) (OR (OR p q) r))");
        }

        [Test]
        public void DistributivenessRule1()
        {
            CheckIsValid("(IFF (AND p (OR q r)) (OR (AND p q) (AND p r)))");
        }

        [Test]
        public void DistributivenessRule2()
        {
            CheckIsValid("(IFF (OR p (AND q r)) (AND (OR p q) (OR p r)))");
        }

        [Test]
        public void ContrapositionRule2()
        {
            CheckIsValid("(IFF (IMPLIES p q) (IMPLIES (NOT q) (NOT p)))");
        }
    }
}