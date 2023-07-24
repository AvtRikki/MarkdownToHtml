using MarkdownToHtml.ObjectConverters;
using MarkdownToHtml.SyntaxObjects;

namespace MarkdownToHtml
{
    internal class MarkdownToHtmlConverter
    {
        private readonly List<ObjectConverter> _converters = new List<ObjectConverter>();
        private BlockObjectConverter? _currentBlock;

        public MarkdownToHtmlConverter()
        {
            RegisterDefaultConverters();
        }

        private void RegisterDefaultConverters()
        {
            _converters.Add(new AmpersandConverter());
            _converters.Add(new LessThanConverter());
            _converters.Add(new HeaderConverter());
            _converters.Add(new BoldItalicConverter());
            _converters.Add(new BoldConverter());
            _converters.Add(new ItalicConverter());
            _converters.Add(new OrderedListConverter());
            _converters.Add(new UnorderedListConverter());
            _converters.Add(new ParagraphConverter());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMarkdownFilePath">The path to a Markdown file that contains input data or text content. The file should be a valid Markdown document.</param>
        /// <param name="outputMarkdownFilePath">The path to the output file after converting from Markdown to HTML</param>
        /// <exception cref="System.FileNotFoundException">Thrown when the file inputMarkdownFilePath with Markdown does not exist</exception>
        public void MarkdownToHtml(string inputMarkdownFilePath, string outputMarkdownFilePath)
        {
            if (!File.Exists(inputMarkdownFilePath))
            {
                throw new FileNotFoundException("Could not find file", inputMarkdownFilePath);
            }

            _currentBlock = null;
            using var htmlStreamWriter = new StreamWriter(outputMarkdownFilePath);
            IEnumerable<string> lines = File.ReadLines(inputMarkdownFilePath);
            foreach (string line in lines)
            {
                string currentLine = line;
                currentLine.Trim(' ');
                if (string.IsNullOrEmpty(currentLine))
                {
                    if (_currentBlock != null)
                    {
                        htmlStreamWriter.WriteLine(_currentBlock.GetBlockFooter());
                        _currentBlock = null;
                    }

                    htmlStreamWriter.WriteLine(Environment.NewLine);
                    continue;
                }

                currentLine = line.Replace("\t", "");
                foreach (ObjectConverter converter in _converters)
                {
                    SyntaxMatch syntaxMatch = converter.Match(currentLine);
                    while (syntaxMatch.Success)
                    {
                        if (converter is BlockObjectConverter blockConverter)
                        {
                            if (_currentBlock == null)
                            {
                                htmlStreamWriter.WriteLine(blockConverter.GetBlockHeader());
                                _currentBlock = blockConverter;
                            }

                            if (_currentBlock != null && _currentBlock.GetType() != blockConverter.GetType())
                                break;
                        }

                        currentLine =  converter.Convert(currentLine, syntaxMatch);
                        if (converter.IsSingleMatchConverter)
                        {
                            break;
                        }

                        syntaxMatch = converter.Match(currentLine);
                    }

                    if (syntaxMatch.Success && converter.IsSingleMatchConverter)
                        break;
                }

                htmlStreamWriter.WriteLine(currentLine);
            }

            if (_currentBlock != null)
            {
                htmlStreamWriter.WriteLine(_currentBlock.GetBlockFooter());
            }
        }

        /// <summary>
        /// Adds an object converter from Markdown to Html to the collection.
        /// </summary>
        /// <param name="converter">The converter from Markdown to Html to add.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the converter converter is null</exception>
        public void AddConverter(ObjectConverter converter)
        {
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            _converters.Add(converter);
        }

        /// <summary>
        /// Removes an object converter from Markdown to Html from the collection.
        /// </summary>
        /// <param name="converter">The converter from Markdown to Html to remove.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the converter converter is null</exception>
        public void RemoveConverter(ObjectConverter converter)
        {
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            _converters.Remove(converter);
        }

        /// <summary>
        /// Retrieves a read-only list of object converters currently registered in the Markdown to Html converter collection.
        /// </summary>
        /// <returns>A read-only list of object converters</returns>
        public IReadOnlyList<ObjectConverter> GetConverters()
        {
            return _converters;
        }

        /// <summary>
        /// Removes all object converters from Markdown to Html from the collection, clearing it.
        /// </summary>
        public void ClearConverters()
        {
            _converters.Clear();
        }
    }
}
