﻿using App.Base.Domain.Common;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public interface IPackageMapRepository : IRepository<PackageMap>
    {
        Task DeleteAsync(string resourceId);
    }
}
