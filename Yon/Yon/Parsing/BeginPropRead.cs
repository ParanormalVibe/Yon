using System;

namespace Yon.Templates
{
    /// <summary>
    /// 
    /// </summary>
    public class BeginPropRead : ITemplateParserRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public bool Evaluate(TemplateParserContext context)
        {
            if (context.Buffer.Current == '{')
            {
                if (context.State == TokenParserState.ReadingProperty)
                {
                    throw new FormatException(); // "{{..."
                }
                else if (context.Buffer.IsEmpty)
                {
                    throw new FormatException(); // "{xyz}{..."
                }
                else if (context.Buffer.Index == context.Template.Length - 2)
                {
                    throw new FormatException(); // "abcd{..."
                }
                else
                {
                    context.Tokens.Enqueue(context.Buffer.ToToken(TemplateTokenType.Delimiter));
                    context.Buffer.Clear();
                    context.State = TokenParserState.ReadingProperty;
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