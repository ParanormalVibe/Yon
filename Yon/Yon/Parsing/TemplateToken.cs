namespace Yon.Parsing
{
    public class TemplateToken
    {
        public TemplateTokenType Type { get; private set; }
        public  string Value { get; private set; }

        public TemplateToken(TemplateTokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}