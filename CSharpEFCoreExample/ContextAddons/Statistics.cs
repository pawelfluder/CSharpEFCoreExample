namespace CSharpEFCoreExample.ContextAddons
{
    internal class Statistics
    {
        private List<string> LogLines { get; set; }
        public string SepStart { get; set; }
        public string SepStop { get; set; }

        public Statistics()
        {
            LogLines = new List<string>();
            SepStart = "<--------------------";
            SepStop = "-------------------->";
        }

        public void AddLogLines(string logLine)
        {
            LogLines.Add(logLine);
        }

        public void AddLogLines(List<string> logLineList)
        {
            LogLines.AddRange(logLineList);
        }

        public void Print()
        {
            LogLines.ForEach(x => Console.WriteLine(x));
            LogLines.Clear();
        }
    }
}
