using Apps.OSS.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.OSS.Domain.AggregateModels.FileAssetAggregate
{
    public class FileAsset : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public long Size { get; protected set; }
        public string FileExt { get; protected set; }
        public int FileState { get; set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }

        #region ctor
        protected FileAsset()
        {

        }

        public FileAsset(string name)
        {

        }
        #endregion




    }
}
