using Dapper;
using System;

namespace Tree.Data.Requests
{
    /// <summary>
    /// 根据主键操作传参
    /// </summary>
    public class IdInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public Guid? Id { get; set; }
    }
}
