using App.Base.Domain.Common;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public class OrganizationTree : Tree
    {
        public string ParentObjId { get; protected set; }
        public string Group { get; protected set; }

        #region ctor
        protected OrganizationTree()
          : base()
        {

        }

        public OrganizationTree(string objId, string name, string nodeType, string parentObjId)
            : base(objId, name, nodeType)
        {
            ParentObjId = parentObjId;
        }
        #endregion


        public void SetGroup(string group)
        {
            Group = group;
        }

        public void UpdateNodeName(string name)
        {
            Name = name;
        }



    }
}
