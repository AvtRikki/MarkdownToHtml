namespace MarkdownToHtml
{
    internal class Consts
    {
        public const string HeaderPatter = "^#{1,}\\s*(.*?)\\s*#{0,}$";
        public const string AmpersandPattern = "(&(?!#?\\w+;))";
        public const string LessThanPattern = "(<)";
        public const string BoldPattern = "(?:\\*{2}|__)(.+?)(?:\\*{2}|__)";
        public const string ItalicPattern = "(?:\\*{1}|_)(.+?)(?:\\*{1}|_)";
        public const string BoldItalicPattern = "(?:\\*{3}|___)(.+?)(?:\\*{3}|___)";
        public const string OrderedListPatter = "^[0-9]+\\.\\s+(.+)";
        public const string UnorderedListPattern = "^(?:\\*|\\+|\\-)\\s+(.+)";
        public const string ParagraphPattern = "^(?!<p>)";
    }
}
