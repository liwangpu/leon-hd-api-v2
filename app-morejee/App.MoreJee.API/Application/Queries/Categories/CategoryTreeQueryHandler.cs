using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Infrastructure.Specifications.CategorySpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryTreeQueryHandler : IRequestHandler<CategoryTreeQuery, CategoryTreeQueryDTO>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        private void FindChildren(LinkedList<CategoryTreeQueryDTO> list, CategoryTreeQueryDTO parent)
        {
            if (parent == null)
                return;
            var node = list.First;
            while (node != null)
            {
                var next = node.Next;

                var item = node.Value;
                if (item.ParentId == parent.Id)
                {
                    parent.Children.Add(item);
                    list.Remove(node);
                }
                node = next;
            }

            parent.Children = parent.Children.OrderBy(x => x.DisplayIndex).ToList();

            //标记首节点和尾节点
            if (parent.Children != null && parent.Children.Count > 0)
            {
                if (parent.Children[0] != null)
                    parent.Children[0].FirstNode = true;
                if (parent.Children[parent.Children.Count - 1] != null)
                    parent.Children[parent.Children.Count - 1].LastNode = true;
            }

            foreach (var item in parent.Children)
            {
                FindChildren(list, item);
            }
        }


        #region ctor
        public CategoryTreeQueryHandler(ICategoryRepository categoryRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.categoryRepository = categoryRepository;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<CategoryTreeQueryDTO> Handle(CategoryTreeQuery request, CancellationToken cancellationToken)
        {
            var cat = await categoryRepository.FindAsync(request.Id);
            if (cat == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Category", request.Id]);

            var filteredIdArr = string.IsNullOrWhiteSpace(request.FilteredIds) ? new string[] { } : request.FilteredIds.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var categories = await categoryRepository.Get(new GetCategoryByFingerprintSpecification(cat.Fingerprint)).ToListAsync();

            foreach (var filteredId in filteredIdArr)
            {
                var filteredCategory = categories.FirstOrDefault(x => x.Id == filteredId);
                if (filteredCategory != null)
                {
                    for (var idx = categories.Count - 1; idx >= 0; idx--)
                    {
                        var it = categories[idx];
                        if (it.LValue >= filteredCategory.LValue && it.RValue <= filteredCategory.RValue)
                            categories.RemoveAt(idx);
                    }
                }
            }

            var catDtos = categories.Select(x => CategoryTreeQueryDTO.From(x)).ToList();
            var linkedList = new LinkedList<CategoryTreeQueryDTO>();
            foreach (var item in catDtos)
                linkedList.AddLast(item);

            var rootCat = catDtos.First(x => string.IsNullOrWhiteSpace(x.ParentId));
            //标记首节点和尾节点
            rootCat.FirstNode = true;
            rootCat.LastNode = true;

            FindChildren(linkedList, rootCat);


            //尝试翻译根节点名称



            return rootCat;
        }
        #endregion
    }
}
