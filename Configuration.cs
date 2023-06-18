using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Railway
{
    [Serializable]
    internal class Configuration
    {
        public string Path { get; set; }

     
        public void SavePath (string path)
        {
            Path = path;
            Startup.SaveConfig(path);
        }
    }
}