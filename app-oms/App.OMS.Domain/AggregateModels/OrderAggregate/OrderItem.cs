using App.Base.Domain.Common;

namespace App.OMS.Domain.AggregateModels.OrderAggregate
{
    public class OrderItem : Entity
    {

        public string ProductId { get; protected set; }
        public string ProductName { get; protected set; }
        public string ProductIcon { get; protected set; }
        public string ProductDescription { get; protected set; }
        public string ProductBrand { get; protected set; }
        public string ProductUnit { get; protected set; }
        public string ProductSpecId { get; protected set; }
        public string ProductSpecName { get; protected set; }
        public string ProductSpecIcon { get; protected set; }
        public string ProductSpecDescription { get; protected set; }
        public int Num { get; protected set; }
        public decimal UnitPrice { get; protected set; }
        public decimal TotalPrice { get { return Num * UnitPrice; } }
        public string Remark { get; protected set; }
        public string OrderId { get; protected set; }
        public Order Order { get; protected set; }

        #region ctor
        protected OrderItem()
        {

        }

        public OrderItem(string productId, string productName, string productDes, string productIcon, string productBrand, string productUnit, string productSpecId, string productSpecName, string productSpecDes, string productSpecIcon, int num, decimal unitPrice, string remark)
            : this()
        {
            Id = GuidGen.NewGUID();
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDes;
            ProductIcon = productIcon;
            ProductBrand = productBrand;
            ProductUnit = productUnit;
            ProductSpecId = productSpecId;
            ProductSpecName = productSpecName;
            ProductSpecDescription = productSpecDes;
            ProductSpecIcon = productSpecIcon;
            Num = num;
            UnitPrice = unitPrice;
            Remark = remark;
        }
        #endregion


        public void ChangeNum(int num)
        {
            Num = num;
        }
    }
}
