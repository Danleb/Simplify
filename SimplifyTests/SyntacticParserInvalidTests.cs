using NUnit.Framework;

namespace SimplifyTests
{
    public class SyntacticParserInvalidTests
    {
        [Test]
        public void TrueTrue()
        {
            AssertFormula.ThrowsSyntaxException("TRUE TRUE");
        }

        [Test]
        public void IFF_OR_A_B_AND_A_B()
        {
            AssertFormula.ThrowsSyntaxException("IFF OR a b AND a b");
        }

        [Test]
        public void OrA_B()
        {
            AssertFormula.ThrowsSyntaxException("OR a b");
        }

        [Test]
        public void OpenBracket()
        {
            AssertFormula.ThrowsSyntaxException("(");
        }

        [Test]
        public void TrueAndFalse()
        {
            AssertFormula.IsInvalid("(AND TRUE FALSE)");
        }
    }
}
