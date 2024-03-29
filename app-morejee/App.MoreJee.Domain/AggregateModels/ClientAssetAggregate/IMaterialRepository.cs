﻿using App.Base.Domain.Common;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task DeleteAsync(Material data, string operatorId);
    }
}
