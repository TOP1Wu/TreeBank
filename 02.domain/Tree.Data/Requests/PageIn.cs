using System;
using System.Collections.Generic;
using System.Text;

namespace Tree.Data.Requests
{
    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class PageIn
    {
        /// <summary>
        /// 页索引
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
