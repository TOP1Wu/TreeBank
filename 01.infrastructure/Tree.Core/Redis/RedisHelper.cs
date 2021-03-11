using AspectCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using Treebank.Core.Autofac;
using Microsoft.Extensions.Options;
namespace Tree.Core.Redis
{
    public static class RedisHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbnum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static RedisClient RedisClient(int? dbnum = null)
        {
            return new RedisClient(0, "whwu.xyz:8080,password=123456");
        }
    }
}
