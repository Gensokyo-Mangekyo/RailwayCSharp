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
    public partial class Form2 : Form
    {
        string Path = "";
        string year = "";
        string number = "";
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string s)
        {
            InitializeComponent();
            Path = s;
            listBox1.Items.AddRange(GetNamesDirectory(Path));
        }

        string[] GetNamesDirectory(string directory)
        {
            string[] dirs = Directory.GetDirectories(directory);
            for (int i = 0; i < dirs.Length; i++)
            {
                dirs[i] = dirs[i].Remove(0, dirs[i].LastIndexOf('\\')+1);
            }
            return dirs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] Limit1 = maskedTextBox1.Text.Split(':');
            string[] Limit2 = maskedTextBox1.Text.Split(':');
            foreach (var item in listBox3.Items)
            {

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            year = listBox1.SelectedItem as string;
            string directory = Path + "\\" + year;
            listBox2.Items.AddRange(GetNamesDirectory(directory));
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            number  = listBox2.SelectedItem as string;
            string directory = Path + "\\" + year + "\\" + number;
            foreach (var item in Directory.GetDirectories(directory))
            {
                if (item.EndsWith("K"))
                {
                    listBox3.Items.Clear();
                    string[] files = Directory.GetFiles(item);
                    for (int i = 0; i < files.Length; i++)
                    {
                        files[i] = files[i].Remove(0, files[i].LastIndexOf('\\') + 1);
                        files[i] = files[i].Remove(files[i].IndexOf('.'));
                    }
                    listBox3.Items.AddRange(files);
                    return;
                }
            }
            MessageBox.Show("Папка с машинистами не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
