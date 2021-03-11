using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
namespace Tree.Data.UnitOfWorks
{
    /// <summary>
    /// 获取配置项参数
    /// </summary>
    public class Demo
    {
        public Demo(IOptions<Redis> redis)
        {
            _redis = redis;
            var dbredis = _redis.Value;
        }

        public readonly IOptions<Redis> _redis;

        public Object DATA() {
            return _redis.Value;
        }
    }
}
