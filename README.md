# Extract Annotations from PDF Documents

[![Product Page](https://img.shields.io/badge/Product%20Page-2865E0?style=for-the-badge&logo=appveyor&logoColor=white)](https://docs.groupdocs.com/parser/net/)
[![Docs](https://img.shields.io/badge/Docs-2865E0?style=for-the-badge&logo=Hugo&logoColor=white)](https://docs.groupdocs.com/parser/net/extract-annotations-from-pdf-documents/)
[![Blog](https://img.shields.io/badge/Blog-2865E0?style=for-the-badge&logo=WordPress&logoColor=white)](https://blog.groupdocs.com/categories/groupdocs.parser-product-family/)
[![Free Support](https://img.shields.io/badge/Free%20Support-2865E0?style=for-the-badge&logo=Discourse&logoColor=white)](https://forum.groupdocs.com/c/parser/)
[![Temporary License](https://img.shields.io/badge/Temporary%20License-2865E0?style=for-the-badge&logo=rocket&logoColor=white)](https://purchase.groupdocs.com/temporary-license/)

## Introduction

This repository demonstrates how to extract, organize, and export reviewer annotations from PDF documents using GroupDocs.Parser for .NET. Whether you're building a review tracker or archiving comments for compliance, these examples provide practical solutions for reading markup straight out of a PDF file.

## Use Case Scenarios

This repository addresses the following use cases:

- **Review workflows**: Collect every reviewer comment from a PDF without opening it in a viewer.
- **Ticketing integration**: Turn annotations into structured records for a bug or task tracker.
- **Document archiving**: Preserve reviewer feedback alongside the document for future reference.
- **Content pipelines**: Combine document text and annotation text into a single transcript.

## The Problem

Documents that go through review often accumulate sticky notes, highlights, and inline comments that are easy to miss once the file has passed through several reviewers. Manually scrolling through a PDF to find every comment is slow and error‑prone, and standard text‑extraction APIs don't surface markup at all. GroupDocs.Parser addresses this by exposing annotations as first‑class data through a dedicated API.

### Common Challenges

- ❌ Manually opening a PDF to find reviewer comments doesn't scale past a few pages.
- ❌ Standard text extraction ignores markup entirely, so comments go unnoticed.
- ❌ Knowing which page a comment belongs to usually requires a separate pass over the document.
- ❌ Exporting comments into analysis‑ready formats (CSV/JSON) demands extra serialization logic.

## The Solution

GroupDocs.Parser provides a straightforward API for reading PDF annotations alongside the rest of a document's content. Key capabilities include:

✅ **Whole‑document extraction** – Pulls every annotation out of a PDF in a single call.
✅ **Page‑level extraction** – Tags each annotation with the page it was found on.
✅ **Combined text + annotations** – Folds annotation text into a regular `GetText` read.
✅ **Feature detection** – Checks whether the loaded format supports annotations before you extract.
✅ **Export to CSV/JSON** – Produces review‑ready reports for spreadsheets or ticketing pipelines.

## Implementation Workflow

Here's a typical workflow for implementing Extract Annotations from PDF Documents:

1. **Check support**: Confirm the document format supports annotation extraction.
2. **Extract annotations**: Pull comments from the whole document or page by page.
3. **Combine with text** (optional): Read document text with annotation text included.
4. **Export report**: Serialize the results to CSV or JSON for review or archiving.

## Requirements

To use these examples, you'll need:

- **.NET 6.0**: The project targets .NET 6, and also runs on .NET 8 and .NET 10 thanks to the platform‑specific packages added in 26.7.
- **GroupDocs.Parser NuGet package**: Version 26.7 or later (annotation extraction was introduced in this release).
- **Temporary license key**: Obtain a 30‑day trial key from the Temporary License badge above.

## Project Structure

```
extract-annotations-from-pdf-documents-net/
│
├── Methods
│  ├── CheckAnnotationSupport.cs
│  ├── ExtractAllAnnotations.cs
│  ├── ExtractAnnotationsByPage.cs
│  ├── ExtractTextWithAnnotations.cs
│  ├── ExportAnnotationsToCsv.cs
│  └── ExportAnnotationsToJson.cs
├── resources
│  └── document-with-annotations.pdf
├── .gitignore
├── ExtractAnnotationsFromPdf.csproj
├── ExtractAnnotationsFromPdf.slnx
├── Program.cs
└── README.md
```

**File Organization:**
- **Methods** – Contains helper classes that perform extraction and export operations.
- **resources** – Holds the sample PDF used by the demo.
- **ExtractAnnotationsFromPdf.csproj** – Project definition with NuGet dependencies.
- **Program.cs** – Entry point that orchestrates the workflow.
- **CheckAnnotationSupport.cs** – Verifies the document format supports annotations before extracting.
- **ExtractAllAnnotations.cs** – Pulls every annotation from the whole document in one call.
- **ExtractAnnotationsByPage.cs** – Tags each annotation with the page it belongs to.
- **ExtractTextWithAnnotations.cs** – Reads document text with annotation text included.
- **ExportAnnotationsToCsv.cs** – Writes annotations to a CSV file for spreadsheet review.
- **ExportAnnotationsToJson.cs** – Writes annotations to a JSON file for API integration.

## Practical Examples

### Use Case: Check Annotation Support

**When to Use:** Before extracting, to confirm the loaded document format supports annotations.

```csharp
using (var parser = new Parser(path))
{
    return parser.Features.Annotations;
}
```

**What This Solves:** Avoids relying on an implicit `null` return from `GetAnnotations` and makes support checks explicit.

**Real-World Application:** Batch jobs can skip unsupported files early instead of failing deep inside a processing loop.

### Use Case: Extract All Annotations from a PDF

**When to Use:** When you need every comment in a document, regardless of page.

```csharp
var result = new List<string>();
using (var parser = new Parser(path))
{
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
```

**What This Solves:** Provides a fast way to check whether a document has any open comments at all.

**Real-World Application:** A review tracker can flag a document as "has feedback" the moment it's uploaded.

### Use Case: Extract Annotations Page‑by‑Page

**When to Use:** When you need to know which page each comment belongs to.

```csharp
var result = new List<AnnotationRecord>();
using (var parser = new Parser(path))
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
```

**What This Solves:** Attaches page context to every comment, which raw whole‑document extraction can't provide.

**Real-World Application:** Editors can jump straight to the page a comment refers to instead of searching for it.

### Use Case: Extract Text Together with Annotations

**When to Use:** When you want a single transcript that includes both document text and reviewer comments.

```csharp
using (var parser = new Parser(path))
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
```

**What This Solves:** Avoids a second pass over the document when a combined view is all you need.

**Real-World Application:** Generate a single reviewable transcript for teams that don't need a separate comment list.

### Use Case: Export Annotations to CSV

**When to Use:** When review results need to be opened in spreadsheet tools.

```csharp
var sb = new StringBuilder();
sb.AppendLine("page,value");

foreach (var record in records)
{
    sb.AppendLine($"{record.PageIndex},{CsvEscape(record.Value)}");
}

File.WriteAllText(outputPath, sb.ToString());
```

**What This Solves:** Generates a ready‑to‑import CSV report without custom parsing.

**Real-World Application:** Teams can feed the CSV into Excel checklists or lightweight review dashboards.

### Use Case: Export Annotations to JSON

**When to Use:** When downstream systems consume comments via APIs or a ticketing pipeline.

```csharp
var sb = new StringBuilder();
sb.AppendLine("[");

for (int i = 0; i < records.Count; i++)
{
    var comma = i < records.Count - 1 ? "," : string.Empty;
    sb.AppendLine($"  {{ \"page\": {records[i].PageIndex}, \"value\": \"{Escape(records[i].Value)}\" }}{comma}");
}

sb.AppendLine("]");
File.WriteAllText(outputPath, sb.ToString());
```

**What This Solves:** Provides a stable, structured payload that any service can deserialize directly.

**Real-World Application:** Automatically create a ticket per annotation in a project‑management tool.

## Benefits

By using GroupDocs.Parser for Extract Annotations from PDF Documents, you gain:

- **Time savings**: Automates comment discovery that would otherwise require manually opening every file.
- **Accuracy**: Distinguishes unsupported formats from documents that simply have no comments.
- **Context**: Page‑level extraction keeps every comment tied to where it was left.
- **Flexibility**: Choose between a flat dump, a page‑tagged report, or a combined text transcript.
- **Integration‑ready**: CSV and JSON exports plug straight into spreadsheets, dashboards, or ticketing systems.

## Keywords

`GroupDocs.Parser`, `.NET`, `PDF annotations`, `document review`, `reviewer comments`, `PDF parsing`, `CSV export`, `JSON export`, `document markup`, `C#`, `GroupDocs`, `parser API`, `PDF comments`, `annotation extraction`, `content pipeline`, `PDF automation`, `document tools`, `parser SDK`

**Ready to get started?** [View Documentation](https://docs.groupdocs.com/parser/net/extract-annotations-from-pdf-documents/) | [Get Support](https://forum.groupdocs.com/c/parser/) | [Request License](https://purchase.groupdocs.com/temporary-license/)
