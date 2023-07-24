namespace MarkdownToHtml.ObjectConverters
{
    public class UnorderedListConverter : BaseListConverter
    {
        public UnorderedListConverter() : base(Consts.UnorderedListPattern)
        {
        }

        public override string GetBlockHeader()
        {
            return "<ul>";
        }

        public override string GetBlockFooter()
        {
            return "</ul>";
        }
    }
}
