using System.Reflection;

namespace CSharpEFCoreExample.Repetition
{
    internal class PathsOperations : IPathsOperations
    {
        public string MoveDirectoriesUp(string path, int level)
        {
            for (int i = 0; i < level; i++)
            {
                path = Directory.GetParent(path).FullName;
            }
            return path;
        }

        public string GetProjectFolderPath(string projectName)
        {
            string startupProjectFolder = default;
            var max = 7;
            var currentFolder = Directory.GetCurrentDirectory();
            var directories = Directory.GetDirectories(currentFolder);

            for (var i = 0; i < max; i++)
            {
                directories = Directory.GetDirectories(currentFolder);
                startupProjectFolder = directories.SingleOrDefault(x => Path.GetFileName(x) == projectName);

                if (startupProjectFolder != default)
                {
                    return startupProjectFolder;
                }

                currentFolder = MoveDirectoriesUp(currentFolder, 1);
            }

            throw new InvalidOperationException();
        }

        public string GetStartupProjectFolderPath()
        {
            string projectName = Assembly.GetEntryAssembly().GetName().Name;
            string projectPath = GetProjectFolderPath(projectName);
            return projectPath;
        }
    }
}
