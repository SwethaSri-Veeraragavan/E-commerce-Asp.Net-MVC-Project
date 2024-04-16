using Bigbasket.DataAccess.Data;
using Bigbasket.DataAccess.Repository.IRepository;
using Bigbasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigbasket.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private BigBasketDbContext _db;
        public ProductRepository(BigBasketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=> u.ProductId == obj.ProductId);
            if (objFromDb != null)
            {
                objFromDb.ProductName = obj.ProductName;
                objFromDb.Mrp = obj.Mrp;
                objFromDb.Price = obj.Price;
                objFromDb.Offer = obj.Offer;
                objFromDb.ProductDescription = obj.ProductDescription;
                objFromDb.AboutProduct = obj.AboutProduct;
                objFromDb.Benefit = obj.Benefit;
                objFromDb.Uses = obj.Uses;
                //objFromDb.ImageUrl = objFromDb.ImageUrl;
                objFromDb.CategoryId = obj.CategoryId;
                

                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

                //_db.SaveChanges();
            }
        }
    }
    
}
