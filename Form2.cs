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
        string cabina = "";
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string s)
        {
            InitializeComponent();
            Path = s;
            listBox1.Items.AddRange(GetNamesDirectory(Path));
            if (Startup.configuration.Path != "")
            {
                listBox4.Items.AddRange(GetNamesDirectory(Startup.configuration.Path));
                if (listBox4.Items.Count > 0)
                    listBox4.SelectedItem = listBox4.Items[0];
            }
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
        bool CheckTimeStart(string s, string s1)
        {
            string[] ArrCheck = s.Split('_');
            string[] ArrStart = s1.Split(':');
            if (int.Parse(ArrCheck[0]) > int.Parse(ArrStart[0]))
                return true;
            if (int.Parse(ArrCheck[1]) >= int.Parse(ArrStart[1]) && int.Parse(ArrCheck[0]) == int.Parse(ArrStart[0]))
                return true;
            return false;
        }

        bool CheckTimeEnd(string s, string s1)
        {
            string[] ArrCheck = s.Split('_');
            string[] ArrStart = s1.Split(':');
            if (int.Parse(ArrCheck[0]) < int.Parse(ArrStart[0]))
                return true;
            if (int.Parse(ArrCheck[1]) <= int.Parse(ArrStart[1]) && int.Parse(ArrCheck[0]) == int.Parse(ArrStart[0]))
            return true;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string limit1 = maskedTextBox1.Text;
            string limit2 = maskedTextBox2.Text;
            try
            {
                List<string> Results = new List<string>();
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    if (CheckTimeStart(listBox3.Items[i].ToString(), limit1) && CheckTimeEnd(listBox3.Items[i].ToString(), limit2))
                    {
                        Results.Add(listBox3.Items[i].ToString());
                    }
                }
                foreach (var item in Results)
                {
                   string file = $"{Path}\\{year}\\{number}\\{cabina}\\{item}.mp4";
                    File.Copy(file, Startup.configuration.Path + "\\" + listBox4.SelectedItem.ToString() + "\\Кабина\\" + $"{item}.mp4");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Возникла ошибка в процессе выполнения программы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    cabina = item.Remove(0, item.LastIndexOf('\\') + 1);
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

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.Show();
            Close();
        }
    }
}
