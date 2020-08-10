namespace Учет_товаров_на_складе.Models
{
    // Класс, описывающий абстрактный товар
    public abstract class AbstractProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
    }
}
