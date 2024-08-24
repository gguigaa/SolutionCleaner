using SolutionCleaner.Models;

namespace SolutionCleaner.Services
{
    internal static class MainService
    {
        public static void Run()
        {
            Console.WriteLine("---------- Solution Cleaner ----------");

            bool verificationMode = GetVerificationMode();

            string workFolder = GetLocalExecution();
            if (string.IsNullOrEmpty(workFolder))
                return;

            string solutionFile = VerifySolutionExistence(workFolder);
            if (string.IsNullOrEmpty(solutionFile))
                return;

            CleanerConfigurationsModel cleanerConfigurations = getCleanerConfigurations();

            Console.WriteLine($"Solution found ({solutionFile}). Proceed with cleaning this work folder? (Y/N | Default: Y)");
            bool runCleaner = ReadBooleanUserAnswer(Console.ReadLine(), true);

            if (!runCleaner)
            {
                Console.WriteLine("Operation aborted.");
                return;
            }

            Console.WriteLine("Cleaning solution folders...");

            new CleanerService(verificationMode, cleanerConfigurations).Run(workFolder);

            Console.WriteLine("Operation completed. Press any button to exit...");
            Console.ReadKey();
        }

        private static bool GetVerificationMode()
        {
            Console.WriteLine("Run on verification mode? When on verification mode, will only show messages on console, without erasing folders. (Y/N | Default: N)");
            bool modoVerificacao = ReadBooleanUserAnswer(Console.ReadLine(), false);

            Console.WriteLine($"Verification mode: {(modoVerificacao ? "YES" : "NO")}");
            return modoVerificacao;
        }

        private static string GetLocalExecution()
        {
            Console.WriteLine("Run program on this directory? (Y/N | Default: Y))");
            bool execucaoLocal = ReadBooleanUserAnswer(Console.ReadLine(), true);

            string pastaDeTrabalho;

            if (execucaoLocal)
            {
                pastaDeTrabalho = Environment.CurrentDirectory;
                Console.WriteLine($"Running program on local directory: : {pastaDeTrabalho}.");
            }
            else
            {
                Console.WriteLine("Enter the directory where the program will be run:");
                string? caminhoDiretorio = Console.ReadLine();

                if (string.IsNullOrEmpty(caminhoDiretorio))
                {
                    Console.WriteLine("Invalid directory. Aborting operation.");
                    return string.Empty;
                }

                pastaDeTrabalho = caminhoDiretorio;
                Console.WriteLine($"Running program on directory {caminhoDiretorio}.");
            }

            return pastaDeTrabalho;
        }

        private static string VerifySolutionExistence(string pastaDeTrabalho)
        {
            IEnumerable<string> arquivos = Directory.GetFiles(pastaDeTrabalho);

            string? arquivoSolucao = arquivos.FirstOrDefault(x => x.Contains(".sln"));
            if (string.IsNullOrEmpty(arquivoSolucao))
            {
                Console.WriteLine("No solution found. Aborting operation.");
                return string.Empty;
            }

            return arquivoSolucao;
        }

        private static CleanerConfigurationsModel getCleanerConfigurations()
        {
            Console.WriteLine("Use default cleaning configurations? (Y/N | Default: Y))");
            Console.WriteLine($"Clean .vs folders: YES");
            Console.WriteLine($"Clean bin folders: YES");
            Console.WriteLine($"Clean obj folders: YES");
            Console.WriteLine($"Clean packages folders: NO");

            bool utilizarOpcoesPadrao = ReadBooleanUserAnswer(Console.ReadLine(), true);

            var cleanerConfigurations = new CleanerConfigurationsModel();

            if (utilizarOpcoesPadrao)
            {
                cleanerConfigurations.CleanVs = true;
                cleanerConfigurations.CleanBin = true;
                cleanerConfigurations.CleanObj = true;
                cleanerConfigurations.CleanPackages = false;
            }
            else
            {
                Console.WriteLine("Clean .vs folders? (Y/N | Default: Y))");
                cleanerConfigurations.CleanVs = ReadBooleanUserAnswer(Console.ReadLine(), true);

                Console.WriteLine("Clean bin folders? (Y/N | Default: Y))");
                cleanerConfigurations.CleanBin = ReadBooleanUserAnswer(Console.ReadLine(), true);

                Console.WriteLine("Clean obj folders? (Y/N | Default: Y))");
                cleanerConfigurations.CleanObj = ReadBooleanUserAnswer(Console.ReadLine(), true);

                Console.WriteLine("Clean packages folders? (Y/N | Default: Y))");
                cleanerConfigurations.CleanPackages = ReadBooleanUserAnswer(Console.ReadLine(), true);

                Console.WriteLine("Customized cleaning configurations:");
                Console.WriteLine($"Clean .vs folders: {(cleanerConfigurations.CleanVs ? "YES" : "NO")}");
                Console.WriteLine($"Clean bin folders: {(cleanerConfigurations.CleanBin ? "YES" : "NO")}");
                Console.WriteLine($"Clean obj folders: {(cleanerConfigurations.CleanObj ? "YES" : "NO")}");
                Console.WriteLine($"Clean packages folders: {(cleanerConfigurations.CleanPackages ? "YES" : "NO")}");
            }

            return cleanerConfigurations;
        }

        public static bool ReadBooleanUserAnswer(string? resposta, bool respostaPadraoVazia)
        {
            if (string.IsNullOrEmpty(resposta))
                return respostaPadraoVazia;

            return resposta.Equals("Y", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
