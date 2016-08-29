using System;
using System.Threading.Tasks;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BrewerRepository : GenericRepository<Coffee>, IBrewerRepository
    {
        public BrewerRepository(BookifyContext context) : base(context)
        {
        }
        public async Task<Coffee> GetCoffee()
        {
            return await Task.Factory.StartNew( 
                () => new Coffee()
                {
                    BadCoffee = new Random().NextDouble() >= 0.5
                });
        }
    }

    public interface IBrewerRepository
    {
        Task<Coffee> GetCoffee();
    }
}
