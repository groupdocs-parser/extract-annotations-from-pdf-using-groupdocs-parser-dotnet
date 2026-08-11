using System.Collections.Generic;
using GroupDocs.Parser;
using GroupDocs.Parser.Data;

namespace GroupDocs.Samples.ExtractAnnotationsFromPdf.Methods
{
    public static class ExtractAllAnnotations
    {
        // Extracts every annotation value from the whole document in one pass
        public static List<string> Run(string path)
        {
            var result = new List<string>();
            using (var parser = new GroupDocs.Parser.Parser(path))
            {
                // GetAnnotations() returns null when the format doesn't support annotations
                IEnumerable<AnnotationItem> annotations = parser.GetAnnotations();
                if (annotations == null)
                {
                    return result;
                }

                foreach (var item in annotations)
                {
                    result.Add(item.Value);
                }
            }
            return result;
        }
    }
}
