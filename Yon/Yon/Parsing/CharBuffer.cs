using System;
using System.Text;

namespace Yon.Templates
{
    /// <summary>
    /// Used by implementations of ITemplateParserRule.
    /// The collection of previous characters
    /// that will eventually be combined to form a token string.
    /// This class was created in order to allow parser rules to
    /// still access the character buffer while removing their ability
    /// to append to the buffer.
    /// This restriction makes it easier to reason about the effects that
    /// different rules have on parsing.
    /// </summary>
    public class CharBuffer
    {
        /// <summary>
        /// The final character at the end of the buffer.
        /// When used in parsing templates, this character
        /// is the current character being evaluated by parser rules.
        /// </summary>
        public char Current => _builder[^1];

        /// <summary>
        /// The number of characters currently in the buffer.
        /// </summary>
        public int Length => _builder.Length;

        /// <summary>
        /// Returns true if the buffer is empty.
        /// </summary>
        public bool IsEmpty => _builder.Length == 0;
        
        /// <summary>
        /// The index within the source string that the
        /// buffer is currently reading from.
        /// </summary>
        public  int Index { get; private set; }

        private readonly StringBuilder _builder;
        

        /// <summary>
        /// Creates a new instance of the CharBuffer class.
        /// </summary>
        /// <param name="source">The CharBufferSource used
        /// to create the CharBuffer.</param>
        public CharBuffer(CharBufferSource source)
        {
            Index = -1;
            _builder = source.Builder;
        }
        
        public void Clear()
        {
            _builder.Clear();
        }

        /// <summary>
        /// Creates a new token with of the type specified by tokenType,
        /// and with a value equal to the concatenation result of all of
        /// the characters in the buffer.
        /// Throws InvalidOperationException when the buffer is empty
        /// </summary>
        /// <param name="tokenType">The type of the new token</param>
        /// <returns></returns>
        public TemplateToken ToToken(TemplateTokenType tokenType)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Unable to create a token from an empty buffer");
            }
            return new TemplateToken(tokenType, _builder.ToString());
        }

        private void IncrementIndex(object sender, EventArgs e)
        {
            Index++;
        }
    }
}