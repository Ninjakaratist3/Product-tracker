using System;
using System.Windows.Forms;
using Учет_товаров_на_складе.DataBase;

namespace Учет_товаров_на_складе
{
    public partial class AddWarehouse : Form
    {
        public AddWarehouse()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Добавление склада в БД
            if (textBox1.Text != "")
            {
                Warehouse warehouse = new Warehouse()
                {
                    Address = textBox1.Text
                };

                using (GoodsContext db = new GoodsContext())
                {
                    db.Warehouses.Add(warehouse);
                    await db.SaveChangesAsync();
                }
            }
            Close();
        }
    }
}
