using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private void button3_Click(object sender, EventArgs e)
        {   
            if (NumberTrainBox.Text != "" && FolderNameBox.Text != "")
            {
                string TrainFolder = "\\ЭПЗД " + NumberTrainBox.Text + " за " + dateTimePicker1.Value.ToString("d");
                string Result = TrainFolder + "\\" + FolderNameBox.Text;
                if (!Directory.Exists(Config.Path + TrainFolder))
                {
                    Directory.CreateDirectory(Config.Path + Result);
                    MessageBox.Show($"Каталог {Result} создан успешно ", "Информация");
                }
                else
                    MessageBox.Show("Такой каталог поезда уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Некоректный ввод данных","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private string CheckNetworkPath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.ValidateNames = false;
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                openFileDialog.FileName = "Выбирете сетевой путь";
                DialogResult dialogResult = openFileDialog.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    if (openFileDialog.FileName.StartsWith("\\"))
                    {
                        return openFileDialog.FileName.Remove(openFileDialog.FileName.LastIndexOf('\\'));
                    }
                    else MessageBox.Show("Некоректный cетевой путь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.SaveG1(CheckNetworkPath());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Config.SaveG2(CheckNetworkPath());
        }

        void SVN(string G,string s)
        {
            if (G == null)
            {
                MessageBox.Show(s, "Ошибка конфигурации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var form = new Form2(G);
            form.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SVN(Config.G1,"Не найден сетевой путь СВН Г1");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SVN(Config.G2, "Не найден сетевой путь СВН Г9");
        }
    }
}
