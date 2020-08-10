using System.Linq;
using System.Windows.Forms;
using Учет_товаров_на_складе.Models;
using Учет_товаров_на_складе.Edition;

namespace Services
{
    public class Remove : IEditor
    {
        public async void Product(Product _product)
        {
            using (GoodsContext db = new GoodsContext())
            {
                // Получение выбранного в таблице продукта
                Product DeleteProduct = db.Products.Where(pr => pr.Id == _product.Id).FirstOrDefault();

                // Удаление
                db.Products.Remove(DeleteProduct);
                await db.SaveChangesAsync();
            }
        }

        public async void Supplier(Supplier _supplier)
        {
            using (GoodsContext db = new GoodsContext())
            {
                Supplier delItem = new Supplier();

                // Получаем поставщика из БД
                try
                {
                    delItem = db.Suppliers.Where(item => item.Id == _supplier.Id).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("Такого поставщика нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var products = db.Products.Where(item => item.SupplierId == delItem.Id).ToList();

                foreach (var product in products)
                {
                    product.SupplierId = null;
                }

                // Удаление
                db.Suppliers.Remove(delItem);
                await db.SaveChangesAsync();
            }
        }

        public async void Warehouse(Warehouse _warehouse)
        {
            using (GoodsContext db = new GoodsContext())
            {
                WarehouseRemove warehouseRemove = new WarehouseRemove();

                warehouseRemove.delAddress = _warehouse.Address;
                warehouseRemove.ShowDialog();
                string newAddress = warehouseRemove.ChangeWarehouse();
                bool del = warehouseRemove.Del;

                // Получаем склад из БД
                Warehouse delItem = new Warehouse();
                try
                {
                    delItem = db.Warehouses.Where(item => item.Id == _warehouse.Id).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("Такого склада нет в базе", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получем все продукты с этого склада для удаления
                var products = db.Products.Where(item => item.WarehouseId == delItem.Id).ToList();

                if (del)
                {
                    db.Products.RemoveRange(products);
                }
                else
                {
                    if (newAddress != "")
                    {
                        int id = db.Warehouses.Where(item => item.Address == newAddress).FirstOrDefault().Id;
                        foreach (var product in products)
                            product.WarehouseId = id;
                    }
                    else
                    {
                        return;
                    }
                }

                // Удаление
                db.Warehouses.Remove(delItem);
                await db.SaveChangesAsync();
            }
        }
    }
}
