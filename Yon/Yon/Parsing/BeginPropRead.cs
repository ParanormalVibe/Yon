using System;

namespace Yon.Parsing
{
    /// <summary>
    /// 
    /// </summary>
    public class BeginPropRead : ITemplateLexerRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public bool Evaluate(TemplateLexerContext context)
        {
            if (context.CurrentCharacter == '{')
            {
                if (context.State == TokenLexerState.ReadingProperty)
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
                    context.State = TokenLexerState.ReadingProperty;
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