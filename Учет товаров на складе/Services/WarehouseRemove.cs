using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.Models;

namespace Учет_товаров_на_складе.Edition
{
    public partial class WarehouseRemove : Form
    {
        public bool Del;
        public string delAddress;

        public WarehouseRemove()
        {
            InitializeComponent();
        }

        public string ChangeWarehouse()
        {
            return comboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Del = false;
            label1.Text = "Перенести в ";
            comboBox1.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
            using (GoodsContext db = new GoodsContext())
            {
                var warehouses = db.Warehouses.Where(item => item.Address != delAddress).Select(item => item.Address).ToArray();
                comboBox1.Items.AddRange(warehouses);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Del = true;
            Close();
        }

        private void WarehouseRemove_Load(object sender, EventArgs e)
        {
            label1.Text = "Вы удаляете склад из списка. Удалить товары данного склада или перенести их в другой?";
            comboBox1.Visible = false;
            button3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool error = true;
            using (GoodsContext db = new GoodsContext())
            {
                var warehouses = db.Warehouses.Where(item => item.Address != delAddress).Select(item => item.Address).ToArray();
                if (!warehouses.Contains(comboBox1.Text))
                {
                    MessageBox.Show("Такого склада нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error = false;
                }
            }
            if (error)
                Close();
        }
    }
}
