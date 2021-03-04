using System;
using NUnit.Framework;
using Yon.Parsing;

namespace Yon.Tests.Parsing
{
    public class CharBufferTests
    {
        private CharBufferSource BufferSource { get; set; }

        [SetUp]
        public void SetUp()
        {
            BufferSource = new CharBufferSource();
        }
        
        public class ToToken : CharBufferTests
        {
            /// <summary>
            /// This is expected to throw because allowing tokens with empty values
            /// could be a problem down the road, so it's better to just avoid it.
            /// </summary>
            [Test]
            public void Throws_InvalidOperationException_When_Buffer_Empty()
            {
                Assert.Throws<InvalidOperationException>(() =>
                    BufferSource.Buffer.ToToken(default));
            }
        }
    }
}
