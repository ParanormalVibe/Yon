using System.Text;
using System;

namespace Yon.Templates
{
    public class CharBufferSource
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Appended;
        
        /// <summary>
        /// 
        /// </summary>
        public CharBuffer Buffer { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public StringBuilder Builder;
        
        public CharBufferSource()
        {
            Builder = new StringBuilder();
            Buffer = new CharBuffer(this);
        }
        
        public void Append(char c)
        {
            Builder.Append(c);
            Appended?.Invoke(this, new EventArgs());
        }
    }
}