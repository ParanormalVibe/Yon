using System;
using NUnit.Framework;
using Yon.Parsing;

namespace Yon.Tests.Parsing
{
    public class EndPropReadTests
    {

        public class Evaluate
        {
            /// <summary>
            /// We throw when the buffer is empty because that usually
            /// means that the user didn't provide a property name value
            /// (e.g "abc{}xyz"), which would be problematic to allow.
            /// </summary>
            [Test]
            public void Throws_FormatException_When_Buffer_Is_Empty()
            {
                var src = new CharBufferSource();
                var context = new TemplateLexerContext(src.Buffer, "}abcd");
                context.State = TokenLexerState.ReadingProperty;
                context.CurrentCharacter = '}';
                Assert.Throws<FormatException>(() => new EndPropRead().Evaluate(context));
            }
            
            /// <summary>
            /// We throw on the ReadingDelimiter context state because that means the
            /// closing property character '}' appeared before the opening property
            /// character '{'.
            /// </summary>
            [Test]
            public void Throws_FormatException_When_Context_State_Is_ReadingDelimiter()
            {
                var src = new CharBufferSource();
                var context = new TemplateLexerContext(src.Buffer, "abcd}");
                src.Append('a');
                src.Append('b');
                src.Append('c');
                src.Append('d');
                context.CurrentCharacter = '}';
                context.State = TokenLexerState.ReadingDelimiter;
                Assert.Throws<FormatException>(() => new EndPropRead().Evaluate(context));
            }
            
            /// <summary>
            /// Testing the happy path.
            /// </summary>
            [Test]
            public void Creates_Property_Token_When_Input_Expected_And_Buffer_Not_Empty()
            {
                var src = new CharBufferSource();
                var context = new TemplateLexerContext(src.Buffer, "{abcd}");
                src.Append('{');
                src.Append('a');
                src.Append('b');
                src.Append('c');
                src.Append('d');
                context.CurrentCharacter = '}';
                context.State = TokenLexerState.ReadingProperty;
                new EndPropRead().Evaluate(context);
                Assert.AreEqual("{abcd", context.Tokens.Dequeue()?.Value);
            }
        }
    }
}