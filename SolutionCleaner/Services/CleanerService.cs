using SolutionCleaner.Models;

namespace SolutionCleaner.Services
{
    internal class CleanerService(bool verificationMode, CleanerConfigurationsModel cleanerConfigurations)
    {
        private readonly bool _verificationMode = verificationMode;
        private readonly CleanerConfigurationsModel _cleanerConfigurations = cleanerConfigurations;

        public void Run(string workFolder)
        {
            CleanWorkFolder(workFolder);

            IEnumerable<string> directoriesWithProjects = GetDirectoriesWithProjects(workFolder);

            foreach (var directoryWithProject in directoriesWithProjects)
            {
                CleanDirectoryWithProject(directoryWithProject);
            }
        }

        private void CleanWorkFolder(string workFolder)
        {
            IEnumerable<string> directories = Directory.GetDirectories(workFolder);

            CleanVsFolder(directories);
            CleanPackagesFolder(directories);
        }

        private IEnumerable<string> GetDirectoriesWithProjects(string workFolder)
        {
            IEnumerable<string> directories = Directory.GetDirectories(workFolder);
            var directoriesWithProjects = new List<string>();

            foreach (var directory in directories)
            {
                IEnumerable<string> filesCandidateFolder = Directory.GetFiles(directory);
                if (filesCandidateFolder.Any(x => x.Contains(".csproj")))
                {
                    directoriesWithProjects.Add(directory);
                }
            }

            return directoriesWithProjects;
        }

        private void CleanDirectoryWithProject(string directoryWithProject)
        {
            IEnumerable<string> directories = Directory.GetDirectories(directoryWithProject);

            CleanVsFolder(directories);
            CleanBinFolder(directories);
            CleanObjFolder(directories);
            CleanPackagesFolder(directories);
        }

        private void CleanVsFolder(IEnumerable<string> directoryContent)
        {
            if (!_cleanerConfigurations.CleanVs)
                return;

            string? vsFolder = directoryContent.FirstOrDefault(x => x.Contains(".vs", StringComparison.CurrentCultureIgnoreCase));
            CleanDirectory(vsFolder);
        }

        private void CleanBinFolder(IEnumerable<string> directoryContent)
        {
            if (!_cleanerConfigurations.CleanBin)
                return;

            string? binFolder = directoryContent.FirstOrDefault(x => x.Contains("bin", StringComparison.CurrentCultureIgnoreCase));
            CleanDirectory(binFolder);
        }

        private void CleanObjFolder(IEnumerable<string> directoryContent)
        {
            if (!_cleanerConfigurations.CleanBin)
                return;

            string? objFolder = directoryContent.FirstOrDefault(x => x.Contains("obj", StringComparison.CurrentCultureIgnoreCase));
            CleanDirectory(objFolder);
        }

        private void CleanPackagesFolder(IEnumerable<string> directoryContent)
        {
            if (!_cleanerConfigurations.CleanPackages)
                return;

            string? packagesFolder = directoryContent.FirstOrDefault(x => x.Contains("packages", StringComparison.CurrentCultureIgnoreCase));
            CleanDirectory(packagesFolder);
        }

        private void CleanDirectory(string? directory)
        {
            if (string.IsNullOrEmpty(directory))
                return;


            if (!_verificationMode)
            {
                Console.WriteLine("Deleting " + directory);
                Directory.Delete(directory, true);
            }
            else
                Console.WriteLine("Would delete " + directory);
        }
    }
}
