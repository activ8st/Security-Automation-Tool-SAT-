using System;
using System.IO;
using System.Text.Json;

namespace SAT.Core
{
    public class ReportGenerator
    {
        public static void GenerateHtmlReport(string target, JsonDocument results, string outputPath)
        {
            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>SAT Report - {target}</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 20px; background-color: #f4f4f9; }}
        h1 {{ color: #333; }}
        .card {{ background: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); margin-bottom: 20px; }}
        pre {{ background: #eee; padding: 10px; border-radius: 4px; overflow-x: auto; }}
        .missing {{ color: red; font-weight: bold; }}
        .ok {{ color: green; font-weight: bold; }}
    </style>
</head>
<body>
    <h1>Security Automation Tool (SAT) Report</h1>
    <p><strong>Target:</strong> {target}</p>
    <p><strong>Data:</strong> {DateTime.Now}</p>

    <div class='card'>
        <h2>Risultati Scansione (JSON Grezzo)</h2>
        <pre>{JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true })}</pre>
    </div>
</body>
</html>";

            File.WriteAllText(outputPath, html);
            Console.WriteLine($"[*] Report generato con successo in: {outputPath}");
        }
    }
}
