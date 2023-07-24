using MarkdownToHtml.SyntaxObjects;

namespace MarkdownToHtml.ObjectConverters
{
    public abstract class BlockObjectConverter : ObjectConverter
    {
        protected BlockObjectConverter(string pattern) : base(pattern)
        {
        }

        public abstract string GetBlockHeader();

        public abstract string GetBlockFooter();
    }
}
