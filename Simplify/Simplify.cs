using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simplifing
{
    public class Simplify
    {
        public string Input { get; }
        public bool? IsValid { get; private set; }

        public List<string> Contraarguments { get; private set; } = new List<string>();

        private LexicalParser _lexicalParser;

        public Simplify(string input)
        {
            Input = input;
            _lexicalParser = new LexicalParser(input);
        }

        public bool Check()
        {
            var tokens = _lexicalParser.Parse();
            var node = CreateTree(tokens);

            if (_lexicalParser.VariablesCount == 0)
            {
                IsValid = node.Calculate();
                return IsValid.Value;
            }

            var variableValues = Enumerable.Range(0, _lexicalParser.VariablesCount).Select(v => false).ToArray();
            var alwaysTrue = true;

            while (true)
            {
                var value = node.Calculate(variableValues);
                if (!value)
                {
                    alwaysTrue = false;
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < _lexicalParser.VariablesCount; i++)
                    {
                        stringBuilder.Append(_lexicalParser.VariableNames[i] + " = " + variableValues[i]);
                        if (i < _lexicalParser.VariablesCount - 1)
                        {
                            stringBuilder.Append(Environment.NewLine);
                        }
                    }
                    Contraarguments.Add(stringBuilder.ToString());
                }

                var wasChanged = false;
                for (int i = 0; i < variableValues.Length; i++)
                {
                    if (!variableValues[i])
                    {
                        wasChanged = true;
                        variableValues[i] = true;
                        for (int u = 0; u < i; u++)
                        {
                            variableValues[u] = false;
                        }
                        break;
                    }
                }
                if (!wasChanged)
                {
                    break;
                }
            }

            IsValid = alwaysTrue;
            return alwaysTrue;
        }

        private Node CreateTree(List<Token> tokens)
        {
            var node = CreateTree(tokens, 0, out var parsingEndIndex);
            if (parsingEndIndex != tokens.Count)
            {
                throw new SyntaxException("End of formula was expected.");
            }
            return node;
        }

        private Node CreateTree(List<Token> tokens, int startIndex, out int parsingEndIndex, bool isSubFormula = true)
        {
            if (startIndex >= tokens.Count)
            {
                if (isSubFormula)
                {
                    throw new SyntaxException("Unexpected end of formula. Opening bracket or identifier was expected.");
                }
                else
                {
                    throw new SyntaxException("Unexpected end of formula. Operator was expected.");
                }
            }

            var token = tokens[startIndex];

            if (isSubFormula)
            {
                switch (token.TokenType)
                {
                    case TokenType.LiteralTrue:
                    case TokenType.LiteralFalse:
                    case TokenType.Variable:
                        {
                            var node = new Node(token);
                            parsingEndIndex = startIndex + 1;
                            return node;
                        }
                    case TokenType.OpenBracket:
                        {
                            var nextStartIndex = startIndex + 1;
                            var node = CreateTree(tokens, nextStartIndex, out parsingEndIndex, false);
                            if (parsingEndIndex >= tokens.Count || tokens[parsingEndIndex].TokenType != TokenType.CloseBracket)
                            {
                                throw new SyntaxException("Closing bracket is missing after the expression.");
                            }
                            if (startIndex == 0 && parsingEndIndex != tokens.Count - 1)
                            {
                                throw new SyntaxException("End of formula was expected.");
                            }

                            parsingEndIndex++;
                            return node;
                        }
                    default:
                        {
                            throw new SyntaxException($"Unexpected token type: {token.TokenType}. Opening bracket was expected.");
                        }
                }
            }
            else
            {
                switch (token.TokenType)
                {
                    case TokenType.OperatorAnd:
                    case TokenType.OperatorOr:
                        {
                            startIndex++;
                            //todo add parsing list of arguments
                            var child1 = CreateTree(tokens, startIndex, out var endIndex1);
                            var child2 = CreateTree(tokens, endIndex1, out parsingEndIndex);
                            var node = new Node(token, child1, child2);
                            return node;
                        }
                    case TokenType.OperatorImplies:
                    case TokenType.OperatorIIF:
                        {
                            startIndex++;
                            var child1 = CreateTree(tokens, startIndex, out var endIndex1);
                            var child2 = CreateTree(tokens, endIndex1, out parsingEndIndex);
                            var node = new Node(token, child1, child2);
                            return node;
                        }
                    case TokenType.OpetatorNot:
                        {
                            startIndex++;
                            var child1 = CreateTree(tokens, startIndex, out parsingEndIndex);
                            var node = new Node(token, child1);
                            return node;
                        }
                    default:
                        {
                            throw new SyntaxException($"Unexpected token type: {token.TokenType}. Operator was expected.");
                        }
                }
            }
        }
    }
}
