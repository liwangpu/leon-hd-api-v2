using App.Base.Domain.Common;
using System.Collections.Generic;
using System.Linq;
namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class ProductPermissionGroup : Entity
    {
        private readonly List<ProductPermissionItem> _ownProductItems;
        private readonly List<ProductPermissionOrgan> _ownOrganItems;
        public IReadOnlyCollection<ProductPermissionItem> OwnProductItems => _ownProductItems;
        public IReadOnlyCollection<ProductPermissionOrgan> OwnOrganItems => _ownOrganItems;
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string OrganizationId { get; protected set; }

        #region ctor
        protected ProductPermissionGroup()
        {
            _ownProductItems = new List<ProductPermissionItem>();
            _ownOrganItems = new List<ProductPermissionOrgan>();
        }
        public ProductPermissionGroup(string name, string description, string organizationId)
        {
            Id = GuidGen.NewGUID();
            Name = name;
            Description = description;
            OrganizationId = organizationId;
        }
        #endregion

        public void UpdateBasicInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void AddOwnOrganization(string organId)
        {
            var exit = OwnOrganItems.Any(x => x.OrganizationId == organId);
            if (exit) return;
            var it = new ProductPermissionOrgan(organId, Id);
            _ownOrganItems.Add(it);
        }

        public void DeleteOwnOrganization(string itemId)
        {
            for (var idx = _ownOrganItems.Count - 1; idx >= 0; idx--)
            {
                if (_ownOrganItems[idx].Id == itemId)
                {
                    _ownOrganItems.RemoveAt(idx);
                    break;
                }
            }
        }

        public void AddOwnProduct(string productId)
        {
            var exit = OwnProductItems.Any(x => x.ProductId == productId);
            if (exit) return;
            var it = new ProductPermissionItem(productId, Id);
            _ownProductItems.Add(it);
        }

        public void DeleteOwnProduct(string itemId)
        {
            for (var idx = _ownProductItems.Count - 1; idx >= 0; idx--)
            {
                if (_ownProductItems[idx].Id == itemId)
                {
                    _ownProductItems.RemoveAt(idx);
                    break;
                }
            }
        }

    }
}
