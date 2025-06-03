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

        public void AddSupplier(Supplier supplier)
        {
            supplier.SupplierId = IdGenerator.GetNextId();

            _supplierRepository.AddSupplier(supplier);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAllSuppliers();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierRepository.GetSupplierById(supplierId);
        }

        public void DeleteSupplier(int supplierId)
        {
            _supplierRepository.DeleteSupplier(supplierId);
        }

    }
}
