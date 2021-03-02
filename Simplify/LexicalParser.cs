using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplifing
{
    public class LexicalParser
    {
        private static readonly Dictionary<string, Token> StaticTokens = new Dictionary<string, Token>{
            {"AND", new Token(TokenType.OperatorAnd) },
            {"OR", new Token(TokenType.OperatorOr) },
            {"IMPLIES", new Token(TokenType.OperatorImplies) },
            {"NOT", new Token(TokenType.OpetatorNot) },
            {"IFF", new Token(TokenType.OperatorIIF) },
            {"(", new Token(TokenType.OpenBracket) },
            {")", new Token(TokenType.CloseBracket) },
            {"TRUE", new Token(TokenType.LiteralTrue) },
            {"FALSE", new Token(TokenType.LiteralFalse) },
        };
        private static readonly char Space = ' ';

        public string Input { get; }
        public bool IsParsed { get; private set; }
        public IReadOnlyList<string> VariableNames => _variableNames;
        public IReadOnlyList<Token> Tokens => _tokens;
        public int VariablesCount => VariableNames.Count;

        private List<string> _variableNames = new List<string>();
        private List<Token> _tokens = new List<Token>();

        public LexicalParser(string input)
        {
            Input = input;
        }

        public List<Token> Parse()
        {
            if (IsParsed)
            {
                return _tokens;
            }

            var currentIndex = 0;
            var nextVariableIndex = 0;

            while (currentIndex < Input.Length)
            {
                Token nextToken = null;

                if (Input[currentIndex] == Space)
                {
                    currentIndex++;
                    continue;
                }

                foreach (var (name, token) in StaticTokens)
                {
                    if (Input.IndexOf(name, currentIndex) == currentIndex)
                    {
                        nextToken = token;
                        currentIndex += name.Length;
                        break;
                    }
                }

                if (nextToken == null)
                {
                    var staticTokenIndexes = StaticTokens.Keys.Select(name => Input.IndexOf(name, currentIndex))
                            .OrderBy(v => v);

                    int staticTokenMinIndex = -1;
                    if (!staticTokenIndexes.All(v => v == -1))
                    {
                        staticTokenMinIndex = staticTokenIndexes.FirstOrDefault(v => v > -1);
                    }

                    var nextSpaceIndex = Input.IndexOf(Space, currentIndex);

                    int variableEndIndex;
                    if (staticTokenMinIndex * nextSpaceIndex > 0)
                    {
                        variableEndIndex = Math.Min(staticTokenMinIndex, nextSpaceIndex);
                    }
                    else
                    {
                        variableEndIndex = Math.Max(staticTokenMinIndex, nextSpaceIndex);
                    }

                    if (variableEndIndex == -1)
                    {
                        variableEndIndex = Input.Length;
                    }

                    var length = variableEndIndex - currentIndex;
                    var variableName = Input.Substring(currentIndex, length);
                    foreach (var character in variableName)
                    {
                        if (!char.IsLetterOrDigit(character))
                        {
                            throw new LexicalException($"Unacceptable character in variable name: {character}.");
                        }
                    }
                    if (char.IsDigit(variableName[0]))
                    {
                        throw new LexicalException($"Variable name cannot start with digit.");
                    }

                    var variableIndex = _variableNames.IndexOf(variableName);
                    if (variableIndex == -1)
                    {
                        _variableNames.Add(variableName);
                        variableIndex = nextVariableIndex;
                        nextVariableIndex++;
                    }

                    nextToken = new Token(TokenType.Variable, variableName, variableIndex);
                    currentIndex += variableName.Length;
                }

                if (nextToken == null)
                {
                    throw new LexicalException($"Failed to parse next token: {Input.Substring(currentIndex)}.");
                }
                else
                {
                    _tokens.Add(nextToken);
                }
            }

            IsParsed = true;
            return _tokens;
        }
    }
}
