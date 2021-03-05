using NUnit.Framework;
using Yon.Parsing;

namespace Yon.Tests.Parsing
{
    public class GracefulExitTests
    {

        public class Evaluate
        {
            [Test]
            public void Creates_Delimiter_Token_When_Input_Expected_And_Buffer_Not_Empty()
            {
                var src = new CharBufferSource();
                var context = new TemplateLexerContext(src.Buffer, "abcd");
                src.Append('a');
                src.Append('b');
                src.Append('c');
                context.CurrentCharacter = 'd';
                new GracefulExit().Evaluate(context);
                Assert.AreEqual("abcd", context.Tokens.Dequeue()?.Value);
            }
        }
    }
}