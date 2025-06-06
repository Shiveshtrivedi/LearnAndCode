namespace InventoryManagementConsole.DTOs
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int QuantityAvailable { get; set; }
        public ProductDto Product { get; set; }

    }
}
