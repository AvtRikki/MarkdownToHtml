using MarkdownToHtml.SyntaxObjects;

namespace MarkdownToHtml.ObjectConverters
{
    public class BoldConverter : ObjectConverter
    {
        public BoldConverter() : base(Consts.BoldPattern)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            return Replace(input, GetHtmlBoldText(match.Value), match.Index, match.Length);
        }

        private string GetHtmlBoldText(string text)
        {
            return $"<strong>{text}</strong>";
        }
    }
}
