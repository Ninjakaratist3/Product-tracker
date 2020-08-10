using Services;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.Models;


namespace Учет_товаров_на_складе
{
    public partial class ChangeProduct : Form
    {
        public string productName;
        private IEditor _editor;

        public ChangeProduct()
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

        // Получаем продукт для вывода его на форму
        public void ProductLoad (string productName)
        {
            this.productName = productName;
            using (GoodsContext db = new GoodsContext())
            {
                var product = db.Products.Where(pr => pr.Name == productName).Select(pr => pr).FirstOrDefault();
                textBox1.Text = product.Name;
                textBox2.Text = product.Amount.ToString();
                textBox3.Text = product.Price.ToString();
                if (product.Supplier == null)
                    comboBox1.Text = "";
                else
                    comboBox1.Text = product.Supplier.CompanyName;
                comboBox2.Text = product.Warehouse.Address;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _editor = new Change();

            Product product;

            using (GoodsContext db = new GoodsContext())
            {
                product = new Product()
                {
                    Id = db.Products.Where(pr => pr.Name == productName).FirstOrDefault().Id,
                    Name = textBox1.Text,
                    Price = Convert.ToDouble(textBox3.Text),
                    Supplier = db.Suppliers.Where(sup => sup.CompanyName == comboBox1.Text).FirstOrDefault(),
                    Warehouse = db.Warehouses.Where(w => w.Address == comboBox2.Text).FirstOrDefault(),
                };
            }

            // Изменение
            _editor.Product(product);
            Close();
        }
    }
}
