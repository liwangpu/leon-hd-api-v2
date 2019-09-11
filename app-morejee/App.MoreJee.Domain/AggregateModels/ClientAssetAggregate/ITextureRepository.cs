﻿using App.MoreJee.Domain.SeedWork;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public interface ITextureRepository : IRepository<Texture>
    {
        Task DeleteAsync(Texture data, string operatorId);
    }
}
