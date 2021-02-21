using System;
using System.Collections.Generic;

namespace Simplifing
{
    public class Node
    {
        public Token Token { get; set; }
        public List<Node> Children { get; set; }

        public Node(Token token, params Node[] nodesList)
        {
            Token = token;
            if (nodesList != null && nodesList.Length != 0)
            {
                Children = new List<Node>(nodesList);
            }
        }

        public bool Calculate(bool[] variableValues = null)
        {
            switch (Token.TokenType)
            {
                case TokenType.LiteralTrue:
                    {
                        return true;
                    }
                case TokenType.LiteralFalse:
                    {
                        return false;
                    }
                case TokenType.OpetatorNot:
                    {
                        var childValue = Children[0].Calculate(variableValues);
                        var value = !childValue;
                        return value;
                    }
                case TokenType.OperatorOr:
                    {
                        var childValue1 = Children[0].Calculate(variableValues);
                        var childValue2 = Children[1].Calculate(variableValues);
                        var value = childValue1 || childValue2;
                        return value;
                    }
                case TokenType.OperatorAnd:
                    {
                        var childValue1 = Children[0].Calculate(variableValues);
                        var childValue2 = Children[1].Calculate(variableValues);
                        var value = childValue1 && childValue2;
                        return value;
                    }
                case TokenType.OperatorImplies:
                    {
                        var childValue1 = Children[0].Calculate(variableValues);
                        var childValue2 = Children[1].Calculate(variableValues);
                        var value = !childValue1 || (childValue1 && childValue2);
                        return value;
                    }
                case TokenType.OperatorIIF:
                    {
                        var childValue1 = Children[0].Calculate(variableValues);
                        var childValue2 = Children[1].Calculate(variableValues);
                        var value = childValue1 == childValue2;
                        return value;
                    }
                case TokenType.Variable:
                    {
                        var value = variableValues[Token.VariableIndex];
                        return value;
                    }
                default:
                    {
                        throw new Exception($"Unrecognized token type {Token.TokenType}");
                    }
            }
        }
    }
}
