using System;
using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.Models;

namespace Services
{
    class Change : IEditor
    {
        public void Product(Product _product)
        {
            using (GoodsContext db = new GoodsContext())
            {
                var product = db.Products.Where(pr => pr.Id == _product.Id).FirstOrDefault();

                product.Name = _product.Name;

                try 
                { 
                    product.Price = _product.Price;
                }
                catch
                {
                    MessageBox.Show("Неправильный ввод в поле \"Количество\"", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    product.SupplierId = _product.Supplier.Id;
                }
                catch
                {
                    if (!(_product.Supplier.CompanyName == ""))
                    {
                        MessageBox.Show("Такого поставщика нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                try
                {
                    product.WarehouseId = _product.Warehouse.Id;
                }
                catch
                {
                    MessageBox.Show("Такого склада нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                db.SaveChanges();
            }
        }

        public void Supplier(Supplier _supplier)
        {
            using (GoodsContext db = new GoodsContext())
            {
                Supplier supplier = new Supplier();

                try
                {
                    supplier = db.Suppliers.Where(item => item.Id == _supplier.Id).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("Такого поставщика нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохраняем изменения
                supplier.CompanyName = _supplier.CompanyName;
                supplier.Contacts = _supplier.Contacts;
                db.SaveChanges();
            }
        }

        public void Warehouse(Warehouse _warehouse)
        {
            using (GoodsContext db = new GoodsContext())
            {
                Warehouse warehouse = new Warehouse();

                try
                {
                    warehouse = db.Warehouses.Where(item => item.Id == _warehouse.Id).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("Такого склада нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохраняем изменения
                warehouse.Address = _warehouse.Address;
                db.SaveChanges();
            }
        }
    }
}
