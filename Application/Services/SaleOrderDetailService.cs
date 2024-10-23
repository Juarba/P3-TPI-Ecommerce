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
    public class SaleOrderDetailService : ISaleOrderDetailService
    {
        private readonly ISaleOrderDetailRepository _saleOrderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleOrderRepository _saleOrderRepository;

        public SaleOrderDetailService(ISaleOrderDetailRepository saleOrderDetailRepository, IProductRepository productRepository, ISaleOrderRepository saleOrderRepository)
        {
            _saleOrderDetailRepository = saleOrderDetailRepository;
            _productRepository = productRepository;
            _saleOrderRepository = saleOrderRepository;
        }

        public List<SaleOrderDetailResponseDTO> GetAllBySaleOrder(int saleOrderId)
        {
            var saleOrder = _saleOrderRepository.Get(saleOrderId);
            return saleOrder.SaleOrderDetails.Select(detail => new SaleOrderDetailResponseDTO
            {
                Id = detail.Id,
                Amount = detail.Amount,
                Product = new ProductResponseDTO
                {
                    Id = detail.Product.Id,
                    Name = detail.Product.Name,
                    Price = detail.Product.Price
                }
            }).ToList();

        }

        public List<SaleOrderDetailResponseDTO> GetAllByProducts(int productId)
        {
           var product = _productRepository.Get(productId);
           
            var saleOrderDetails = _saleOrderDetailRepository.GetAllByProduct(productId);
            return saleOrderDetails.Select(s => new SaleOrderDetailResponseDTO
            {
                Id = s.Id,
                Amount = s.Amount,
                Product = new ProductResponseDTO
                {
                    Id = s.Product.Id,
                    Name = s.Product.Name,
                    Price = s.Product.Price
                }
            }).ToList();
        }

        public SaleOrderDetailResponseDTO? Get(int id)
        {
            var saleOrderDetail = _saleOrderDetailRepository.Get(id);
            if(saleOrderDetail is null)
            {
                return null;
            }
            return new SaleOrderDetailResponseDTO
            {
                Id = saleOrderDetail.Id,
                Amount = saleOrderDetail.Amount,
                Product = new ProductResponseDTO
                {
                    Id = saleOrderDetail.Product.Id,
                    Name = saleOrderDetail.Product.Name,
                    Price = saleOrderDetail.Product.Price
                }
            };
        }

        public void Add(SaleOrderDetailCreateDTO dto)
        {
            var product = _productRepository.Get(dto.ProductId);
            

            var saleOrderDetail = new SaleOrderDetail()
            {
                ProductId = dto.ProductId,
                SaleOrderId = dto.SaleOrderId,
                Amount = dto.Amount,
                UnitPrice = product.Price
            };

            _saleOrderDetailRepository.Add(saleOrderDetail);

            var saleOrder = _saleOrderRepository.Get(dto.SaleOrderId);
            if(saleOrder is not null)
            {
                saleOrder.Total += saleOrderDetail.Amount * product.Price;
                _saleOrderRepository.Update(saleOrder);
            }
        }

        public void Delete(int id)
        {
            var saleOrderDelete = _saleOrderDetailRepository.Get(id);
            if (saleOrderDelete is not null)
            {
                var saleOrder = _saleOrderRepository.Get(saleOrderDelete.SaleOrderId);
                if(saleOrder is not null)
                {
                    saleOrder.Total -= saleOrderDelete.Amount * saleOrderDelete.UnitPrice;
                    _saleOrderRepository.Update(saleOrder);
                }

                _saleOrderDetailRepository.Delete(saleOrderDelete);
            }
        }

        public void Update(int id, SaleOrderDetailUpdateDTO dto)
        {
            var saleOrderDetailUpdate = _saleOrderDetailRepository.Get(id);
            if(saleOrderDetailUpdate == null)
            {
                throw new NotFoundException("No se encontro ningun detalle de venta");
            }
            var product = _productRepository.Get(dto.ProductId);
            if(product == null)
            {
                throw new NotFoundException("No se encontro ningun producto");
            }

            saleOrderDetailUpdate.ProductId = dto.ProductId;
            saleOrderDetailUpdate.Amount = dto.Amount;
           
        }
    }
}
