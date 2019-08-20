using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace App.MoreJee.API.Application.Queries.PackageMaps
{
    public class PackageMapIdentityQuery : IRequest<PackageMapIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public string MapType { get; protected set; }
        public PackageMapIdentityQuery(string id, string mapType)
        {
            Id = id;
            MapType = mapType;
        }
    }

    public class PackageMapIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Package { get; set; }
        public List<DependencyMap> Dependencies { get; set; }
        public string ResourceId { get; set; }
        public string ResourceType { get; set; }
        public string Property { get; set; }

        public static PackageMapIdentityQueryDTO From(PackageMap data, string mapType)
        {

            var dto = new PackageMapIdentityQueryDTO
            {
                Id = data.Id,
                Package = data.Package,
                ResourceId = data.ResourceId,
                ResourceType = data.ResourceType,
                Property = data.Property
            };

            if (mapType.Contains("source"))
                dto.Dependencies = DependencyMap.From(data.DependencyAssetUrlsOfSource);
            if (mapType.Contains("uncooked"))
                dto.Dependencies = DependencyMap.From(data.DependencyAssetUrlsOfUnCooked);
            else if (mapType.Contains("ios"))
                dto.Dependencies = DependencyMap.From(data.DependencyAssetUrlsOfIOSCooked);
            else if (mapType.Contains("android"))
                dto.Dependencies = DependencyMap.From(data.DependencyAssetUrlsOfAndroidCooked);
            else
                dto.Dependencies = DependencyMap.From(data.DependencyAssetUrlsOfWin64Cooked);

            return dto;
        }

        public class DependencyMap
        {
            public string Package { get; set; }
            public string AssetUrl { get; set; }

            public static List<DependencyMap> From(string deps)
            {
                var list = new List<DependencyMap>();
                if (!string.IsNullOrWhiteSpace(deps))
                {
                    var itArr = deps.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var itStr in itArr)
                    {
                        var arr = itStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
                        if (arr.Length >= 2)
                            list.Add(new DependencyMap { Package = arr[0], AssetUrl = arr[1] });
                    }
                }
                return list;
            }
        }
    }



}
