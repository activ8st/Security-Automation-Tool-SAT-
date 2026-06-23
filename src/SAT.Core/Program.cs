using System;
using System.IO;

namespace SAT.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- SECURITY AUTOMATION TOOL (SAT) ---");
            Console.WriteLine("[v1.0.0-MVP]");
            
            if (args.Length == 0)
            {
                Console.WriteLine("Utilizzo: SAT.Core <target>");
                Console.WriteLine("Esempio: SAT.Core https://example.com");
                return;
            }

            string target = args[0];
            string pythonScriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "SAT.Scanner", "engine.py");
            pythonScriptPath = Path.GetFullPath(pythonScriptPath);

            var orchestrator = new ScannerOrchestrator(pythonScriptPath);
            var results = orchestrator.RunScan(target);

            if (results != null)
            {
                Console.WriteLine("[*] Scansione completata con successo. Generazione report...");
                string reportPath = $"SAT_Report_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                ReportGenerator.GenerateHtmlReport(target, results, reportPath);
            }
            else
            {
                Console.WriteLine("[!] La scansione è fallita. Controlla gli errori precedenti.");
            }
        }
    }
}
