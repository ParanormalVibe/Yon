using System.Text;
using System;

namespace Yon.Parsing
{
    /// <summary>
    /// This class was created in order to allow the
    /// TemplateLexer algorithm to append to the buffer
    /// without also giving implementations of ITemplateLexerRule
    /// the ability to do so.
    /// </summary>
    public class CharBufferSource
    {
        /// <summary>
        /// The event that fires whenever Append(char c) is called.
        /// Created to notify the child CharBuffer of when a
        /// new character has been appended.
        /// </summary>
        public event EventHandler<EventArgs> Appended;
        
        /// <summary>
        /// The child buffer instance created by this CharBufferSource instance.
        /// </summary>
        public CharBuffer Buffer { get; private set; }

        /// <summary>
        /// The underlying data structure used to contain buffer characters.
        /// </summary>
        public StringBuilder Builder { get; private set; }

        /// <summary>
        /// Creates a new instance of the CharBufferSource class.
        /// </summary>
        public CharBufferSource()
        {
            Builder = new StringBuilder();
            Buffer = new CharBuffer(this);
        }
        
        /// <summary>
        /// Appends a new character to the end of the buffer.
        /// </summary>
        /// <param name="c">The character to append to the buffer.</param>
        public void Append(char c)
        {
            Builder.Append(c);
            Appended?.Invoke(this, new EventArgs());
        }
    }
}