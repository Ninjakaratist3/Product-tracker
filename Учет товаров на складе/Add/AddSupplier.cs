using System;
using System.Windows.Forms;
using Учет_товаров_на_складе.DataBase;

namespace Учет_товаров_на_складе
{
    public partial class AddSupplier : Form
    {
        public AddSupplier()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Добавление поставщика в БД
            if(textBox1.Text != "" && textBox2.Text != "")
            { 
                Supplier supplier = new Supplier()
                {
                    CompanyName = textBox1.Text,
                    Contacts = textBox2.Text
                };
                using (GoodsContext db = new GoodsContext())
                {
                    db.Suppliers.Add(supplier);
                    await db.SaveChangesAsync();
                }
            }
            Close();  
        }
    }
}
