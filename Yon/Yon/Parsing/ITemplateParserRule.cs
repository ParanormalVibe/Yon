using System.Collections.Generic;
using System.Text;

namespace Yon.Templates
{
    /// <summary>
    /// Template Lexer rules were created to improve the readability of
    /// the template parsing algorithm by compartmentalizing and
    /// streamlining the parsing logic.
    /// This way the parsing algorithm implementation better conveys
    /// the thought process behind it.
    /// </summary>
    public interface ITemplateLexerRule
    {
        /// <summary>
        /// Attempts to execute the parsing rule and returns true if
        /// the current character matched the rule,
        /// and false if the current character did not.
        /// </summary>
        /// <param name="context">The Lexer context to evaluate.</param>
        /// <returns></returns>
        bool Evaluate(TemplateLexerContext context);
    }
}