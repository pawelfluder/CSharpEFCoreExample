namespace CSharpEFCoreExample.Repetition
{
    public class FileService
    {
        public IPathsOperations Path { get; private set; }

        public FileService()
        {
            Path = new PathsOperations();
        }
    }
}
