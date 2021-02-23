using NUnit.Framework;

namespace SimplifyTests
{
    public class SyntacticParserTests
    {
        [Test]
        public void True()
        {
            AssertFormula.IsValid("TRUE");
        }

        [Test]
        public void False()
        {
            AssertFormula.IsInvalid("FALSE");
        }

        [Test]
        public void TrueOrTrue()
        {
            AssertFormula.IsValid("(OR TRUE TRUE)");
        }

        [Test]
        public void IdempotenticRule1()
        {
            AssertFormula.IsValid("(IFF p (AND p p))");
        }

        [Test]
        public void IdempotenticRule2()
        {
            AssertFormula.IsValid("(IFF p (OR p p))");
        }

        [Test]
        public void DoubleNegationRule()
        {
            AssertFormula.IsValid("(IFF (NOT(NOT p)) p)");
        }

        [Test]
        public void DeMorganRule1()
        {
            AssertFormula.IsValid("(IFF (NOT(OR p q)) (AND (NOT p) (NOT q)))");
        }

        [Test]
        public void DeMorganRule2()
        {
            AssertFormula.IsValid("(IFF (NOT (AND p q)) (OR (NOT p) (NOT q)))");
        }

        [Test]
        public void CommutabilityRule1()
        {
            AssertFormula.IsValid("(IFF (AND p q) (AND q p))");
        }

        [Test]
        public void CommutabilityRule2()
        {
            AssertFormula.IsValid("(IFF (OR p q) (OR q p))");
        }

        [Test]
        public void AssociativityRule1()
        {
            AssertFormula.IsValid("(IFF (AND p (AND q r)) (AND (AND p q) r))");
        }

        [Test]
        public void AssociativityRule2()
        {
            AssertFormula.IsValid("(IFF (OR p (OR q r)) (OR (OR p q) r))");
        }

        [Test]
        public void DistributivenessRule1()
        {
            AssertFormula.IsValid("(IFF (AND p (OR q r)) (OR (AND p q) (AND p r)))");
        }

        [Test]
        public void DistributivenessRule2()
        {
            AssertFormula.IsValid("(IFF (OR p (AND q r)) (AND (OR p q) (OR p r)))");
        }

        [Test]
        public void ContrapositionRule()
        {
            AssertFormula.IsValid("(IFF (IMPLIES p q) (IMPLIES (NOT q) (NOT p)))");
        }

        [Test]
        public void OrTrue_A()
        {
            AssertFormula.IsValid("(OR TRUE a)");
        }

        [Test]
        public void OpenOrTrueTrue()
        {
            AssertFormula.IsValid("(OR TRUE TRUE)");
        }
    }
}