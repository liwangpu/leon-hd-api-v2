﻿using App.Base.Domain.Common;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public interface IMapRepository : IRepository<Map>
    {
        Task DeleteAsync(Map data, string operatorId);
    }
}
