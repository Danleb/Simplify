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

        private SimplifyLexicalParser _lexicalParser;

        public Simplify(string input)
        {
            Input = input;
            _lexicalParser = new SimplifyLexicalParser(input);
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
            return CreateTree(tokens, 0, out _);
        }

        private Node CreateTree(List<Token> tokens, int startIndex, out int endIndex, bool isSubFormula = true)
        {
            var token = tokens[startIndex];

            switch (token.TokenType)
            {
                case TokenType.OpenBracket:
                    {
                        startIndex++;
                        var node = CreateTree(tokens, startIndex, out endIndex);
                        if (tokens[endIndex].TokenType != TokenType.CloseBracket)
                        {
                            throw new Exception("Missing closing bracket after the expression.");
                        }
                        endIndex++;
                        return node;
                    }
                case TokenType.CloseBracket:
                    {
                        endIndex = startIndex + 1;
                        return null;
                    }
                case TokenType.LiteralTrue:
                case TokenType.LiteralFalse:
                    {
                        var node = new Node(token);
                        endIndex = startIndex + 1;
                        return node;
                    }
                case TokenType.OperatorAnd:
                case TokenType.OperatorOr:
                case TokenType.OperatorImplies:
                case TokenType.OperatorIIF:
                    {
                        startIndex++;
                        var child1 = CreateTree(tokens, startIndex, out var endIndex1);
                        var child2 = CreateTree(tokens, endIndex1, out endIndex);
                        var node = new Node(token, child1, child2);
                        return node;
                    }
                case TokenType.OpetatorNot:
                    {
                        startIndex++;
                        var child1 = CreateTree(tokens, startIndex, out endIndex);
                        var node = new Node(token, child1);
                        return node;
                    }
                case TokenType.Variable:
                    {
                        var node = new Node(token);
                        endIndex = startIndex + 1;
                        return node;
                    }
                default:
                    {
                        throw new Exception("Unexpected token type:" + token.TokenType);
                    }
            }
        }
    }
}
