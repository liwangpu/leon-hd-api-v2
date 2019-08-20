using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.Basic.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Infrastructure.Repositories
{
    public class OrganizationTreeRepository : IOrganizationTreeRepository
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
        public OrganizationTreeRepository(BasicAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<OrganizationTree> FindAsync(string id)
        {
            return await _context.Set<OrganizationTree>().FindAsync(id);
        }

        public IQueryable<OrganizationTree> Get(ISpecification<OrganizationTree> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<OrganizationTree>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<OrganizationTree> Paging(IPagingSpecification<OrganizationTree> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<OrganizationTree>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(OrganizationTree entity)
        {
            if (string.IsNullOrWhiteSpace(entity.ParentObjId))
            {
                entity.SetLValue(1);
                entity.SetRValue(2);
                entity.SetGroup(GuidGen.NewGUID());
            }
            else
            {
                var parentNode = _context.Set<OrganizationTree>().FirstOrDefault(x => x.ObjId == entity.ParentObjId);
                if (parentNode == null)
                    throw new Exception($"找不到ObjId为{entity.ParentObjId}的上级节点");
                var referenceNodes = _context.Set<OrganizationTree>().Where(x => x.Group == parentNode.Group).ToList();
                //新节点
                entity.SetLValue(parentNode.RValue);
                entity.SetRValue(parentNode.RValue + 1);
                entity.SetGroup(parentNode.Group);
                entity.SetParentId(parentNode.Id);
                for (int idx = referenceNodes.Count - 1; idx >= 0; idx--)
                {
                    var node = referenceNodes[idx];
                    //1.父节点
                    //只是右值加2
                    if (node.Id == parentNode.Id)
                    {
                        node.SetRValue(node.RValue + 2);
                        continue;
                    }

                    //2.顶级节点
                    //只是右值加2
                    if (node.LValue == 1)
                    {
                        node.SetRValue(node.RValue + 2);
                        continue;
                    }

                    //3.沿线节点
                    //左右值都加2
                    if (node.LValue > parentNode.LValue && node.RValue > parentNode.RValue)
                    {
                        node.SetLValue(node.LValue + 2);
                        node.SetRValue(node.RValue + 2);
                        continue;
                    }
                }
                _context.Set<OrganizationTree>().UpdateRange(referenceNodes);
            }
            _context.Set<OrganizationTree>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(OrganizationTree entity)
        {
            _context.Set<OrganizationTree>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
           
        }


    }
}
