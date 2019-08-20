using MediatR;
using System.Collections.Generic;

namespace App.OMS.API.Application.Commands.Orders
{
    /// <summary>
    /// 订单创建参数命令
    /// </summary>
    public class OrderCreateCommand : IRequest<string>
    {
        /// <summary>
        /// 订单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 订单项信息
        /// </summary>

        public List<OrderItem> OrderItems { get; set; }

        #region ctor
        public OrderCreateCommand()
        {
            OrderItems = new List<OrderItem>();
        }
        #endregion
    }

    /// <summary>
    /// 订单项
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 产品规格Id
        /// </summary>
        public string ProductSpecId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
