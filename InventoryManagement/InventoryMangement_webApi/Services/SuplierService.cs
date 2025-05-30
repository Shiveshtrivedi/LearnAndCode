using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;

namespace InventoryManagement.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public OperationResult AddSupplier(Supplier supplier)
        {
            supplier.SupplierId = IdGenerator.GetNextId();

            return _supplierRepository.AddSupplier(supplier);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAllSuppliers();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierRepository.GetSupplierById(supplierId);
        }

        public OperationResult DeleteSupplier(int supplierId)
        {
            var result = _supplierRepository.DeleteSupplier(supplierId);
            if (!result.IsSuccess)
                throw new OperationFailedException("DeleteSupplier", result.ErrorMessage);

            return result;
        }

    }
}
