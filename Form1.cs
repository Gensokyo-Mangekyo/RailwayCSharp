using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Railway
{
    public partial class Form1 : Form
    {
        internal readonly Configuration Config;
        public Form1()
        {
            Config = Startup.configuration;
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace("") && string.IsNullOrWhiteSpace(FolderTextBox.Text))
            {
                if (Directory.Exists(Config.Path))
                {
                    string DirectoryString = Config.Path  + @"\" + FolderTextBox.Text;
                    Directory.CreateDirectory(DirectoryString);
                  
                }
                else MessageBox.Show("Неверно указана директория", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Данные не введены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult dialogResult= dialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    Config.SavePath(dialog.SelectedPath);
                }
            }
            
        }
    }
}
