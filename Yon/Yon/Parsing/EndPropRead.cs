using System;

namespace Yon.Parsing
{
    /// <summary>
    /// This lexer rule attempts to match on '}' characters
    /// and then create a new token of type TemplateTokenType.Property.
    /// </summary>
    public class EndPropRead : ITemplateLexerRule
    {
        /// <summary>
        /// Attempts to execute the parsing rule and returns true if
        /// the current character matched the rule,
        /// and false if the current character did not.
        /// </summary>
        /// <param name="context">The Lexer context to evaluate.</param>
        /// <exception cref="FormatException">Throws if the buffer is empty
        /// or if the lexer is in the ReadingProperty state.</exception>
        public bool Evaluate(TemplateLexerContext context)
        {
            if (context.CurrentCharacter == '}')
            {
                if (context.Buffer.IsEmpty)
                {
                    throw new FormatException(); // "}..." or "abc{}xyz"
                }
                else if (context.State != TokenLexerState.ReadingProperty)
                {
                    throw new FormatException(); // "}}"
                }
                else
                {
                    context.Tokens.Enqueue(context.Buffer.ToToken(TemplateTokenType.Property));
                    context.Buffer.Clear();
                    context.State = TokenLexerState.ReadingDelimiter;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}