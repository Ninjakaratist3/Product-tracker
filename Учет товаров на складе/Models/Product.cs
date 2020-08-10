using System.ComponentModel.DataAnnotations.Schema;

namespace Учет_товаров_на_складе.Models
{
    public class Product : AbstractProduct
    {
        // Поля, описывающие товар получаем из базового класса

        // Поля для связи с другими таблицами
        // Поставщик
        public int? SupplierId { get; set; }
        // Склад
        public int WarehouseId { get; set; }

        // Поля для получения значений из других таблиц
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }
    }
}
