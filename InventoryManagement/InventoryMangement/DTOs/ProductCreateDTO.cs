namespace InventoryManagement.DTOs
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }

}
