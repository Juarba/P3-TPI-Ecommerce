using Application.Interfaces;
using Application.Models;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SaleOrderService:ISaleOrderService
    {
        private readonly ISaleOrderRepository _repository;

        public SaleOrderService(ISaleOrderRepository repository) 
        {
            _repository = repository;
        }

        public List<SaleOrderResponseDTO> GetAllByClient(int clientId)
        {
            var saleOrders = _repository.GetAllByClient(clientId);
            return saleOrders.Select(s => new SaleOrderResponseDTO
            {
                Id = s.Id,
                Total = s.Total,
                PaymentMethod = s.PaymentMethod,
                Client = new ClientResponseDTO
                {
                    Id = s.Client.Id,
                    Name = s.Client.Name,
                    LastName = s.Client.LastName,
                },
                SaleOrderDetails = s.SaleOrderDetails.Select(d => new SaleOrderDetailResponseDTO
                {
                    Id = d.Id,
                    Amount = d.Amount,
                    Product = new ProductResponseDTO
                    {
                        Id = d.Product.Id,
                        Name = d.Product.Name,
                        Price = d.Product.Price,
                    }
                }).ToList()

            }).ToList();
        }

        public SaleOrderResponseDTO? Get(int id) 
        {
            var saleOrder = _repository.Get(id);
            if(saleOrder is null)
            {
                return null;
            }
            return new SaleOrderResponseDTO
            {
                Id = saleOrder.Id,
                Total = saleOrder.Total,
                PaymentMethod = saleOrder.PaymentMethod,
                Client = new ClientResponseDTO
                {
                    Id = saleOrder.Client.Id,
                    Name = saleOrder.Client.Name,
                    LastName = saleOrder.Client.LastName,

                },
                SaleOrderDetails = saleOrder.SaleOrderDetails.Select(s => new SaleOrderDetailResponseDTO
                {
                    Id = s.Id,
                    Amount = s.Amount,
                    Product = new ProductResponseDTO
                    {
                        Id = s.Product.Id,
                        Name = s.Product.Name,
                        Price = s.Product.Price
                    }

                }).ToList()
            };
        }

        public void Add(SaleOrderCreateDTO createSaleOrder) 
        {
            SaleOrder saleOrder = new SaleOrder()
            {
                ClientId = createSaleOrder.ClientId,
                PaymentMethod = createSaleOrder.PaymentMethod,
            };
            
            _repository.Add(saleOrder);
            
        }

        public void Update(int id, SaleOrderUpdateDTO update) 
        {
            var product = _repository.Get(id);
            if (product is null)
                throw new NotAllowedException();

            if(update.Shipment is not null)
                product.Shipment=(bool)update.Shipment;

            if(update.PaymentMethod != string.Empty)
                product.PaymentMethod=update.PaymentMethod;
                

            if(update.client is not null)
                product.Client=(Client)update.client;
            _repository.Update(product);
        }

        public void Delete(int id) 
        {
            var product = _repository.Get(id);
            if(product is not null) 
            {
                _repository.Delete(product);
            }
        }


    }
}
