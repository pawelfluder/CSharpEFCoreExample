namespace CSharpEFCoreExample.Repetition
{
    public interface IPathsOperations
    {
        string MoveDirectoriesUp(string path, int level);
        string GetStartupProjectFolderPath();
        string GetProjectFolderPath(string projectName);
    }
}