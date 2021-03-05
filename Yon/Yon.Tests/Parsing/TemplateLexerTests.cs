using System;
using NUnit.Framework;
using Yon.Parsing;

namespace Yon.Tests.Parsing
{
    public class TemplateLexerTests
    {

        public class GetTokens : TemplateLexerTests
        {
            
            /// <summary>
            /// Testing the happy path for correct token types.
            /// </summary>
            /// <param name="template"></param>
            /// <param name="values"></param>
            [TestCase("hello{xyz}", TemplateTokenType.Delimiter, TemplateTokenType.Property)]
            [TestCase("Game v{Major}.{Minor}", TemplateTokenType.Delimiter, TemplateTokenType.Property, 
                TemplateTokenType.Delimiter, TemplateTokenType.Property)]
            public void Returns_Token_Types_When_Input_Expected(string template, params TemplateTokenType[] types)
            {
                var tokens = new TemplateLexer().GetTokens(template);
                for (int i = 0; i < types.Length; i++)
                {
                    Assert.AreEqual(tokens.Dequeue().Type, types[i]);
                }
            }
            
            /// <summary>
            /// Testing the happy path for correct token values.
            /// </summary>
            /// <param name="template"></param>
            /// <param name="values"></param>
            [TestCase("hello{xyz}", "hello", "xyz")]
            [TestCase("Game v{Major}.{Minor}", "Game v", "Major", ".", "Minor")]
            public void Returns_Token_Values_When_Input_Expected(string template, params string[] values)
            {
                var tokens = new TemplateLexer().GetTokens(template);
                for (int i = 0; i < values.Length; i++)
                {
                    Assert.AreEqual(tokens.Dequeue().Value, values[i]);
                }
            }
            
            /// <summary>
            /// Testing all of the varieties of syntax errors that I could come up with
            /// to ensure that as many errors as possible are caught by the lexer.
            /// </summary>
            [TestCase("{xyz}{abcd}")]
            [TestCase("{{axb")]
            [TestCase("abcd{")]
            [TestCase("}abcd")]
            [TestCase("ab{}cd")]
            [TestCase("{x}}")]
            [TestCase("abcd}")]
            public void Throws_Format_Exception_When_Syntax_Error_Exists(string template)
            {
                Assert.Throws<FormatException>(() => new TemplateLexer().GetTokens(template));
            }
        }
    }
}