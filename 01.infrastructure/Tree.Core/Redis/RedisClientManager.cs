using StackExchange.Redis;
using System;
using System.Collections.Concurrent;

namespace Tree.Core.Redis
{
    public class RedisClientManager
    {
        //系统自定义Key前缀
        /// <summary>
        /// 
        /// </summary>
        public const string SysCustomKey = "";

        //"127.0.0.1:6379, allow admin=true
        /// <summary>
        /// 
        /// </summary>
        private const string RedisConnectionString = "127.0.0.1:6379,allowadmin=true";

        /// <summary>
        /// 
        /// </summary>
        private static readonly object Locker = new object();


        /// <summary>
        /// 
        /// </summary>
        private static ConnectionMultiplexer _instance;


        /// <summary>
        /// 
        /// </summary>
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> ConnectionCache =
            new ConcurrentDictionary<string, ConnectionMultiplexer>();

        /// <summary>
        /// 单例获取Redis多路连接器
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = GetManager();
                        }
                    }
                }

                return _instance;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            connectionString = connectionString ?? RedisConnectionString;
            var connect = ConnectionMultiplexer.Connect(connectionString);
            //注册如下事件
            //connect.ConnectionFailed += MuxerConnectionFailed;
            //connect.ConnectionRestored += MuxerConnectionRestored;
            //connect.ErrorMessage += MuxerErrorMessage;
            //connect.ConfigurationChanged += MuxerConfigurationChanged;
            //connect.HashSlotMoved += MuxerHashSlotMoved;
            //connect.InternalError += MuxerInternalError;

            return connect;
        }

        /// <summary>
        /// 缓存获取Redis多路连接器
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            if (!ConnectionCache.ContainsKey(connectionString))
            {
                ConnectionCache[connectionString] = GetManager(connectionString);
            }

            return ConnectionCache[connectionString];
        }

        #region 事件

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine("RedisClientManager_MuxerConfigurationChanged_Configuration_changed__" + e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine("RedisClientManager_MuxerConfigurationChanged_Configuration_changed__" + e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("RedisClientManager_MuxerConfigurationChanged_Configuration_changed__" + e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("RedisClientManager_MuxerConfigurationChanged_Configuration_changed__" + e.EndPoint +
                              ", " + e.FailureType +
                              (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine("RedisClientManager_MuxerHashSlotMoved_HashSlotMoved_NewEndPoint" + e.NewEndPoint +
                              "RedisClientManager_MuxerHashSlotMoved____OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("RedisClientManager_MuxerInternalError_InternalError_Message " + e.Exception.Message);
        }

        #endregion 事件
    }
}
