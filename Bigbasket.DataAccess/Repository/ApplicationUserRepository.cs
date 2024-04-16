using Bigbasket.DataAccess.Data;
using Bigbasket.DataAccess.Repository;
using Bigbasket.Models;
using BigBasket.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigBasket.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository {
        private BigBasketDbContext _db;
        public ApplicationUserRepository(BigBasketDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ApplicationUser applicationUser) {
            _db.ApplicationUsers.Update(applicationUser);
        }

        
    }
}
