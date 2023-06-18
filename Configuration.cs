namespace Railway
{
    internal class Configuration
    {
        public string Path { get; private set; }

        public Configuration()
        {
            
        }

        public void SavePath (string path)
        {
            Path = path;
        }
    }
}