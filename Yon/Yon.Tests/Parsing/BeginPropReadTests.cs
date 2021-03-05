using System;
using NUnit.Framework;
using Yon.Parsing;

namespace Yon.Tests.Parsing
{
    public class BeginPropReadTests
    {
        private BeginPropRead TestRule { get; set; }
        private TemplateLexerContext Context { get; set; }
        
        [SetUp]
        public void SetUp()
        {
            TestRule = new BeginPropRead();
        }
        public class Evaluate : BeginPropReadTests
        {
            /// <summary>
            /// We throw when the buffer is empty because it means that
            /// there isn't any delimiter before this property definition.
            /// Allowing this to happen could result in unparsable release names.
            /// </summary>
            [Test]
            public void Throws_Format_Exception_When_Buffer_Empty()
            {
                var src = new CharBufferSource();
                Context = new TemplateLexerContext(src.Buffer, "{x}{");
                Context.CurrentCharacter = '{';
                Assert.Throws<FormatException>(() => TestRule.Evaluate(Context));
            }
            
            /// <summary>
            /// We throw when the context state is ReadingProperty because
            /// the result of this rule matching is to enter that state.
            /// An attempt to reenter the ReadingProperty state without
            /// exiting into the ReadingDelimiter state would be an
            /// invalid operation.
            /// </summary>
            [Test]
            public void Throws_Format_Exception_When_Context_State_Is_ReadingProperty()
            {
                var src = new CharBufferSource();
                Context = new TemplateLexerContext(src.Buffer, "abcdefg");
                Context.CurrentCharacter = '{';
                Assert.Throws<FormatException>(() => TestRule.Evaluate(Context));
            }
            
            /// <summary>
            /// We throw when the rule matches on the last character in
            /// the line because it'll be impossible to properly
            /// exit the property reading state with a non-null property value.
            /// </summary>
            [Test]
            public void Throws_Format_Exception_When_Line_End()
            {
                var src = new CharBufferSource();
                Context = new TemplateLexerContext(src.Buffer, "abcd{");
                src.Append('a');
                src.Append('b');
                src.Append('c');
                src.Append('d');
                Context.CurrentCharacter = '{';
                Assert.Throws<FormatException>(() => TestRule.Evaluate(Context));
            }
            
            [Test]
            public void Changes_Context_State_When_Input_Expected()
            {
                var src = new CharBufferSource();
                Context = new TemplateLexerContext(src.Buffer, "ab{xyz}");
                Context.State = TokenLexerState.ReadingDelimiter;
                src.Append('a');
                src.Append('b');
                Context.CurrentCharacter = '{';
                TestRule.Evaluate(Context);
                Assert.AreEqual(Context.State, TokenLexerState.ReadingProperty);
            }
        }
    }
}