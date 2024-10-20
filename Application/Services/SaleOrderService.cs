﻿using Application.Interfaces;
using Application.Models;
using Domain.Entities;
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

        public List<SaleOrder> GetAllByClient(int clientId)
        {
            return _repository.GetAllByClient(clientId);
        }

        public SaleOrder? Get(int id) 
        { 
        return _repository.Get(id);
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
                throw new Exception();

            if(update.Shipment is not null)
                product.Shipment=(bool)update.Shipment;

            if(update.PaymentMethod != string.Empty)
                product.PaymentMethod=update.PaymentMethod;
                _repository.Update(product);

            if(update.client is not null)
                product.Client=(Client)update.client;
            
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
