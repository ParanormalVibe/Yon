using System;

namespace Yon.Templates
{
    /// <summary>
    /// 
    /// </summary>
    public class EndPropRead : ITemplateParserRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public bool Evaluate(TemplateParserContext context)
        {
            if (context.Buffer.Current == '}')
            {
                if (context.Buffer.IsEmpty)
                {
                    throw new FormatException(); // "}..." or "abc{}xyz"
                }
                else if (context.State != TokenParserState.ReadingProperty)
                {
                    throw new FormatException(); // "}}"
                }
                else
                {
                    context.Tokens.Enqueue(context.Buffer.ToToken(TemplateTokenType.Property));
                    context.Buffer.Clear();
                    context.State = TokenParserState.ReadingDelimiter;
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