using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Учет_товаров_на_складе.Models;

namespace Учет_товаров_на_складе
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            // Заполнение выпадающих списков
            using (GoodsContext db = new GoodsContext())
            {
                // Заполнение сomboBox1 поставщиками
                foreach (var supplier in db.Suppliers)
                {
                    comboBox1.Items.Add(supplier.CompanyName);
                }

                // Заполнение сomboBox2 складами
                foreach (var warehouse in db.Warehouses)
                {
                    comboBox2.Items.Add(warehouse.Address);
                }
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            double amount = 0;
            double price = 0;
            bool error = false;

            // Проверка поля количества на правильность ввода 
            try
            {
                amount = double.Parse(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Неверный ввод данных  в поле \"Количество\"", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                error = true;
            }

            // Проверка поля цена на правильность ввода 
            try
            {
                price = double.Parse(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Неверный ввод данных  в поле \"Цена\"", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Text = "";
                error = true;
            }

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "")
            {
                using (GoodsContext db = new GoodsContext())
                {
                    int supplierId = 0;
                    int warehuseId = 0;
                    try
                    {
                        // Получение Id поставщика
                        int temp = db.Suppliers.Where(el => el.CompanyName == comboBox1.Text).Select(el => el.Id).First();
                        supplierId = Convert.ToInt32(temp);
                    }
                    catch
                    {
                        MessageBox.Show("Такого поставщика нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox1.Text = "";
                        error = true;
                    }

                    try
                    {
                        // Получение Id склада
                        int temp = db.Warehouses.Where(el => el.Address == comboBox2.Text).Select(el => el.Id).First();
                        warehuseId = Convert.ToInt32(temp);
                    }
                    catch
                    {
                        MessageBox.Show("Такого склада нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox2.Text = "";
                        error = true;
                    }
                    // Создание объекта товара
                    Product product = new Product()
                    {
                        Name = textBox1.Text,
                        Amount = amount,
                        Price = price,
                        SupplierId = supplierId,
                        WarehouseId = warehuseId
                    };

                    if (!error) 
                    {
                        // Добавление товара
                        db.Products.Add(product);
                        await db.SaveChangesAsync();
                    }
                }
            }
            if (!error)
                Close();
        }
    }
}
