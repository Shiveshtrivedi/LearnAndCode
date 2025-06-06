using InventoryManagement.Context;
using InventoryManagement.Enum;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Tests.MockData;
using System;
using System.Linq;
using Xunit;

namespace InventoryManagement.Tests
{
    public class SupplierRepositoryTests : IDisposable
    {
        private readonly SupplierRepository _repository;

        public SupplierRepositoryTests()
        {
            SupplierDb.SupplierData.Clear();
            SupplierDb.SupplierData.AddRange(SupplierRepositoryMockData.Suppliers);

            _repository = new SupplierRepository();
        }

        public void Dispose()
        {
            SupplierDb.SupplierData.Clear();
        }

        [Fact]
        public void GetAllSuppliers_ReturnsAllSuppliers()
        {
            var suppliers = _repository.GetAllSuppliers();

            Assert.Equal(2, suppliers.Count());
        }

        [Fact]
        public void GetSupplierById_ExistingId_ReturnsSupplier()
        {
            var supplier = _repository.GetSupplierById(1);

            Assert.Equal("Supplier One", supplier.SupplierName);
        }

        [Fact]
        public void GetSupplierById_NonExistingId_ThrowsSupplierNotFoundException()
        {
            Assert.Throws<SupplierNotFoundException>(() => _repository.GetSupplierById(99));
        }

        [Fact]
        public void AddSupplier_NewSupplier_ReturnsSuccess()
        {
            var newSupplier = new Supplier { SupplierId = 3, SupplierName = "Supplier Three", ContactNumber = "5555555555" };

            var result = _repository.AddSupplier(newSupplier);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void AddSupplier_NewSupplier_AddsSupplierToDb()
        {
            var newSupplier = new Supplier { SupplierId = 3, SupplierName = "Supplier Three", ContactNumber = "5555555555" };
            _repository.AddSupplier(newSupplier);

            Assert.Contains(newSupplier, SupplierDb.SupplierData);
        }

        [Fact]
        public void AddSupplier_ExistingSupplierId_ReturnsFail()
        {
            var duplicateSupplier = new Supplier { SupplierId = 1, SupplierName = "Duplicate Supplier", ContactNumber = "0000000000" };

            var result = _repository.AddSupplier(duplicateSupplier);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void AddSupplier_ExistingSupplierId_ReturnsAlreadyExistsErrorCode()
        {
            var duplicateSupplier = new Supplier { SupplierId = 1, SupplierName = "Duplicate Supplier", ContactNumber = "0000000000" };

            var result = _repository.AddSupplier(duplicateSupplier);

            Assert.Equal(ErrorCode.AlreadyExists, result.ErrorCode);
        }

        [Fact]
        public void DeleteSupplier_ExistingId_ReturnsSuccess()
        {
            var result = _repository.DeleteSupplier(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void DeleteSupplier_ExistingId_RemovesSupplierFromDb()
        {
            _repository.DeleteSupplier(1);

            Assert.DoesNotContain(SupplierDb.SupplierData, s => s.SupplierId == 1);
        }

        [Fact]
        public void DeleteSupplier_NonExistingId_ReturnsFail()
        {
            var result = _repository.DeleteSupplier(99);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void DeleteSupplier_NonExistingId_ReturnsAlreadyExistsErrorCode()
        {
            var result = _repository.DeleteSupplier(99);

            Assert.Equal(ErrorCode.AlreadyExists, result.ErrorCode);
        }
    }
}
