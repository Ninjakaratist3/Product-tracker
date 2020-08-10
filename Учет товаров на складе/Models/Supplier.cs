using System.Collections.Generic;

namespace Учет_товаров_на_складе.Models
{
    public class Supplier
    {
        // Поля таблицы
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Contacts { get; set; }

        // Список товаров для определенного поставщика
        public virtual List<Product> Products { get; set; }
    }
}
