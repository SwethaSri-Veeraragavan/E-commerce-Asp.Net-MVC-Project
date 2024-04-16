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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private BigBasketDbContext _db;
        public ShoppingCartRepository(BigBasketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }
    }
}
