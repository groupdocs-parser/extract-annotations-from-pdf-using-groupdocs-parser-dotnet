using System.Collections.Generic;
using GroupDocs.Parser;
using GroupDocs.Parser.Data;

namespace GroupDocs.Samples.ExtractAnnotationsFromPdf.Methods
{
    // A single annotation tagged with the zero-based page it was found on
    public class AnnotationRecord
    {
        public int PageIndex { get; set; }
        public string Value { get; set; }
    }

    public static class ExtractAnnotationsByPage
    {
        // Walks every page and tags each annotation with its zero-based page index
        public static List<AnnotationRecord> Run(string path)
        {
            var result = new List<AnnotationRecord>();
            using (var parser = new GroupDocs.Parser.Parser(path))
            {
                if (!parser.Features.Annotations)
                {
                    return result;
                }

                var info = parser.GetDocumentInfo();
                if (info == null || info.PageCount == 0)
                {
                    return result;
                }

                for (int pageIndex = 0; pageIndex < info.PageCount; pageIndex++)
                {
                    IEnumerable<AnnotationItem> pageAnnotations = parser.GetAnnotations(pageIndex);
                    if (pageAnnotations == null)
                    {
                        continue;
                    }

                    foreach (var item in pageAnnotations)
                    {
                        result.Add(new AnnotationRecord { PageIndex = pageIndex, Value = item.Value });
                    }
                }
            }
            return result;
        }
    }
}
