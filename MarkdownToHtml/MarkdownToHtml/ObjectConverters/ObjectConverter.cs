using System.Text;
using System.Text.RegularExpressions;

namespace MarkdownToHtml.SyntaxObjects
{
    public abstract class ObjectConverter
    {
        private Regex? _regex;

        protected ObjectConverter() 
        { 
        }

        protected ObjectConverter(string pattern)
        {
            _regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        public virtual bool IsSingleMatchConverter { get;} = false;

        public virtual SyntaxMatch Match(string input)
        {
            var match = _regex.Match(input);
            if (match.Success)
            {
                return new SyntaxMatch(match);
            }
            else
            {
                return SyntaxMatch.NotMatch;
            }
        }

        public abstract string Convert(string input, SyntaxMatch match);

        protected string Replace(string input, string replacement, int index, int lenght)
        {
            var result = new StringBuilder(input.Substring(0, index));
            result.Append(replacement);
            int totalLenght = index + lenght;
            if (totalLenght != input.Length)
            {
                result.Append(input.Substring(totalLenght));
            }

            return result.ToString();
        }
    }
}
