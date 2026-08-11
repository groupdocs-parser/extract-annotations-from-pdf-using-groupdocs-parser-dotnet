using System;
using System.IO;
using GroupDocs.Parser;
using GroupDocs.Samples.ExtractAnnotationsFromPdf.Methods;

namespace GroupDocs.Samples.ExtractAnnotationsFromPdf
{
    public static class Program
    {
        private const string LicensePath = @"YOUR-LICENSE-PATH-HERE";
        private static readonly string ProjectRoot = Directory.GetCurrentDirectory();
        private static readonly string InputDir = Path.Combine(ProjectRoot, "resources");
        private static readonly string OutputDir = Path.Combine(ProjectRoot, "output");

        public static int Main()
        {
            try
            {
                SetLicense();
                Directory.CreateDirectory(OutputDir);

                var pdfPath = Path.Combine(InputDir, "document-with-annotations.pdf");

                if (!File.Exists(pdfPath))
                {
                    Console.WriteLine($"FAIL missing input file at {InputDir}");
                    return 2;
                }

                var supported = CheckAnnotationSupport.Run(pdfPath);
                Assert(supported, $"CheckAnnotationSupport reported annotations supported = {supported}");

                var allAnnotations = ExtractAllAnnotations.Run(pdfPath);
                Assert(allAnnotations.Count > 0, $"ExtractAllAnnotations returned {allAnnotations.Count} annotations");

                var byPage = ExtractAnnotationsByPage.Run(pdfPath);
                Assert(byPage.Count > 0, $"ExtractAnnotationsByPage reported {byPage.Count} page-tagged annotations");

                var textWithAnnotations = ExtractTextWithAnnotations.Run(pdfPath);
                Assert(!string.IsNullOrEmpty(textWithAnnotations), $"ExtractTextWithAnnotations returned {textWithAnnotations.Length} characters");

                var csvPath = Path.Combine(OutputDir, "annotations.csv");
                ExportAnnotationsToCsv.Run(byPage, csvPath);
                Assert(File.Exists(csvPath) && new FileInfo(csvPath).Length > 0, $"ExportAnnotationsToCsv wrote {new FileInfo(csvPath).Length} bytes to annotations.csv");

                var jsonPath = Path.Combine(OutputDir, "annotations.json");
                ExportAnnotationsToJson.Run(byPage, jsonPath);
                Assert(File.Exists(jsonPath) && new FileInfo(jsonPath).Length > 0, $"ExportAnnotationsToJson wrote {new FileInfo(jsonPath).Length} bytes to annotations.json");

                Console.WriteLine();
                Console.WriteLine("ALL PASS");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return 1;
            }
        }

        private static void SetLicense()
        {
            if (!File.Exists(LicensePath))
            {
                Console.WriteLine($"WARN license file not found at {LicensePath}; running in evaluation mode");
                return;
            }
            var license = new License();
            license.SetLicense(LicensePath);
            Console.WriteLine("License applied");
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception($"assert failed: {message}");
            }
            Console.WriteLine($"PASS {message}");
        }
    }
}
