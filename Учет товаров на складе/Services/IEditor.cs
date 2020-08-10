using Учет_товаров_на_складе.Models;

namespace Services
{
    interface IEditor
    {
        void Product(Product product);
        void Supplier(Supplier supplier);
        void Warehouse(Warehouse warehouse);
    }
}
