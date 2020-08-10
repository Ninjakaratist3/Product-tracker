using System.Data.Entity;

namespace Учет_товаров_на_складе.Models
{
    // Класс для связи с БД
    // Для работы с БД используется Entity Framework
    public class GoodsContext : DbContext
    {
        public GoodsContext() : base("DBConnection") 
        {
        }

        // Таблицы БД
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
