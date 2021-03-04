using System.Collections.Generic;
using System;

namespace Yon.Templates
{
    /// <summary>
    /// This class is used to provide parser rules with
    /// the information they need to contribute to the
    /// parsing operation.
    /// </summary>
    public class TemplateParserContext
    {
        /// <summary>
        /// The character buffer being used in this parsing context.
        /// Used to track the current character(s) being evaluated.
        /// </summary>
        public CharBuffer Buffer { get; private set; }
        
        /// <summary>
        /// The tokens that the parsing operation has yielded thus far.
        /// </summary>
        public Queue<TemplateToken> Tokens { get; private set; }
        
        /// <summary>
        /// The template subject to parsing.
        /// </summary>
        public string Template { get; private set; }
        
        /// <summary>
        /// The current state/mode of the parser.
        /// </summary>
        public TokenParserState State { get; set; }

        /// <summary>
        /// Creates a new instance of the TemplateParserContext class.
        /// </summary>
        /// <param name="buffer">The character buffer to be used by the parser.
        /// Usually supplied by a CharBufferSource.</param>
        /// <param name="template">The template string to parse.</param>
        public TemplateParserContext(CharBuffer buffer, string template)
        {
            Buffer = buffer;
            Tokens = new Queue<TemplateToken>();
            Template = template;
            State = TokenParserState.ReadingDelimiter;
        }
    }
}