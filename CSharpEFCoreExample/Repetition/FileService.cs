namespace CSharpEFCoreExample.Repetition
{
    internal class FileService
    {
        public IPathsOperations Path { get; private set; }

        public FileService()
        {
            Path = new PathsOperations();
        }
    }
}
