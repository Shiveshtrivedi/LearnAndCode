using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using InventoryMangement.Repositories.Interface;
using InventoryMangement.Services.Interface;

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

        public Supplier GetOrCreateDefaultSupplier(int supplierId)
        {
            var supplier = _supplierRepository.GetSupplierById(supplierId);

            if (supplier == null)
            {
                supplier = _supplierRepository
                    .GetAllSuppliers()
                    .FirstOrDefault(suppliers => suppliers.SupplierName.Equals("Default Supplier", StringComparison.OrdinalIgnoreCase));

                if (supplier == null)
                {
                    supplier = new Supplier
                    {
                        SupplierId = IdGenerator.GetNextId(),
                        SupplierName = "Default Supplier"
                    };

                    _supplierRepository.AddSupplier(supplier);
                }
            }

            return supplier;
        }


    }
}
