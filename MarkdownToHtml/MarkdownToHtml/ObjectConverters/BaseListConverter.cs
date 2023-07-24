namespace MarkdownToHtml.ObjectConverters
{
    public abstract class BaseListConverter : BlockObjectConverter
    {
        protected BaseListConverter(string pattern) : base(pattern)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            return Replace(input, GetHtmlListItem(match.Value), match.Index, match.Length);
        }

        private string GetHtmlListItem(string text)
        {
            return $"<li>{text}</li>";
        }
    }
}
