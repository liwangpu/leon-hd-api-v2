using MediatR;
namespace App.MoreJee.Domain.Events.PackageMapEvents
{
    public class PakcageMapDeleteEvent : INotification
    {
        public string SourceAssetId { get; protected set; }
        public string UnCookedAssetId { get; protected set; }
        public string CookedAssetId { get; protected set; }
        public string CookedAssetIdOfAndroid { get; protected set; }
        public string CookedAssetIdOfIOS { get; protected set; }

        public PakcageMapDeleteEvent(string packageStr)
        {
            //if (string.IsNullOrWhiteSpace(packageStr)) return;
            //var obj = JsonConvert.DeserializeObject<PackageStructure>(packageStr);
            //SourceAssetId = obj.SourceAssetId;
            //UnCookedAssetId = obj.UnCookedAssetId;
            //CookedAssetId = obj.CookedAssetId;
            //CookedAssetIdOfAndroid = obj.CookedAssetIdOfAndroid;
            //CookedAssetIdOfIOS = obj.CookedAssetIdOfIOS;
        }
    }
}
