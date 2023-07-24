namespace MarkdownToHtml.SyntaxObjects
{
    public class HeaderConverter : ObjectConverter
    {
        public HeaderConverter() : base(Consts.HeaderPatter)
        {
        }

        public override string Convert(string input, SyntaxMatch match)
        {
            int count = input.TakeWhile(symbol => symbol == '#').Count();
            var header = GetHtmlHeaderByWeight(count, match.Value);
            return header.Replace("#", string.Empty);
        }

        public override bool IsSingleMatchConverter => true;

        private string GetHtmlHeaderByWeight(int weight, string? input)
        {
            switch (weight)
            {
                case 1: 
                    return $"<h1>{input}</h1>";
                case 2: 
                    return $"<h2>{input}</h2>";
                case 3: 
                    return $"<h3>{input}</h3>";
                case 4: 
                    return $"<h4>{input}</h4>";
                case 5: 
                    return $"<h5>{input}</h5>";
                default: 
                    return $"<h6>{input}</h6>";
            }
        }
    }
}
