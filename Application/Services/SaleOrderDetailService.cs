using Application.Models;
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
    public class SaleOrderDetailService
    {
        private readonly ISaleOrderDetailRepository _saleOrderRepository;
        private readonly IProductRepository _productRepository;

        public SaleOrderDetailService(ISaleOrderDetailRepository saleOrderRepository, IProductRepository productRepository)
        {
            _saleOrderRepository = saleOrderRepository;
            _productRepository = productRepository;
        }

        public List<SaleOrderDetail> GetAllBySaleOrder(int saleOrderId)
        {
            return _saleOrderRepository.GetAllBySaleOrder(saleOrderId);
        }

        public List<SaleOrderDetail> GetAllByProducts(int productId)
        {
            return _saleOrderRepository.GetAllByProduct(productId);
        }

        public SaleOrderDetail? Get(int id)
        {
            return _saleOrderRepository.Get(id);
        }

        public void Add(SaleOrderDetailCreateDTO dto)
        {
            var product = _productRepository.Get(dto.ProductId);
            if (product == null)
            {
                throw new NotAllowedException("El producto no fue encontrado");
            }

            var saleOrderDetail = new SaleOrderDetail()
            {
                ProductId = dto.ProductId,
                SaleOrderId = dto.SaleOrderId,
                Amount = dto.Amount,
                UnitPrice = product.Price,
            };
        }

        public void Delete(int id)
        {
            var saleOrderDelete = _saleOrderRepository.Get(id);
            if (saleOrderDelete != null)
            {
                _saleOrderRepository.Delete(saleOrderDelete);
            }
        }

        public void Update(int id, SaleOrderDetailUpdateDTO dto)
        {
            var saleOrderDetailUpdate = _saleOrderRepository.Get(id);
            if(saleOrderDetailUpdate == null)
            {
                throw new NotAllowedException("No se encontro ningun detalle de venta");
            }
            var product = _productRepository.Get(dto.ProductId);
            if(product == null)
            {
                throw new NotAllowedException("No se encontro ningun producto");
            }

            saleOrderDetailUpdate.ProductId = dto.ProductId;
            saleOrderDetailUpdate.Amount = dto.Amount;
           
        }
    }
}
