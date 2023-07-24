namespace MarkdownToHtml.ObjectConverters
{
    public class ParagraphConverter : BlockObjectConverter
    {
        public ParagraphConverter() : base(Consts.ParagraphPattern)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            return input;
        }

        public override bool IsSingleMatchConverter => true;

        public override string GetBlockHeader()
        {
            return "<p>";
        }

        public override string GetBlockFooter()
        {
            return "</p>";
        }
    }
}
