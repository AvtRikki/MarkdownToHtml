using MarkdownToHtml.SyntaxObjects;
using System.Text.RegularExpressions;

namespace MarkdownToHtml.ObjectConverters
{
    public class LessThanConverter : ObjectConverter
    {
        public LessThanConverter() : base(Consts.LessThanPattern)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            return Regex.Replace(input, match.Value, "&lt;");
        }
    }
}
