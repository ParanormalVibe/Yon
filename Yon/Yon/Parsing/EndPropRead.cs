using System;

namespace Yon.Parsing
{
    /// <summary>
    /// 
    /// </summary>
    public class EndPropRead : ITemplateLexerRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public bool Evaluate(TemplateLexerContext context)
        {
            if (context.Buffer.Current == '}')
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