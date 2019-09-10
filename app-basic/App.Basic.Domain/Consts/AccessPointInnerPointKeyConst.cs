namespace App.Basic.Domain.Consts
{
    public class AccessPointInnerPointKeyConst
    {
        /// <summary>
        /// 商品基础信息编辑,不包括价格
        /// </summary>
        public const string ProductBasicInfoManagement = "PRODUCT_BASIC_INFO_MANAGEMENT";
        /// <summary>
        /// 查看进货价
        /// </summary>
        public const string PurchasePriceRetrieve = "PURCHASE_PRICE_RETRIEVE";
        /// <summary>
        /// 查看代理商价格
        /// </summary>
        public const string PartnerPriceRetrieve = "PARTNER_PRICE_RETRIEVE";
        /// <summary>
        /// 查看零售价
        /// </summary>
        public const string PriceRetrieve = "PRICE_RETRIEVE";
        /// <summary>
        /// 编辑进货价
        /// </summary>
        public const string PurchasePriceEdit = "PURCHASE_PRICE_EDIT";
        /// <summary>
        /// 编辑代理商价格
        /// </summary>
        public const string PartnerPriceEdit = "PARTNER_PRICE_EDIT";
        /// <summary>
        /// 编辑零售价
        /// </summary>
        public const string PriceEdit = "PRICE_EDIT";

        /// <summary>
        /// 管理客户端资源,包括编辑和删除权限
        /// 客户端资源是实现IClientAsset接口的
        /// </summary>
        public const string ClientAssetManagement = "CLIENT_ASSET_MANAGEMENT";


    }
}
