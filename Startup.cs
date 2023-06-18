using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Railway
{
     class Startup
    {
        private static Startup instance;
        public static Configuration configuration { get; private set; }

        public static void SaveConfig(string path = null, string G1 = null, string G2 = null)
        {
            string Directory = Environment.CurrentDirectory + "\\Config.dat";
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(Directory, FileMode.OpenOrCreate))
            {
                if (path != null)
                configuration.Path = path;
                if (G1 != null)
                configuration.G1 = G1;
                if (G2 != null)
                configuration.G2 = G2;
                formatter.Serialize(fs, configuration);
            }
        }

        public static Startup onInit() //Singleton
        {
            if (instance == null)
            {
                instance = new Startup();
                configuration = new Configuration();
                string Directory = Environment.CurrentDirectory + "\\Config.dat";
                BinaryFormatter formatter = new BinaryFormatter();
                if (!File.Exists(Directory))
                {
                    SaveConfig(Environment.CurrentDirectory);
                }
                else
                {
                    using (FileStream fs = new FileStream(Directory, FileMode.Open))
                    {
                        configuration = (Configuration)formatter.Deserialize(fs);
                    }
                }
            }
            return instance;
        }
    }
}
