using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GroupDocs.Samples.ExtractAnnotationsFromPdf.Methods
{
    public static class ExportAnnotationsToCsv
    {
        // Writes page-tagged annotations to a two-column CSV file
        public static void Run(List<AnnotationRecord> records, string outputPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("page,value");

            foreach (var record in records)
            {
                sb.AppendLine($"{record.PageIndex},{CsvEscape(record.Value)}");
            }

            File.WriteAllText(outputPath, sb.ToString());
        }

        // Safely quotes fields containing commas, quotes, or line breaks
        private static string CsvEscape(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
            {
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }
    }
}
