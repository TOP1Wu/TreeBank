using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using Tree.Core.Domain.UnitOfWork;

namespace Tree.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IOptions<DBOption> options)
        {
            _options = options;
            var dbOption = _options.Value;
            Connection = new SqlConnection(dbOption.ConnectionString);
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }


        public readonly IOptions<DBOption> _options;

        ///// <summary>
        ///// 工作单元
        ///// </summary>
        //public UnitOfWork(IOptions<Redis> options)
        //{
        //    _options = options;
        //    var dbOption = _options.Value;
        //    //Connection = new SqlConnection(dbOption.ConnectionString);
        //    //if (Connection.State == ConnectionState.Closed)
        //    //{
        //    //    Connection.Open();
        //    //}
        //}


        //public readonly IOptions<Redis> _options;

        /// <summary>
        /// 连接对象
        /// </summary>
        public IDbConnection Connection { get; }

        /// <summary>
        /// 事务
        /// </summary>
        public IDbTransaction Transaction { get; private set; }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            Transaction.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            Transaction?.Rollback();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Transaction?.Dispose();
            Connection.Dispose();
        }
    }
}
