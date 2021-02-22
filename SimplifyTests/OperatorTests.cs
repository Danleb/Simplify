using NUnit.Framework;

namespace SimplifyTests
{
    public class OperatorTests
    {
        [Test]
        public void OperatorOr()
        {
            SyntacticParserTests.CheckIsValid("OR TRUE TRUE");
            SyntacticParserTests.CheckIsValid("OR TRUE FALSE");
            SyntacticParserTests.CheckIsValid("OR FALSE TRUE");
            SyntacticParserTests.CheckIsValid("OR FALSE FALSE", false);
        }

        [Test]
        public void OperatorAnd()
        {
            SyntacticParserTests.CheckIsValid("AND TRUE TRUE");
            SyntacticParserTests.CheckIsValid("AND TRUE FALSE", false);
            SyntacticParserTests.CheckIsValid("AND FALSE TRUE", false);
            SyntacticParserTests.CheckIsValid("AND FALSE FALSE", false);
        }

        [Test]
        public void OperatorNot()
        {
            SyntacticParserTests.CheckIsValid("NOT FALSE");
            SyntacticParserTests.CheckIsValid("NOT TRUE", false);
        }

        [Test]
        public void OperatorIFF()
        {
            SyntacticParserTests.CheckIsValid("IFF TRUE TRUE");
            SyntacticParserTests.CheckIsValid("IFF FALSE FALSE");
            SyntacticParserTests.CheckIsValid("IFF TRUE FALSE", false);
            SyntacticParserTests.CheckIsValid("IFF FALSE TRUE", false);
        }

        [Test]
        public void OperatorIMPLIES()
        {
            SyntacticParserTests.CheckIsValid("IMPLIES TRUE TRUE");
            SyntacticParserTests.CheckIsValid("IMPLIES TRUE FALSE", false);
            SyntacticParserTests.CheckIsValid("IMPLIES FALSE FALSE");
            SyntacticParserTests.CheckIsValid("IMPLIES FALSE TRUE");
        }
    }
}
