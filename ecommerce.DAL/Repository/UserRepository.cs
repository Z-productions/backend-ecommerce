using ecommerce.DAL.Repository.Contrato;
using ecommerce.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ecommerce.DAL.Repository
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        private readonly EcommerceContext ecommerceContext;

        public UserRepository(EcommerceContext ecommerceContext) : base(ecommerceContext)
        {
            this.ecommerceContext = ecommerceContext;
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
