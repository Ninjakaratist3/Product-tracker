using System;
using System.IO;
using System.Windows.Forms;

namespace Учет_товаров_на_складе
{
    public partial class History : Form
    {
        string path = Application.StartupPath + $"\\История пополнения и списания.txt";

        public History()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            //StreamWriter writer = new StreamWriter(path);
            //writer.WriteLine();
            //writer.Close();
        }

        private void History_Load(object sender, EventArgs e)
        {
            textBox1.Text = TextRead();
            textBox1.TabStop = false;
        }

        public string TextRead()
        {
            StreamReader reader = new StreamReader(path);
            return reader.ReadToEnd();
        }
    }
}
