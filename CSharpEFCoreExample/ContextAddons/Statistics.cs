namespace CSharpEFCoreExample.ContextAddons
{
    internal class Statistics
    {
        public List<string> LogLines { get; set; }
        public string SepStart { get; set; }
        public string SepStop { get; set; }

        public Statistics()
        {
            LogLines = new List<string>();
            SepStart = "<--------------------";
            SepStop = "-------------------->";
        }
    }
}
