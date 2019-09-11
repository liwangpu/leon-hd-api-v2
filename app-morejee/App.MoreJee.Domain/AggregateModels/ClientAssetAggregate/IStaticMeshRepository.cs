﻿using App.MoreJee.Domain.SeedWork;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public interface IStaticMeshRepository : IRepository<StaticMesh>
    {
        Task DeleteAsync(StaticMesh data, string operatorId);
    }
}
