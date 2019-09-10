using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        public BasicAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public UserRoleRepository(BasicAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<UserRole> FindAsync(string id)
        {
            return await _context.Set<UserRole>().FindAsync(id);
        }

        public IQueryable<UserRole> Get(ISpecification<UserRole> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<UserRole>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<UserRole> Paging(IPagingSpecification<UserRole> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<UserRole>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public void Add(UserRole entity)
        {
            entity._CustomizeId(GuidGenerator.NewGUID());
            _context.Set<UserRole>().Add(entity);
        }


        public async Task DeleteAsync(string id, string operatorId)
        {
            var UserRole = await _context.Set<UserRole>().FirstOrDefaultAsync(x => x.Id == id);
            _context.Set<UserRole>().Remove(UserRole);
            await _context.SaveEntitiesAsync();
        }


    }
}
