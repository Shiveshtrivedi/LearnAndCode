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
    public class InventoryRepositoryTests : IDisposable
    {
        private readonly InventoryRepository _repository;

        public InventoryRepositoryTests()
        {
            InventoryDb.InventoryData.Clear();
            InventoryDb.InventoryData.AddRange(InventoryRepositoryMockData.Inventories);

            _repository = new InventoryRepository();
        }

        public void Dispose()
        {
            InventoryDb.InventoryData.Clear();
        }

        [Fact]
        public void GetAllInventories_ReturnsAllInventories()
        {
            var inventories = _repository.GetAllInventories();

            Assert.Equal(2, inventories.Count());
        }

        [Fact]
        public void GetInventoryByProductId_ExistingProductId_ReturnsInventoryQuantity()
        {
            var inventory = _repository.GetInventoryByProductId(1);

            Assert.Equal(100, inventory.QuantityAvailable);
        }

        [Fact]
        public void GetInventoryByProductId_NonExistingProductId_ThrowsInventoryException()
        {
            var exception = Assert.Throws<InventoryException>(() => _repository.GetInventoryByProductId(99));
            Assert.Equal("Inventory Not Found", exception.Message);
        }

        [Fact]
        public void AddInventory_NewInventory_ReturnsSuccess()
        {
            var newInventory = new Inventory { ProductId = 3, QuantityAvailable = 75 };

            var result = _repository.AddInventory(newInventory);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void AddInventory_NewInventory_AddsInventoryToDb()
        {
            var newInventory = new Inventory { ProductId = 3, QuantityAvailable = 75 };
            _repository.AddInventory(newInventory);

            Assert.Contains(newInventory, InventoryDb.InventoryData);
        }

        [Fact]
        public void AddInventory_ExistingProductId_ReturnsFail()
        {
            var duplicateInventory = new Inventory { ProductId = 1, QuantityAvailable = 200 };

            var result = _repository.AddInventory(duplicateInventory);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void AddInventory_ExistingProductId_ReturnsAlreadyExistsErrorCode()
        {
            var duplicateInventory = new Inventory { ProductId = 1, QuantityAvailable = 200 };

            var result = _repository.AddInventory(duplicateInventory);

            Assert.Equal(ErrorCode.AlreadyExists, result.ErrorCode);
        }

        [Fact]
        public void UpdateInventory_ExistingProductId_ReturnsSuccess()
        {
            var result = _repository.UpdateInventory(1, 150);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void UpdateInventory_ExistingProductId_UpdatesQuantity()
        {
            _repository.UpdateInventory(1, 150);
            var updatedInventory = _repository.GetInventoryByProductId(1);

            Assert.Equal(150, updatedInventory.QuantityAvailable);
        }

        [Fact]
        public void UpdateInventory_NonExistingProductId_ThrowsInventoryException()
        {
            var exception = Assert.Throws<InventoryException>(() => _repository.UpdateInventory(99, 100));
            Assert.Equal("Inventory Not Found", exception.Message);
        }
    }
}
