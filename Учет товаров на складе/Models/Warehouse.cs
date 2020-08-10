using System.Collections.Generic;

namespace Учет_товаров_на_складе.Models
{
    public class Warehouse
    {
        // Поля таблицы
        public int Id { get; set; }
        public string Address { get; set; }

        // Список товаров определенного склада
        public virtual List<Product> Products { get; set; }
    }
}
