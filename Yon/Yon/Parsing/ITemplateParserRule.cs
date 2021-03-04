using System.Collections.Generic;
using System.Text;

namespace Yon.Templates
{
    /// <summary>
    /// Template parser rules were created to improve the readability of
    /// the template parsing algorithm by compartmentalizing and
    /// streamlining the parsing logic.
    /// This way the parsing algorithm implementation better conveys
    /// the thought process behind it.
    /// </summary>
    public interface ITemplateParserRule
    {
        /// <summary>
        /// Attempts to execute the parsing rule and returns true if
        /// the current character should be appended to the buffer,
        /// and false if the current character should be discarded.
        /// </summary>
        /// <param name="context">The parser context to evaluate.</param>
        /// <returns></returns>
        bool Evaluate(TemplateParserContext context);
    }
}