using App.Basic.Domain.Consts;
using App.Basic.Domain.SeedWork;
using System.Collections.Generic;

namespace App.Basic.Domain.AggregateModels.PermissionAggregate
{
    public class AccessPoint : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string PointKey { get; protected set; }
        public int IsInner { get; protected set; }

        public string ApplyOranizationTypeIds { get; protected set; }

        #region ctor
        protected AccessPoint()
        {
        }

        public AccessPoint(string name, string pointKey, string description, string applyOrganTypeIds = null)
            : this()
        {
            Name = name;
            PointKey = pointKey;
            Description = description;
            ApplyOranizationTypeIds = applyOrganTypeIds;
        } 
        #endregion

        public AccessPoint(string name, string pointKey, string description, List<int> applyOrganTypeIds)
            : this(name, pointKey, description)
        {
            if (applyOrganTypeIds == null || applyOrganTypeIds.Count == 0) return;

            ApplyOranizationTypeIds = string.Join(",", applyOrganTypeIds);
        }


        public void UpdateBasicInfo(string name, string pointKey, string description)
        {
            Name = name;
            PointKey = pointKey;
            Description = description;
        }

        public void SignInner()
        {
            IsInner = EntityStateConst.Yes;
        }

    }
}
