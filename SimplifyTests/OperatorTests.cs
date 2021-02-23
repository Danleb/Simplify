using NUnit.Framework;

namespace SimplifyTests
{
    public class OperatorTests
    {
        [Test]
        public void OperatorOr()
        {
            AssertFormula.IsValid("(OR TRUE TRUE)");
            AssertFormula.IsValid("(OR TRUE FALSE)");
            AssertFormula.IsValid("(OR FALSE TRUE)");
            AssertFormula.IsInvalid("(OR FALSE FALSE)");
        }

        [Test]
        public void OperatorAnd()
        {
            AssertFormula.IsValid("(AND TRUE TRUE)");
            AssertFormula.IsInvalid("(AND TRUE FALSE)");
            AssertFormula.IsInvalid("(AND FALSE TRUE)");
            AssertFormula.IsInvalid("(AND FALSE FALSE)");
        }

        [Test]
        public void OperatorNot()
        {
            AssertFormula.IsValid("(NOT FALSE)");
            AssertFormula.IsInvalid("(NOT TRUE)");
        }

        [Test]
        public void OperatorIFF()
        {
            AssertFormula.IsValid("(IFF TRUE TRUE)");
            AssertFormula.IsValid("(IFF FALSE FALSE)");
            AssertFormula.IsInvalid("(IFF TRUE FALSE)");
            AssertFormula.IsInvalid("(IFF FALSE TRUE)");
        }

        [Test]
        public void OperatorIMPLIES()
        {
            AssertFormula.IsValid("(IMPLIES TRUE TRUE)");
            AssertFormula.IsInvalid("(IMPLIES TRUE FALSE)");
            AssertFormula.IsValid("(IMPLIES FALSE FALSE)");
            AssertFormula.IsValid("(IMPLIES FALSE TRUE)");
        }
    }
}
