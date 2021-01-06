using AspectCore.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using Treebank.Core.Autofac;
using Microsoft.Extensions.Options;
namespace Tree.Core.Redis
{
    public static class RedisHelper
    {
        public readonly IOptions<RedisConfig> _redisconfig;


        public Data(IOptions<RedisConfig> redisConfig)
        {
            _redisconfig = redisConfig;
            return redisConfig;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbnum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static RedisClient RedisClient(int? dbnum = null)
        {
            //var configuration = ServiceLocator.Instance.Resolve<IConfiguration>();
            //var conn = configuration["Redis:Link"];
            //if (dbnum == null)
            //{
            //    dbnum = int.Parse(configuration["Redis:DefaultDb"]);
            //}

            //if (configuration == null)
            //{
            //    throw new System.Exception("!!!!!");
            //}
            return new RedisClient(0, "acvip.cn:8080,password=123456");
            //return new RedisClient(dbnum.Value, conn);
        }
    }
}
