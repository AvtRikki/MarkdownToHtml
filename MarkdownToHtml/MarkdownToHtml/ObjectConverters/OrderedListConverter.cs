namespace MarkdownToHtml.ObjectConverters
{
    public class OrderedListConverter : BaseListConverter
    {
        public OrderedListConverter() : base(Consts.OrderedListPatter)
        {
        }

        public override string GetBlockHeader()
        {
            return "<ol>";
        }

        public override string GetBlockFooter()
        {
            return "</ol>";
        }
    }
}
