using Services;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.Models;

namespace Учет_товаров_на_складе.Add
{
    // Класс для редактирования поставщиков и складов
    public partial class SupplierAndWarehouses : Form
    {
        private IEditor _editor;

        public SupplierAndWarehouses()
        {
            InitializeComponent();
            label1.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button3.Visible = false;
        }

        private void SupplierAndWarehouses_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] {"Поставщик", "Склад"});
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool notEmpty = true;
            comboBox2.Items.Clear();
            // Заполнение 2 выпадающего спиcка относительно 1
            using (GoodsContext db = new GoodsContext()) 
            {
                // Если в первом выбран "Поставщик"
                if (comboBox1.SelectedItem.ToString() == "Поставщик")
                {
                    var items = db.Suppliers;
                    foreach (var item in items)
                        comboBox2.Items.Add(item.CompanyName);
                    if (items.Count() <= 0)
                        notEmpty = false;
                }
                // Если в первом выбран "Склад"
                else
                {
                    var items = db.Warehouses;
                    foreach (var item in items)
                        comboBox2.Items.Add(item.Address);
                    if (items.Count() <= 0)
                        notEmpty = false;
                }
            }
            if (notEmpty)
                comboBox2.SelectedIndex = 0;
            else
                comboBox2.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Автоматическое заполнение полей для изменения
            if (comboBox1.SelectedItem.ToString() == "Поставщик")
            {
                using (GoodsContext db = new GoodsContext())
                {
                    // Получаем поставщика из БД по его Названию
                    var supplier = db.Suppliers.Where(item => item.CompanyName == comboBox2.Text).Select(item => item).First();
                    // Заполняем поля формы полученными данными
                    textBox1.Text = supplier.CompanyName;
                    textBox2.Text = supplier.Contacts;
                    db.SaveChanges();
                }
            }
            else
            {
                label2.Visible = false;
                textBox2.Visible = false;
                using (GoodsContext db = new GoodsContext())
                {
                    // Получаем склад из БД по его Адресу
                    var warehouse = db.Warehouses.Where(item => item.Address == comboBox2.Text).Select(item => item).First();
                    // Заполняем поля формы полученными данными
                    textBox1.Text = warehouse.Address;
                    db.SaveChanges();
                }
            }
        }


        // Удаление
        private void button2_Click(object sender, EventArgs e)
        {
            bool error = true;
            if (comboBox1.Text == "Поставщик" && comboBox1.Text == "Склад")
            {
                MessageBox.Show("Данный объект отсутствует в списке", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.SelectedIndex = 0;
                error = false;
            }
            if (error)
            {
                _editor = new Remove();
                if (comboBox1.SelectedItem.ToString() == "Поставщик")
                {
                    Supplier supplier;
                    using (GoodsContext db = new GoodsContext())
                    {
                        supplier = new Supplier()
                        {
                            Id = db.Suppliers.Where(sup => sup.CompanyName == comboBox2.Text).FirstOrDefault().Id
                        };
                    }
                    _editor.Supplier(supplier);
                }
                else if (comboBox1.SelectedItem.ToString() == "Склад")
                {
                    Warehouse warehouse;
                    using (GoodsContext db = new GoodsContext())
                    {
                        warehouse = new Warehouse()
                        {
                            Id = db.Warehouses.Where(w => w.Address == comboBox2.Text).FirstOrDefault().Id
                        };
                    }
                    _editor.Warehouse(warehouse);
                }
                Close();
            }
        }

        // Изменение
        private void button3_Click(object sender, EventArgs e)
        {
            bool error = true;
            if (comboBox1.Text != "Поставщик" && comboBox1.Text != "Склад")
            {
                MessageBox.Show("Данный объект отсутствует в списке", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.SelectedIndex = 0;
                error = false;
            }

            if (error) 
            {
                _editor = new Change();
                if (comboBox1.SelectedItem.ToString() == "Поставщик")
                {
                    Supplier supplier;
                    using (GoodsContext db = new GoodsContext())
                    {
                        supplier = new Supplier()
                        {
                            Id = db.Suppliers.Where(sup => sup.CompanyName == comboBox2.Text).FirstOrDefault().Id,
                            CompanyName = textBox1.Text,
                            Contacts = textBox2.Text
                        };
                    }
                    _editor.Supplier(supplier);
                }
                else
                {
                    Warehouse warehouse;
                    using (GoodsContext db = new GoodsContext())
                    {
                        warehouse = new Warehouse()
                        {
                            Id = db.Warehouses.Where(w => w.Address == comboBox2.Text).FirstOrDefault().Id,
                            Address = textBox1.Text
                        };
                    }
                    _editor.Warehouse(warehouse);
                }
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Поставщик")
            {
                try
                {
                    using (GoodsContext db = new GoodsContext())
                    {
                        var supplier = db.Suppliers.Where(item => item.CompanyName == comboBox2.Text).Select(item => item).First();
                    }
                }
                catch
                {
                    MessageBox.Show("Такого поставщика нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (comboBox1.Text == "Склад")
            {
                try
                {
                    using (GoodsContext db = new GoodsContext())
                    {
                        var warehouse = db.Warehouses.Where(item => item.Address == comboBox2.Text).Select(item => item).First();
                    }
                }
                catch
                {
                    MessageBox.Show("Такого склада нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Вывод полей для определенного класса
            if (comboBox1.SelectedItem.ToString() == "Поставщик")
            {
                label1.Text = "Название";
                label1.Visible = true;
                label2.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                button3.Visible = true;
            }
            else
            {
                label1.Text = "Адрес";
                label1.Visible = true;
                textBox1.Visible = true;
                button3.Visible = true;
            }
        }
    }
}
