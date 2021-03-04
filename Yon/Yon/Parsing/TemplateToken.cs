namespace Yon.Parsing
{
    /// <summary>
    /// A token produced as output by the TemplateLexer and its rules.
    /// TemplateTokens are then used to aid in parsing resource names.
    /// </summary>
    public class TemplateToken
    {
        /// <summary>
        /// The type of this template token.
        /// </summary>
        public TemplateTokenType Type { get; private set; }
        
        /// <summary>
        /// The value of this template token.
        /// </summary>
        public  string Value { get; private set; }

        /// <summary>
        /// Creates a new instance of the class TemplateToken.
        /// </summary>
        /// <param name="type">The type that this token belongs to.</param>
        /// <param name="value">The value that this token holds.</param>
        public TemplateToken(TemplateTokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}