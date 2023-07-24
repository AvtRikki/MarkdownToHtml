using MarkdownToHtml.SyntaxObjects;

namespace MarkdownToHtml.ObjectConverters
{
    public class ItalicConverter : ObjectConverter
    {
        public ItalicConverter() : base(Consts.ItalicPattern)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            return Replace(input, GetHtmlItalicText(match.Value), match.Index, match.Length);
        }

        private string GetHtmlItalicText(string text)
        {
            return $"<em>{text}</em>";
        }
    }
}
