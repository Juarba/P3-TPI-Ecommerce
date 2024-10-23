using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public StockStatus CheckStock(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            // Si el producto no es encontrado, retorna un estado de Agotado
            if (product is null)
            {
                return StockStatus.Agotado; // O puedes usar un valor predeterminado que consideres adecuado
            }

            return product.Stock > 0 ? StockStatus.Disponible : StockStatus.Agotado;
        }
    }
}
