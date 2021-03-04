using System;

namespace Yon.Parsing
{
    /// <summary>
    /// This class encapsulates a Lexer rule that tries to gracefully
    /// end parsing at the end of a string by adding any remaining characters
    /// to the token queue as a single delimiter token.
    /// </summary>
    public class GracefulExit : ITemplateLexerRule
    {
        /// <summary>
        /// Attempts to execute the parsing rule and returns true if
        /// the current character matched the rule,
        /// and false if the current character did not.
        /// </summary>
        /// <param name="context">The Lexer context to evaluate.</param>
        /// <exception cref="FormatException">Throws if the string terminates during a property definition.</exception>
        public bool Evaluate(TemplateLexerContext context)
        {
            // We use -2 as the final character won't be appended until _after_ the final round of rule evaluations.
            if (context.Buffer.Index != context.Template.Length - 2)
            {
                return false;
            }
            // Remaining characters become a token as well
            if (context.State == TokenLexerState.ReadingDelimiter && !context.Buffer.IsEmpty
                && context.Buffer.Current != '{' && context.Buffer.Current != '}')
            {
                var token = context.Buffer.ToToken(TemplateTokenType.Delimiter);
                // This is the one and only situation in which we need to work around
                // being unable to modify the buffer.
                token = new TemplateToken(TemplateTokenType.Delimiter, token.Value + context.Buffer.Current);
                context.Tokens.Enqueue(context.Buffer.ToToken(TemplateTokenType.Delimiter));
                context.Buffer.Clear();
                return true;
            }
            // TODO: provide a decent error message
            else if (context.State == TokenLexerState.ReadingProperty)
            {
                throw new FormatException();
            }
            return true;
        }
    }
}