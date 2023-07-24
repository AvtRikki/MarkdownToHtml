using System.Text.RegularExpressions;

namespace MarkdownToHtml
{
    public class SyntaxMatch
    {
        public static SyntaxMatch NotMatch { get; } = new SyntaxMatch();

        private SyntaxMatch()
        {
            Success = false;
            Value = string.Empty;
        }

        public SyntaxMatch(bool success, string value, int index, int length)
        {
            Success = success;
            Value = value;
            Index = index;
            Length = length;
        }

        public SyntaxMatch(Match match) 
            : this(match.Success, match.Groups[1].Value, match.Index, match.Length)
        {
        }

        public bool Success { get; }

        public string Value { get;}

        public int Index { get; }

        public int Length { get; }
    }
}
