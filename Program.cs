using Spectre.Console;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SackboySaveFix
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var saveFilePath = GetSaveFilePath(args);

                var saveFile = await ReadSaveFile(saveFilePath);
                var selectedWorld = AskForSelectedWorld();
                var outputFile = ProduceOutputFile(saveFile, selectedWorld);

                SaveOriginalFileBackup(saveFilePath);

                await File.WriteAllBytesAsync(saveFilePath, outputFile);

                AnsiConsole.Markup("[green]Success![/] Press Enter to exit");
            }
            catch (Exception e) when (e is ProgramExecutionException)
            {
                AnsiConsole.Markup($"[red]ERROR! {e.Message}[/]");
            }

            Console.ReadLine();
        }

        private static void SaveOriginalFileBackup(string saveFilePath)
        {
            var backupFileName = Path.ChangeExtension(saveFilePath, $".bak{Path.GetExtension(saveFilePath)}");

            // Do not replace previous backups
            // Loop until valid name is found
            if (File.Exists(backupFileName))
            {
                int backupFileNameIdx = 0;
                string indexedBackupFileName;
                do
                {
                    backupFileNameIdx++;
                    indexedBackupFileName = backupFileName + "." + backupFileNameIdx;
                } while (File.Exists(indexedBackupFileName));

                backupFileName = indexedBackupFileName;
            }

            File.Copy(saveFilePath, backupFileName);
        }

        private static World AskForSelectedWorld() =>
            AnsiConsole.Prompt(
                new SelectionPrompt<World>()
                    .Title("Which [green]world[/] do you want to unlock?")
                    .AddChoices(Enum.GetValues<World>()));

        private static async Task<byte[]> ReadSaveFile(string saveFilePath)
        {
            if (!File.Exists(saveFilePath))
            {
                throw new ProgramExecutionException($"File \"{saveFilePath}\" doesn't exist!");
            }

            return await File.ReadAllBytesAsync(saveFilePath);
        }

        private static string GetSaveFilePath(string[] args)
        {
            if (args.Length > 0)
            {
                return args[0];
            }

            return AnsiConsole.Ask<string>("Please enter the [green]path to the save file (General)[/]:")?.Trim('"');
        }

        private static byte[] ProduceOutputFile(in byte[] saveFile, World selectedWorld)
        {
            var pipelineResults = new SectionsPipeline(saveFile, selectedWorld).Run();

            var sizeDifference = pipelineResults
                .Select(result => result.NewSection.Length - (result.OriginalSectionEnd - result.OriginalSectionStart))
                .Sum();

            var outputFile = new byte[saveFile.Length + sizeDifference];

            var fileIndex = 0;
            var outputIndex = 0;

            foreach (var result in pipelineResults)
            {
                if (fileIndex < result.OriginalSectionStart)
                {
                    var copyLength = result.OriginalSectionStart - fileIndex;
                    Array.Copy(saveFile, fileIndex, outputFile, outputIndex, copyLength);
                    outputIndex += copyLength;
                }

                Array.Copy(result.NewSection, 0, outputFile, outputIndex, result.NewSection.Length);

                fileIndex = result.OriginalSectionEnd;
                outputIndex += result.NewSection.Length;
            }

            if (fileIndex < saveFile.Length)
            {
                Array.Copy(saveFile, fileIndex, outputFile, outputIndex, saveFile.Length - fileIndex - 1);
            }

            return outputFile;
        }
    }
}
