using System;
using System.Linq;
using System.Threading.Tasks;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Licenta.Inventory.Messages;
using Licenta.Messaging.Messages.Events;
using MassTransit;

namespace Licenta.Inventory.Consumers
{
    class ProductUpdatedEventConsumer : IConsumer<IProductUpdatedEvent>
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _repository;

        public ProductUpdatedEventConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Product>();
        }

        public async Task Consume(ConsumeContext<IProductUpdatedEvent> context)
        {
            var updatedProductId = context.Message.Product.ProductId;
            var inventory = context.Message.Product.Inventory;

            if (_repository.AllEntities().Any(x => x.Id == updatedProductId))
            {
                var productToUpdate = _repository.AllEntities().First(x => x.Id == updatedProductId);
                _repository.Update(productToUpdate);
                productToUpdate.Items = inventory;

                await _unitOfWork.SaveChangesAsync();
                await Console.Out.WriteLineAsync($"Product {updatedProductId} number of items changed.");
            }
            else
            {
                _repository.Add(new Product
                {
                    Id = updatedProductId,
                    Items = inventory
                });

                await _unitOfWork.SaveChangesAsync();
                await Console.Out.WriteLineAsync($"Product {updatedProductId} added.");
            }

            await context.Publish(CreateInventoryUpdatedEvent(updatedProductId, inventory));
        }

        private IProductInventoryUpdatedEvent CreateInventoryUpdatedEvent(int productId, int inventory)
        {
            return new ProductInventoryUpdatedEvent
            {
                ProductId = productId,
                Inventory = inventory
            };
        }
    }
}
