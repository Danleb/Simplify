using NUnit.Framework;
using Simplifing;

namespace SimplifyTests
{
    public class LexicalParserTests
    {
        [Test]
        public void Empty()
        {
            var parser = new SimplifyLexicalParser("");
            var tokens = parser.Parse();
            Assert.AreEqual(0, tokens.Count);
        }

        [Test]
        public void True()
        {
            var parser = new SimplifyLexicalParser("TRUE");
            var tokens = parser.Parse();
            Assert.AreEqual(TokenType.LiteralTrue, tokens[0].TokenType);
        }

        [Test]
        public void False()
        {
            var parser = new SimplifyLexicalParser("FALSE");
            var tokens = parser.Parse();
            Assert.AreEqual(TokenType.LiteralFalse, tokens[0].TokenType);
        }

        [Test]
        public void TrueInBrackets()
        {
            var parser = new SimplifyLexicalParser("(TRUE)");
            var tokens = parser.Parse();
            Assert.AreEqual(TokenType.OpenBracket, tokens[0].TokenType);
            Assert.AreEqual(TokenType.LiteralTrue, tokens[1].TokenType);
            Assert.AreEqual(TokenType.CloseBracket, tokens[2].TokenType);
        }

        [Test]
        public void FalseInBrackets()
        {
            var parser = new SimplifyLexicalParser("(FALSE)");
            var tokens = parser.Parse();
            Assert.AreEqual(TokenType.OpenBracket, tokens[0].TokenType);
            Assert.AreEqual(TokenType.LiteralFalse, tokens[1].TokenType);
            Assert.AreEqual(TokenType.CloseBracket, tokens[2].TokenType);
        }

        [Test]
        public void OrTrueFalse()
        {
            var parser = new SimplifyLexicalParser("(OR TRUE FALSE)");
            var tokens = parser.Parse();
            Assert.AreEqual(TokenType.OpenBracket, tokens[0].TokenType);
            Assert.AreEqual(TokenType.OperatorOr, tokens[1].TokenType);
            Assert.AreEqual(TokenType.LiteralTrue, tokens[2].TokenType);
            Assert.AreEqual(TokenType.LiteralFalse, tokens[3].TokenType);
            Assert.AreEqual(TokenType.CloseBracket, tokens[4].TokenType);
        }

        [Test]
        public void AndTrueFalse()
        {
            var parser = new SimplifyLexicalParser("(AND TRUE FALSE)");
            var tokens = parser.Parse();
            Assert.AreEqual(TokenType.OpenBracket, tokens[0].TokenType);
            Assert.AreEqual(TokenType.OperatorAnd, tokens[1].TokenType);
            Assert.AreEqual(TokenType.LiteralTrue, tokens[2].TokenType);
            Assert.AreEqual(TokenType.LiteralFalse, tokens[3].TokenType);
            Assert.AreEqual(TokenType.CloseBracket, tokens[4].TokenType);
        }

        [Test]
        public void IdempotenticRule1()
        {
            var parser = new SimplifyLexicalParser("(IFF p (AND p p))");
            var tokens = parser.Parse();

            Assert.AreEqual(TokenType.OpenBracket, tokens[0].TokenType);
            Assert.AreEqual(TokenType.OperatorIIF, tokens[1].TokenType);

            Assert.AreEqual(TokenType.Variable, tokens[2].TokenType);
            Assert.AreEqual(0, tokens[2].VariableIndex);
            Assert.AreEqual("p", tokens[2].VariableName);

            Assert.AreEqual(TokenType.OpenBracket, tokens[3].TokenType);
            Assert.AreEqual(TokenType.OperatorAnd, tokens[4].TokenType);

            Assert.AreEqual(TokenType.Variable, tokens[5].TokenType);
            Assert.AreEqual(0, tokens[5].VariableIndex);
            Assert.AreEqual("p", tokens[5].VariableName);

            Assert.AreEqual(TokenType.Variable, tokens[6].TokenType);
            Assert.AreEqual(0, tokens[6].VariableIndex);
            Assert.AreEqual("p", tokens[6].VariableName);

            Assert.AreEqual(TokenType.CloseBracket, tokens[7].TokenType);
            Assert.AreEqual(TokenType.CloseBracket, tokens[8].TokenType);
        }
    }
}
