using MarkdownToHtml.SyntaxObjects;
using System.Text.RegularExpressions;

namespace MarkdownToHtml.ObjectConverters
{
    public class AmpersandConverter : ObjectConverter
    {
        public AmpersandConverter() : base(Consts.AmpersandPattern)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            return Regex.Replace(input, match.Value, "&amp;");
        }
    }
}
