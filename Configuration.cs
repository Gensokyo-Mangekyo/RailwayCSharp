using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Railway
{
    [Serializable]
    internal class Configuration
    {
        public string Path { get; set; }
        public string G1 { get; set; }
        public string G2 { get; set; }

        public void SavePath (string path)
        {
            Path = path;
            Startup.SaveConfig(path);
        }

        public void SaveG1(string g1) { 
            G1 = g1;
            Startup.SaveConfig(null,g1);
        }
        public void SaveG2(string g2)
        {
            G2 = g2;
            Startup.SaveConfig(null,null, g2);
        }
    }
}