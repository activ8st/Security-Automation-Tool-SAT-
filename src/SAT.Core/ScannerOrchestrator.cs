using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace SAT.Core
{
    public class ScannerOrchestrator
    {
        private readonly string _pythonScriptPath;

        public ScannerOrchestrator(string pythonScriptPath)
        {
            _pythonScriptPath = pythonScriptPath;
        }

        private string GetPythonPath()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string directPath = Path.Combine(localAppData, "Programs", "Python", "Python312", "python.exe");
            
            if (File.Exists(directPath))
            {
                return directPath;
            }
            return "python";
        }

        public JsonDocument RunScan(string target)
        {
            if (!File.Exists(_pythonScriptPath))
            {
                throw new FileNotFoundException($"Impossibile trovare lo script Python: {_pythonScriptPath}");
            }

            Console.WriteLine($"[*] Avvio processo Python per la scansione del target: {target}");

            var startInfo = new ProcessStartInfo
            {
                FileName = GetPythonPath(),
                Arguments = $"\"{_pythonScriptPath}\" --target \"{target}\" --format json",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using var process = Process.Start(startInfo);
                if (process == null)
                {
                    throw new Exception("Impossibile avviare il processo Python.");
                }

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"[!] Errore dallo scanner Python (Exit Code {process.ExitCode}): {error}");
                    return null;
                }

                try
                {
                    return JsonDocument.Parse(output);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"[!] Errore nel parsing del JSON di output: {ex.Message}");
                    Console.WriteLine($"Output grezzo: {output}");
                    return null;
                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine("[!] ERRORE FATALE: Il comando 'python' non è stato trovato nel PATH di sistema.");
                Console.WriteLine("Assicurati di aver installato Python e di averlo aggiunto alle variabili d'ambiente.");
                return null;
            }
        }
    }
}
