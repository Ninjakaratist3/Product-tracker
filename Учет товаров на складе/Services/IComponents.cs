using Учет_товаров_на_складе.Models;

namespace Edition
{
    interface IComponents
    {
        void Product(Product product);
        void Supplier(Supplier supplier);
        void Warehouse(Warehouse warehouse);
    }
}
