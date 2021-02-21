namespace Simplifing
{
    public class Token
    {
        public TokenType TokenType { get; }
        public string VariableName { get; }
        public int VariableIndex { get; }

        public Token(TokenType tokenType)
        {
            TokenType = tokenType;
        }

        public Token(TokenType tokenType, string variableName, int variableIndex)
        {
            TokenType = tokenType;
            VariableName = variableName;
            VariableIndex = variableIndex;
        }

        public override string ToString()
        {
            return $"{TokenType}";
        }
    }
}
