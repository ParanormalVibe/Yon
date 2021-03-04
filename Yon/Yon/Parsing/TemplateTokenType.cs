namespace Yon.Parsing
{
    /// <summary>
    /// Used to indicate the type that a lexer token belongs to.
    /// This is then used during parsing in order to read release names
    /// </summary>
    public enum TemplateTokenType
    {
        Delimiter,
        Property
    }
}