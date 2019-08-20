using App.Base.Domain.Common;
using App.Base.Domain.Extentions;
using System;

namespace App.OSS.Domain.AggregateModels.FileAssetAggregate
{
    public class FileAsset : Entity
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
        /// <summary>
        /// 冗余字段,记录一下该资源的访问路径,省的每次put,get,请求都合成一次
        /// </summary>
        public string Url { get; protected set; }


        #region ctor
        private FileAsset()
        {

        }

        public FileAsset(string name, string description, string fileExt, int fileState, long size, string url, string creator)
            : this()
        {
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
            Name = name;
            Description = description;
            Size = size;
            FileExt = fileExt;
            FileState = fileState;
            Size = size;
            Creator = creator;
            Modifier = Creator;
            Url = url;
        }
        #endregion

        /// <summary>
        /// 更新资源文件的基础信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="fileState"></param>
        /// <param name="modifier"></param>
        public void UpdateBasicInfo(string name, string description, int fileState, string modifier)
        {
            Name = name;
            Description = description;
            FileState = fileState;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }


    }
}
