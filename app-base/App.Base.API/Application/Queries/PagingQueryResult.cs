using System.Collections.Generic;

namespace App.Base.API.Application.Queries
{
    public class PagingQueryResult<DTO>
        where DTO : class
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<DTO> Data { get; set; }
    }
}
