using System;
using System.Collections.Generic;
using System.Text;
using Treebank.Core.Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Treebank.Core.Configs
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration Instance => ServiceLocator.Instance.GetService<IConfiguration>();

        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns></returns>
        public static string LoadConfig(string key)
        {
            return Instance[key];
        }
    }
}
