using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.OMS.Infrastructure.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        public OMSAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public CustomerRepository(OMSAppContext context)
        {
            _context = context;
        }
        #endregion


        public async Task<Customer> FindAsync(string id)
        {
            return await _context.Set<Customer>().FindAsync(id);
        }

        public IQueryable<Customer> Get(ISpecification<Customer> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Customer>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Customer> Paging(IPagingSpecification<Customer> specification)
        {
            var noCustomer = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<Customer>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noCustomer ? "ModifiedTime" : specification.OrderBy, noCustomer ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Customer entity)
        {
            _context.Set<Customer>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _context.Set<Customer>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            _context.Set<Customer>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }
    }
}
