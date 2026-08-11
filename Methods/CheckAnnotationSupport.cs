using GroupDocs.Parser;

namespace GroupDocs.Samples.ExtractAnnotationsFromPdf.Methods
{
    public static class CheckAnnotationSupport
    {
        // Returns true if the loaded document format supports annotation extraction
        public static bool Run(string path)
        {
            using (var parser = new GroupDocs.Parser.Parser(path))
            {
                return parser.Features.Annotations;
            }
        }
    }
}
