﻿using App.MoreJee.Domain.SeedWork;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Task LoadOwnProductSpecsAsync(Product entity);
        Task DeleteAsync(Product data, string operatorId);
    }
}
