using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.OMS.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
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
        public OrderRepository(OMSAppContext context)
        {
            _context = context;
        }
        #endregion

        protected EntityEntry<Order> Entry(Order entity)
        {
            return _context.Entry(entity);
        }

        public async Task LoadOwnOrderItemsAsync(Order entity)
        {
            if (entity == null) return;
            await Entry(entity).Collection(x => x.OwnOrderItems).LoadAsync();
        }

        public async Task LoadCustomerAsync(Order entity)
        {
            if (entity == null) return;
            await Entry(entity).Reference(x => x.Customer).LoadAsync();
        }

        public async Task<Order> FindAsync(string id)
        {
            return await _context.Set<Order>().FindAsync(id);
        }

        public IQueryable<Order> Get(ISpecification<Order> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Order>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Order> Paging(IPagingSpecification<Order> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<Order>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "ModifiedTime" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Order entity)
        {
            //生成订单编号
            var beginTime = new DateTime(entity.CreatedTime.Year, entity.CreatedTime.Month, entity.CreatedTime.Day);
            var endTime = beginTime.AddDays(1);
            var orderCount = await _context.Set<Order>().Where(x => x.CreatedTime >= beginTime && x.CreatedTime < endTime).CountAsync();
            var no = beginTime.ToString("yyyyMMdd") + (orderCount + 1).ToString().PadLeft(5, '0');
            entity.SetOrderNo(no);

            _context.Set<Order>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            _context.Set<Order>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            _context.Set<Order>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }


    }
}
