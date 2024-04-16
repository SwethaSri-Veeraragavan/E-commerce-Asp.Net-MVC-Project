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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private BigBasketDbContext _db;
        public CategoryRepository(BigBasketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
    
}
