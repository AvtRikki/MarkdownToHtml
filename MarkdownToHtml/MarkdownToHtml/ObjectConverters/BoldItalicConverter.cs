using MarkdownToHtml.SyntaxObjects;

namespace MarkdownToHtml.ObjectConverters
{
    public class BoldItalicConverter : ObjectConverter
    {
        public BoldItalicConverter() : base(Consts.BoldItalicPattern)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            return Replace(input, GetHtmlBoldItalicText(match.Value), match.Index, match.Length);
        }

        private string GetHtmlBoldItalicText(string text)
        {
            return $"<strong><em>{text}</em></strong>";
        }
    }
}
