namespace InventoryMangement.Exceptions
{
    public class DuplicateCategoryException : Exception
    {
        public DuplicateCategoryException(int categoryId)
            : base($"Category with ID {categoryId} already exists.")
        {
        }
    }
}
