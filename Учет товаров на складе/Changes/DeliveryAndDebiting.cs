using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.Models;

namespace Учет_товаров_на_складе
{
    public partial class DeliveryAndDebiting : Form
    {
        public string productName;

        public DeliveryAndDebiting()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            label1.Text = "Добавить";
            button1.Text = "Добавить";
            checkBox1.Checked = true;
        }

        // Метод для загрузки данных и вывода их
        public void ProductLoad (string name)
        {
            productName = name;
            textBox1.Text = productName;
        }

        public void Add(double countOfProduct, ref string text)
        {
            using (GoodsContext db = new GoodsContext())
            {
                // Изменение кол-ва выбранного товара
                var product = db.Products.Where(pr => pr.Name == productName).Select(pr => pr).First();
                product.Amount += countOfProduct;
                db.SaveChanges();
                // Текст для истории изменений
                text = $"Пополнение\r\n\n" +
                $"{DateTime.Now.ToShortDateString()} было пополнено {countOfProduct} шт. \"{product.Name}\" " +
                $"на склад по адресу: {product.Warehouse.Address}, на сумму {countOfProduct * product.Price} руб.";
            }
        }

        public void Sell(double countOfProduct, ref string text)
        {
            using (GoodsContext db = new GoodsContext())
            {
                // Изменение кол-ва выбранного товара
                var product = db.Products.Where(pr => pr.Name == productName).Select(pr => pr).First();
                product.Amount -= countOfProduct;
                db.SaveChanges();
                // Текст для истории изменений
                text = $"Продажа\r\n\n" +
                        $"{DateTime.Now.ToShortDateString()} было продано {countOfProduct} шт. \"{product.Name}\" " +
                        $"со склада по адресу: {product.Warehouse.Address}, на сумму {countOfProduct * product.Price} руб.";
            }
        }

        public void WriteOff(double countOfProduct, ref string text)
        {
            using (GoodsContext db = new GoodsContext())
            {
                // Изменение кол-ва выбранного товара
                var product = db.Products.Where(pr => pr.Name == productName).Select(pr => pr).First();
                product.Amount -= countOfProduct;
                db.SaveChanges();
                // Текст для истории изменений
                text = $"Списание\r\n\n" +
                        $"{DateTime.Now.ToShortDateString()} было списано {countOfProduct} шт. \"{product.Name}\" " +
                        $"со склада по адресу: {product.Warehouse.Address}, на сумму {countOfProduct * product.Price} руб.\r";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "";
            double countOfProduct = 0;
            bool error = true;
            // Кол-во продуктов для изменения
            try
            {
                countOfProduct = Convert.ToDouble(textBox2.Text);
                if (countOfProduct < 0)
                    throw new ArgumentException();
            }
            catch
            {
                MessageBox.Show("Неверное количество товара", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                error = false;
            }
            if (error) 
            {
                if (radioButton1.Checked)
                    Add(countOfProduct, ref text);
                else if (radioButton2.Checked)
                    Sell(countOfProduct, ref text);
                else
                    WriteOff(countOfProduct, ref text);

                if (checkBox1.Checked)
                {
                    CreateFile();
                    FileWrite(text);
                }
                Close();
            }
        }

        private void CreateFile()
        {
            // Путь до файла
            string path = Application.StartupPath + $"\\История пополнения и списания.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                // Создание файла
                FileStream fileStream = file.Create();
                fileStream.Close();
            }
        }

        private void FileWrite(string text)
        {
            // Путь до файла
            string path = Application.StartupPath + $"\\История пополнения и списания.txt";
            // Запись текста в файл
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(text);
            sw.Close();
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Text = "Добавить";
            button1.Text = "Добавить";
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Text = "Продать";
            button1.Text = "Продать";
        }

        private void radioButton3_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Text = "Списать";
            button1.Text = "Списать";
        }

    }
}
