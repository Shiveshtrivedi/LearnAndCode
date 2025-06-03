using InventoryManagement.DTOs;

namespace InventoryMangement.DTOs
{
    public class ProductUpdateDto : ProductCreateDto
    {
        public int ProductId { get; set; }
    }

}
