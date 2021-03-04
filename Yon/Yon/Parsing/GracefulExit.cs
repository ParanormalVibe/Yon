using System;

namespace Yon.Templates
{
    /// <summary>
    /// This class encapsulates a parser rule that tries to gracefully
    /// end parsing at the end of a string by adding any remaining characters
    /// to the token queue as a single delimiter token.
    /// </summary>
    public class GracefulExit : ITemplateParserRule
    {
        /// <summary>
        /// Attempts to execute the parsing rule and returns true if
        /// the current character should be appended to the buffer,
        /// and false if the current character should be discarded.
        /// </summary>
        /// <param name="context">The parser context to evaluate.</param>
        /// <exception cref="FormatException">Throws if the string terminates during a property definition.</exception>
        public bool Evaluate(TemplateParserContext context)
        {
            // We use -2 as the final character won't be appended until _after_ the final round of rule evaluations.
            if (context.Buffer.Index != context.Template.Length - 2)
            {
                return true;
            }
            // Remaining characters become a token as well
            if (context.State == TokenParserState.ReadingDelimiter && !context.Buffer.IsEmpty)
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
            else if (context.State == TokenParserState.ReadingProperty)
            {
                throw new FormatException();
            }
            return true;
        }
    }
}