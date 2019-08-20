using MediatR;
using System.Collections.Generic;

namespace App.MoreJee.API.Application.Queries.PackageMaps
{
    public class PackageMapNamesMapQuery : IRequest<List<string>>
    {
        public string Packages { get; set; }

        public PackageMapNamesMapQuery(string packages)
        {
            Packages = packages;
        }
    }
}
