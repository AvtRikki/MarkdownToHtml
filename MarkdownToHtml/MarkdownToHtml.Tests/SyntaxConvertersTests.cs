using MarkdownToHtml.ObjectConverters;
using MarkdownToHtml.SyntaxObjects;
using System.Text;
using Xunit;

namespace MarkdownToHtml.Tests
{
    public class SyntaxConvertersTests
    {
        [Fact]
        public void HeaderConverter_ConvertToH1()
        {
            const string expected = "<h1>Header 1</h1>";
            const string input = "# Header 1";
            var headerConverter = new HeaderConverter();
            var match = headerConverter.Match(input);
            Assert.True(match.Success);
            var result = headerConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void HeaderConverter_ConvertToH1HeaderWithClosedHeaderTag()
        {
            const string expected = "<h1>Header 1</h1>";
            const string input = "# Header 1 #";
            var headerConverter = new HeaderConverter();
            var match = headerConverter.Match(input);
            Assert.True(match.Success);
            var result = headerConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void HeaderConverter_ConvertToH1HeaderWithAdditionalText()
        {
            const string expected = "<h1>Header 1 Additional Text</h1>";
            const string input = "# Header 1 #Additional Text";
            var headerConverter = new HeaderConverter();
            var match = headerConverter.Match(input);
            Assert.True(match.Success);
            var result = headerConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AmpersandSyntaxConverter_WordWithAmpersand()
        {
            const string expected = "'AT&amp;T'";
            const string input = "'AT&T'";
            var ampersandConverter = new AmpersandConverter();
            var match = ampersandConverter.Match(input);
            Assert.True(match.Success);
            var result = ampersandConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AmpersandSyntaxConverter_WordWithEncodedAmpersand()
        {
            const string input = "'AT&amp;T'";
            var ampersandConverter = new AmpersandConverter();
            var match = ampersandConverter.Match(input);
            Assert.False(match.Success);
        }

        [Fact]
        public void LessThanConverter_LessThanInText()
        {
            const string expected = "and then a less-than 'x &lt; y'";
            const string input = "and then a less-than 'x < y'";
            var lessThanConverter = new LessThanConverter();
            var match = lessThanConverter.Match(input);
            Assert.True(match.Success);
            var result = lessThanConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BoldConverter_BoldWithDoubleUnderscore()
        {
            const string expected = "this is <strong>bold</strong>";
            const string input = "this is __bold__";
            var boldConverter = new BoldConverter();
            var match = boldConverter.Match(input);
            Assert.True(match.Success);
            var result = boldConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BoldConverter_BoldWithDoubleStars()
        {
            const string expected = "just like <strong>this is</strong>";
            const string input = "just like **this is**";
            var boldConverter = new BoldConverter();
            var match = boldConverter.Match(input);
            Assert.True(match.Success);
            var result = boldConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BoldConverter_BoldWithDoubleStarsAndDoubleUnderscore()
        {
            const string expected = "this is <strong>bold</strong> (just like <strong>this is</strong>)";
            const string input = "this is __bold__ (just like **this is**)";
            var boldConverter = new BoldConverter();
            var match = boldConverter.Match(input);
            var result = boldConverter.Convert(input, match);
            match = boldConverter.Match(result);
            result = boldConverter.Convert(result, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ItalicConverter_ItelicWithSingleUnderscore()
        {
            const string expected = "so is <em>this</em>,";
            const string input = "so is _this_,";
            var italicConverter = new ItalicConverter();
            var match = italicConverter.Match(input);
            Assert.True(match.Success);
            var result = italicConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ItalicConverter_BoldWithSingleStars()
        {
            const string expected = "<em>This is italicized</em>,";
            const string input = "*This is italicized*,";
            var italicConverter = new ItalicConverter();
            var match = italicConverter.Match(input);
            Assert.True(match.Success);
            var result = italicConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ItalicConverter_BoldWithSingleStasAndSingleUnderscore()
        {
            const string expected = "<em>This is italicized</em>, and so is <em>this</em>,";
            const string input = "*This is italicized*, and so is _this_,";
            var italicConverter = new ItalicConverter();
            var match = italicConverter.Match(input);
            var result = italicConverter.Convert(input, match);
            match = italicConverter.Match(result);
            result = italicConverter.Convert(result, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BoldItalicConverter_BoldItalicWithTripleUnderscore()
        {
            const string expected = "this is <strong><em>bold and italic</em></strong>";
            const string input = "this is ___bold and italic___";
            var boldItalicConverter = new BoldItalicConverter();
            var match = boldItalicConverter.Match(input);
            Assert.True(match.Success);
            var result = boldItalicConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BoldItalicConverter_BoldWithTripleStars()
        {
            const string expected = "just like <strong><em>this is</em></strong>";
            const string input = "just like ***this is***";
            var boldItalicConverter = new BoldItalicConverter();
            var match = boldItalicConverter.Match(input);
            Assert.True(match.Success);
            var result = boldItalicConverter.Convert(input, match);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BoldItalicConverter_BoldWithTripleStarsAndTripleUnderscore()
        {
            const string expected = "this is <strong><em>bold and italic</em></strong> (just like <strong><em>this is</em></strong>)";
            const string input = "this is ___bold and italic___ (just like ***this is***)";
            var boldItalicConverter = new BoldItalicConverter();
            var match = boldItalicConverter.Match(input);
            var result = boldItalicConverter.Convert(input, match);
            match = boldItalicConverter.Match(result);
            result = boldItalicConverter.Convert(result, match);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void OrderedListConverter_ExportBlock()
        {
            const string expected = "<ol><li>Numbered lists are easy</li></ol>";
            const string input = "1. Numbered lists are easy";
            StringBuilder result = new StringBuilder();
            var orderedListConverter = new OrderedListConverter();
            var match = orderedListConverter.Match(input);
            Assert.True(match.Success);
            result.Append(orderedListConverter.GetBlockHeader());
            result.Append(orderedListConverter.Convert(input, match));
            result.Append(orderedListConverter.GetBlockFooter());
            Assert.Equal(expected, result.ToString());
                    }

        [Fact]
        public void UnorderedListConverter_ExportBlockWithDash()
        {
            const string expected = "<ul><li>Use a minus sign for a bullet</li></ul>";
            const string input = "- Use a minus sign for a bullet";
            StringBuilder result = new StringBuilder();
            var unorderedListConverter = new UnorderedListConverter();
            var match = unorderedListConverter.Match(input);
            Assert.True(match.Success);
            result.Append(unorderedListConverter.GetBlockHeader());
            result.Append(unorderedListConverter.Convert(input, match));
            result.Append(unorderedListConverter.GetBlockFooter());
            Assert.Equal(expected, result.ToString());
        }

        [Fact]
        public void UnorderedListConverter_ExportBlockWithStars()
        {
            const string expected = "<ul><li>Use a minus sign for a bullet</li></ul>";
            const string input = "* Use a minus sign for a bullet";
            StringBuilder result = new StringBuilder();
            var unorderedListConverter = new UnorderedListConverter();
            var match = unorderedListConverter.Match(input);
            Assert.True(match.Success);
            result.Append(unorderedListConverter.GetBlockHeader());
            result.Append(unorderedListConverter.Convert(input, match));
            result.Append(unorderedListConverter.GetBlockFooter());
            Assert.Equal(expected, result.ToString());
        }

        [Fact]
        public void UnorderedListConverter_ExportBlockWithPlus()
        {
            const string expected = "<ul><li>Use a minus sign for a bullet</li></ul>";
            const string input = "+ Use a minus sign for a bullet";
            StringBuilder result = new StringBuilder();
            var unorderedListConverter = new UnorderedListConverter();
            var match = unorderedListConverter.Match(input);
            Assert.True(match.Success);
            result.Append(unorderedListConverter.GetBlockHeader());
            result.Append(unorderedListConverter.Convert(input, match));
            result.Append(unorderedListConverter.GetBlockFooter());
            Assert.Equal(expected, result.ToString());
        }

        [Fact]
        public void ParagraphConverter_ExportBlock()
        {
            const string expected = "<p>Btw this should be on a single line</p>";
            const string input = "Btw this should be on a single line";
            StringBuilder result = new StringBuilder();
            var paragraphConverter = new ParagraphConverter();
            result.Append(paragraphConverter.GetBlockHeader());
            result.Append(paragraphConverter.Convert(input, SyntaxMatch.NotMatch));
            result.Append(paragraphConverter.GetBlockFooter());
            Assert.Equal(expected, result.ToString());
        }
    }
}