using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public MoreJeeAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public CategoryRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<Category> FindAsync(string id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public async Task AddAsync(Category entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.ParentId))
            {
                var parentCat = await FindAsync(entity.ParentId);
                entity.SetFingerprint(parentCat.Fingerprint);
                entity.SetLValue(parentCat.RValue);
                entity.SetRValue(entity.LValue + 1);
                entity.SetResource(parentCat.Resource);

                var displayIndex = await _context.Set<Category>().Where(x => x.ParentId == parentCat.Id).CountAsync();
                entity.SetDisplayIndex(displayIndex);

                #region 改动相关节点左右值信息
                {
                    var affectCats = await _context.Set<Category>().Where(x => x.Fingerprint == parentCat.Fingerprint && x.RValue >= parentCat.RValue).ToListAsync();

                    for (var idx = affectCats.Count - 1; idx >= 0; idx--)
                    {
                        var cat = affectCats[idx];
                        cat.SetRValue(cat.RValue + 2);
                        if (cat.LValue > parentCat.LValue)
                            cat.SetLValue(cat.LValue + 2);
                        _context.Set<Category>().Update(cat);
                    }
                }
                #endregion

            }
            else
            {
                //全新节点
                entity.SetLValue(1);
                entity.SetRValue(2);
                entity.SetFingerprint();
            }


            _context.Set<Category>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            _context.Set<Category>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task ChangeHierarchyAsync(string categoryId, string parentId)
        {
            var category = await FindAsync(categoryId);
            //需要移动的分类节点
            var hierarchyCategories = await _context.Set<Category>().Where(x => x.Fingerprint == category.Fingerprint && x.LValue >= category.LValue && x.RValue <= category.RValue).ToListAsync();
            //原父节点
            var orgParentCategory = await FindAsync(category.ParentId);
            //改变父节点分两步走
            //1.右值大于当前节点右值的节点,右值减去间距值,左值大于当前节点左值的节点,左值减去间距(间距值等于移动节点的右值减去左值+1)
            var spaceBetween = category.RValue - category.LValue + 1;//间距

            var affectCategories = await _context.Set<Category>().Where(x => x.Fingerprint == category.Fingerprint && x.RValue > category.RValue).ToListAsync();
            ////移除移动分类中左右值在新的节点中的影响
            //for (int idx = affectCategories.Count - 1; idx >= 0; idx--)
            //{
            //    var item = affectCategories[idx];
            //    if (hierarchyCategories.Any(x => x.Id == item.Id))
            //        affectCategories.RemoveAt(idx);
            //}

            for (int idx = affectCategories.Count - 1; idx >= 0; idx--)
            {
                var item = affectCategories[idx];
                item.SetRValue(item.RValue - spaceBetween);
                if (item.LValue > category.LValue)
                    item.SetLValue(item.LValue - spaceBetween);
                _context.Set<Category>().Update(item);
            }
            //先保存一次
            await _context.SaveEntitiesAsync();
            //2.新父节点右值减去要移动分类节点树顶层节点的左值结果为新间距
            //需要移动的分类节点树中所有节点左右值加上新间距
            var newParentCategory = await FindAsync(parentId);
            var newSpeceBetween = newParentCategory.RValue - category.LValue;

            for (int idx = hierarchyCategories.Count - 1; idx >= 0; idx--)
            {
                var item = hierarchyCategories[idx];
                if (item.Id == categoryId)
                {
                    var sameLevelIndexs = await _context.Set<Category>().Where(x => x.ParentId == newParentCategory.Id).Select(x => x.DisplayIndex).ToListAsync();
                    if (sameLevelIndexs != null && sameLevelIndexs.Count > 0)
                        item.SetDisplayIndex(sameLevelIndexs.Max() + 1);
                    else
                        item.SetDisplayIndex(0);
                    item.SetParentId(parentId);
                }
                item.SetLValue(item.LValue + newSpeceBetween);
                item.SetRValue(item.RValue + newSpeceBetween);
                _context.Set<Category>().Update(item);
            }

            var newAffactCategories = await _context.Set<Category>().Where(x => x.Fingerprint == category.Fingerprint && x.RValue >= newParentCategory.RValue).ToListAsync();
            //移除移动分类中左右值在新的节点中的影响
            for (int idx = newAffactCategories.Count - 1; idx >= 0; idx--)
            {
                var item = newAffactCategories[idx];
                if (hierarchyCategories.Any(x => x.Id == item.Id))
                    newAffactCategories.RemoveAt(idx);
            }

            for (int idx = newAffactCategories.Count - 1; idx >= 0; idx--)
            {
                var item = newAffactCategories[idx];
                item.SetRValue(item.RValue + spaceBetween);
                if (item.LValue > newParentCategory.LValue)
                    item.SetLValue(item.LValue + spaceBetween);
                _context.Set<Category>().Update(item);
            }



            await _context.SaveEntitiesAsync();

        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var entity = await FindAsync(id);

            #region 改动相关节点左右值信息
            {
                var affectCats = await _context.Set<Category>().Where(x => x.Fingerprint == entity.Fingerprint && x.RValue > entity.RValue).ToListAsync();

                for (var idx = affectCats.Count - 1; idx >= 0; idx--)
                {
                    var cat = affectCats[idx];
                    cat.SetRValue(cat.RValue - 2);
                    if (cat.LValue > entity.LValue)
                        cat.SetLValue(cat.LValue - 2);
                    _context.Set<Category>().Update(cat);
                }
            }
            #endregion

            _context.Set<Category>().Remove(entity);
            await _context.SaveEntitiesAsync();
        }

        public IQueryable<Category> Get(ISpecification<Category> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Category>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Category> Paging(IPagingSpecification<Category> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Category>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task<string> GetCategoryName(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId)) return string.Empty;

            var catName = await _context.Set<Category>().Where(x => x.Id == categoryId).Select(x => x.Name).FirstOrDefaultAsync();

            return string.IsNullOrWhiteSpace(catName) ? string.Empty : catName;
        }

        public async Task<string> GetAllSubCategoryIds(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId)) return string.Empty;

            var node = await FindAsync(categoryId);
            if (node == null) return string.Empty;

            if (node.RValue - node.LValue == 1) return categoryId;

            var idArr = await _context.Set<Category>().Where(x => x.Fingerprint == node.Fingerprint && x.LValue >= node.LValue && x.RValue <= node.RValue).Select(x => x.Id).ToListAsync();

            return string.Join(',', idArr);
        }
    }
}
