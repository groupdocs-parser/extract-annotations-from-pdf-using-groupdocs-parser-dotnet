using System.IO;
using GroupDocs.Parser;
using GroupDocs.Parser.Options;

namespace GroupDocs.Samples.ExtractAnnotationsFromPdf.Methods
{
    public static class ExtractTextWithAnnotations
    {
        // Extracts document text with annotation text interleaved in a single pass
        public static string Run(string path)
        {
            using (var parser = new GroupDocs.Parser.Parser(path))
            {
                var options = new TextOptions
                {
                    IncludeAnnotations = true
                };

                using (TextReader reader = parser.GetText(options))
                {
                    return reader?.ReadToEnd() ?? string.Empty;
                }
            }
        }
    }
}
