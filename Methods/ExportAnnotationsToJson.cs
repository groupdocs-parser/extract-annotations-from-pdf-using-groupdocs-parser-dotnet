using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GroupDocs.Samples.ExtractAnnotationsFromPdf.Methods
{
    public static class ExportAnnotationsToJson
    {
        // Writes page-tagged annotations to a JSON array of { page, value } objects
        public static void Run(List<AnnotationRecord> records, string outputPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("[");

            for (int i = 0; i < records.Count; i++)
            {
                var comma = i < records.Count - 1 ? "," : string.Empty;
                sb.AppendLine($"  {{ \"page\": {records[i].PageIndex}, \"value\": \"{Escape(records[i].Value)}\" }}{comma}");
            }

            sb.AppendLine("]");
            File.WriteAllText(outputPath, sb.ToString());
        }

        // Escapes JSON special characters
        private static string Escape(string s)
        {
            return s?.Replace("\\", "\\\\").Replace("\"", "\\\"") ?? string.Empty;
        }
    }
}
