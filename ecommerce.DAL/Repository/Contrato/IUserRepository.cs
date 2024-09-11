using ecommerce.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DAL.Repository.Contrato
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Register(User user);

    }
}
