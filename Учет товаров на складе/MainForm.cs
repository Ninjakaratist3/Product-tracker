using Edition;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.Add;
using Учет_товаров_на_складе.DataBase;

namespace Учет_товаров_на_складе
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        // Очищает таблицу и заполняет ее товарами из БД
        private void DataLoad ()
        {
            dataGridView1.Rows.Clear();
            using (GoodsContext db = new GoodsContext())
            {
                // Получение всех продуктов
                var ListOfProducts = db.Products.ToList();
                // Вывод продуктов в таблицу
                foreach (var product in ListOfProducts)
                {
                    if (product.SupplierId != null)
                        dataGridView1.Rows.Add(product.Name, product.Amount, product.Price, product.Supplier.CompanyName, product.Warehouse.Address);
                    else
                        dataGridView1.Rows.Add(product.Name, product.Amount, product.Price, "-", product.Warehouse.Address);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 5;
            DataLoad();
        }

        // Удаление из таблицы и БД
        private void button2_Click(object sender, EventArgs e)
        {
            string name = Convert.ToString(dataGridView1.SelectedCells[0].Value);
            Remove remove = new Remove();
            remove.Product(name);
            DataLoad();
        }


        // Добавление товара
        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct form2 = new AddProduct();
            form2.ShowDialog();
            DataLoad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddWarehouse addWarehouse = new AddWarehouse();
            addWarehouse.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddSupplier addSupplier = new AddSupplier();
            addSupplier.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeProduct changeProduct = new ChangeProduct();
            changeProduct.ProductLoad(dataGridView1.SelectedCells[0].Value.ToString());
            changeProduct.ShowDialog();
            DataLoad();
        }

        // Кнопка "Найти"
        private void button7_Click(object sender, EventArgs e)
        {
            using (GoodsContext db = new GoodsContext())
            {
                // Получает все продукты содержащие ключевое слово
                var findingProducts = db.Products.Where(pr => pr.Name.ToLower().Contains(textBox1.Text.ToLower())).Select(pr => pr);
                dataGridView1.Rows.Clear();
                foreach (var product in findingProducts) 
                    dataGridView1.Rows.Add(product.Name, product.Amount, product.Price, product.Supplier.CompanyName, product.Warehouse.Address);
            }
        }

        // Кнопка "Очистить"
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            DataLoad();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeliveryAndDebiting deliveryAndDebiting = new DeliveryAndDebiting();
            deliveryAndDebiting.ProductLoad(dataGridView1.SelectedCells[0].Value.ToString());
            deliveryAndDebiting.ShowDialog();
            DataLoad();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SupplierAndWarehouses supplierAndWarehouses = new SupplierAndWarehouses();
            supplierAndWarehouses.ShowDialog();
            DataLoad();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            History historyForm = new History();
            historyForm.Show();
        }
    }
}
