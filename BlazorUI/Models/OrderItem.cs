namespace BlazorUI.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; } 
        public int Quantity { get; set; } 
        public double Price { get; set; }
        public double Discount { get; set; } = 0.0;
    }
}
