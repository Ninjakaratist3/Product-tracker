using Edition;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.DataBase;


namespace Учет_товаров_на_складе
{
    public partial class ChangeProduct : Form
    {
        public string productName;

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
            // Новые значения
            string[] fields = new string[] 
            {
                productName, textBox1.Text, textBox3.Text, comboBox1.Text, comboBox2.Text
            };
            Change change = new Change();
            // Изменение
            change.Product(fields);
            Close();
        }
    }
}
