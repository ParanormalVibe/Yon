using System;
using System.Collections.Generic;
using System.Text;

namespace Yon.Parsing
{
    /// <summary>
    /// I hand rolled a Lexer instead of using Regex because I would
    /// like to have detailed syntax error reporting for end users.
    /// I chose this over ANTLR as I didn't feel it necessary to introduce
    /// a new dependency for such a simple grammar.
    /// </summary>
    public class TemplateLexer
    {
        private readonly IList<ITemplateLexerRule> _rules;

        public TemplateLexer()
        {
            _rules = new List<ITemplateLexerRule>();
            _rules.Add(new GracefulExit());
        }
        
        public Queue<TemplateToken> Parse(string template)
        {
            var bufferSource = new CharBufferSource();
            var context = new TemplateLexerContext(bufferSource.Buffer, template);
            foreach (var c in template)
            {
                context.CurrentCharacter = c;
                var matchedRule = false;
                foreach (var rule in _rules)
                {
                    // if even just one rule says to not append the character,
                    // we'll listen to it and ignore all of the rules that say to append
                    matchedRule = rule.Evaluate(context);
                    if (matchedRule)
                        break;
                }
                if (!matchedRule)
                {
                    bufferSource.Append(c);
                }
            }
            return context.Tokens;
        }
    }
}